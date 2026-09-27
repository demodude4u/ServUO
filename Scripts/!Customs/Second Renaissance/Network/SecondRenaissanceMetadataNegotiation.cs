// SPDX-License-Identifier: BSD-2-Clause

using Server.Misc;
using Server.Network;
using System;
using System.Runtime.CompilerServices;

namespace Server.SecondRenaissance
{
    public enum SecondRenaissanceMetadataWireState : byte { Unavailable = 0, Loaded = 1, Invalid = 2 }
    public enum SecondRenaissanceMetadataCompatibility { Unknown, Matched, Mismatched, LocalUnavailable, RemoteUnavailable, Unsupported }

    public sealed class SecondRenaissanceRemoteMetadata
    {
        internal SecondRenaissanceRemoteMetadata(SecondRenaissanceMetadataWireState state, ushort formatMajor, ushort formatMinor,
            ushort schemaMajor, ushort schemaMinor, uint revision, uint count, byte[] hash)
        { State = state; FormatMajor = formatMajor; FormatMinor = formatMinor; SchemaMajor = schemaMajor; SchemaMinor = schemaMinor;
          DatasetRevision = revision; RecordCount = count; DatasetHash = hash; }
        public SecondRenaissanceMetadataWireState State { get; private set; }
        public ushort FormatMajor { get; private set; }
        public ushort FormatMinor { get; private set; }
        public ushort SchemaMajor { get; private set; }
        public ushort SchemaMinor { get; private set; }
        public uint DatasetRevision { get; private set; }
        public uint RecordCount { get; private set; }
        public byte[] DatasetHash { get; private set; }
    }

    public sealed class SecondRenaissanceMetadataSession
    {
        internal SecondRenaissanceMetadataSession() { Compatibility = SecondRenaissanceMetadataCompatibility.Unknown; }
        public bool CapabilityAdvertised { get; internal set; }
        public bool HasRemote { get; internal set; }
        public SecondRenaissanceRemoteMetadata Remote { get; internal set; }
        public SecondRenaissanceMetadataCompatibility Compatibility { get; internal set; }
    }

    public sealed class SecondRenaissanceMetadataIdentityPacket : ProtocolExtension
    {
        public SecondRenaissanceMetadataIdentityPacket(SecondRenaissanceMetadataProvider provider)
            : base(SecondRenaissanceMetadataNegotiation.ProtocolExtensionSubcommand, 58)
        {
            SecondRenaissanceMetadataWireState state = provider != null && provider.IsAvailable
                ? SecondRenaissanceMetadataWireState.Loaded
                : provider != null && provider.State == SecondRenaissanceMetadataProviderState.Invalid
                    ? SecondRenaissanceMetadataWireState.Invalid : SecondRenaissanceMetadataWireState.Unavailable;
            bool loaded = state == SecondRenaissanceMetadataWireState.Loaded;
            byte[] hash = loaded ? ParseHex(provider.DatasetHash) : new byte[32];
            m_Stream.Write(SecondRenaissanceMetadataNegotiation.Magic);
            m_Stream.Write(SecondRenaissanceMetadataNegotiation.ProtocolVersion);
            m_Stream.Write(SecondRenaissanceMetadataNegotiation.PayloadLength);
            m_Stream.Write((byte)state); m_Stream.Write((byte)0);
            m_Stream.Write((ushort)(loaded ? 1 : 0)); m_Stream.Write((ushort)0);
            m_Stream.Write((ushort)(loaded ? 1 : 0)); m_Stream.Write((ushort)0);
            m_Stream.Write(loaded ? provider.DatasetRevision : 0);
            m_Stream.Write(loaded ? (uint)provider.RecordCount : 0);
            m_Stream.Write(hash, 0, hash.Length);
        }
        private static byte[] ParseHex(string value)
        {
            byte[] bytes = new byte[32];
            for (int i = 0; i < bytes.Length; i++) bytes[i] = Convert.ToByte(value.Substring(i * 2, 2), 16);
            return bytes;
        }
    }

    public static class SecondRenaissanceMetadataNegotiation
    {
        public const byte ProtocolExtensionSubcommand = 0xFB;
        public const uint Magic = 0x46535233;
        public const ushort ProtocolVersion = 1;
        public const ushort PayloadLength = 50;
        public const int PacketLength = 62;
        private sealed class Holder { public readonly SecondRenaissanceMetadataSession Value = new SecondRenaissanceMetadataSession(); }
        private static readonly ConditionalWeakTable<NetState, Holder> ByConnection = new ConditionalWeakTable<NetState, Holder>();

        public static void Initialize()
        {
            if (ProtocolExtensions.GetHandler(ProtocolExtensionSubcommand) != null)
                throw new InvalidOperationException("Protocol extension subcommand 0xFB is already registered.");
            ProtocolExtensions.Register(ProtocolExtensionSubcommand, false, OnIdentity);
        }

        public static SecondRenaissanceMetadataSession GetSession(NetState state)
        {
            return state == null ? new SecondRenaissanceMetadataSession() : ByConnection.GetValue(state, _ => new Holder()).Value;
        }

        public static void OnCapabilities(NetState state, bool supported)
        {
            SecondRenaissanceMetadataSession session = GetSession(state);
            session.CapabilityAdvertised = supported;
            session.Compatibility = supported ? Evaluate(session, SecondRenaissanceMetadataRuntime.Current)
                : SecondRenaissanceMetadataCompatibility.Unsupported;
            if (supported) state.Send(new SecondRenaissanceMetadataIdentityPacket(SecondRenaissanceMetadataRuntime.Current));
        }

        private static void OnIdentity(NetState state, PacketReader reader)
        {
            SecondRenaissanceMetadataSession session = GetSession(state);
            if (!session.CapabilityAdvertised || session.HasRemote) return;
            SecondRenaissanceRemoteMetadata remote;
            if (!TryParse(reader.Buffer, reader.Size, out remote)) return;
            session.Remote = remote; session.HasRemote = true;
            session.Compatibility = Evaluate(session, SecondRenaissanceMetadataRuntime.Current);
        }

        public static SecondRenaissanceMetadataCompatibility Evaluate(SecondRenaissanceMetadataSession session,
            SecondRenaissanceMetadataProvider local)
        {
            if (local == null || !local.IsAvailable) return SecondRenaissanceMetadataCompatibility.LocalUnavailable;
            if (!session.CapabilityAdvertised) return SecondRenaissanceMetadataCompatibility.Unsupported;
            if (!session.HasRemote) return SecondRenaissanceMetadataCompatibility.Unknown;
            SecondRenaissanceRemoteMetadata remote = session.Remote;
            if (remote.State != SecondRenaissanceMetadataWireState.Loaded) return SecondRenaissanceMetadataCompatibility.RemoteUnavailable;
            if (remote.FormatMajor != 1 || remote.SchemaMajor != 1 || remote.FormatMinor > 0 || remote.SchemaMinor > 0)
                return SecondRenaissanceMetadataCompatibility.Unsupported;
            byte[] localHash = ParseHex(local.DatasetHash);
            return remote.DatasetRevision == local.DatasetRevision && remote.RecordCount == local.RecordCount
                && FixedEquals(remote.DatasetHash, localHash)
                ? SecondRenaissanceMetadataCompatibility.Matched : SecondRenaissanceMetadataCompatibility.Mismatched;
        }

        internal static bool TryParse(byte[] packet, int size, out SecondRenaissanceRemoteMetadata remote)
        {
            remote = null;
            if (packet == null || size != PacketLength || size > packet.Length || packet[0] != 0xF0
                || U16(packet, 1) != size || packet[3] != ProtocolExtensionSubcommand || U32(packet, 4) != Magic
                || U16(packet, 8) != ProtocolVersion || U16(packet, 10) != PayloadLength) return false;
            var state = (SecondRenaissanceMetadataWireState)packet[12]; byte reserved = packet[13];
            ushort formatMajor = U16(packet, 14), formatMinor = U16(packet, 16), schemaMajor = U16(packet, 18), schemaMinor = U16(packet, 20);
            uint revision = U32(packet, 22), count = U32(packet, 26); byte[] hash = new byte[32]; Buffer.BlockCopy(packet, 30, hash, 0, 32);
            if (reserved != 0 || (state != SecondRenaissanceMetadataWireState.Unavailable && state != SecondRenaissanceMetadataWireState.Loaded
                && state != SecondRenaissanceMetadataWireState.Invalid)) return false;
            bool zero = formatMajor == 0 && formatMinor == 0 && schemaMajor == 0 && schemaMinor == 0 && revision == 0 && count == 0 && IsZero(hash);
            if (state == SecondRenaissanceMetadataWireState.Loaded)
            { if (formatMajor == 0 || schemaMajor == 0 || count > 1000000 || IsZero(hash)) return false; }
            else if (!zero) return false;
            remote = new SecondRenaissanceRemoteMetadata(state, formatMajor, formatMinor, schemaMajor, schemaMinor, revision, count, hash); return true;
        }

        private static byte[] ParseHex(string value) { byte[] b = new byte[32]; for (int i=0;i<32;i++) b[i]=Convert.ToByte(value.Substring(i*2,2),16); return b; }
        private static bool FixedEquals(byte[] a, byte[] b) { int d=a.Length^b.Length; for(int i=0;i<a.Length&&i<b.Length;i++) d|=a[i]^b[i]; return d==0; }
        private static bool IsZero(byte[] value) { int a=0; for(int i=0;i<value.Length;i++) a|=value[i]; return a==0; }
        private static ushort U16(byte[] b,int o) { return (ushort)((b[o]<<8)|b[o+1]); }
        private static uint U32(byte[] b,int o) { return (uint)((b[o]<<24)|(b[o+1]<<16)|(b[o+2]<<8)|b[o+3]); }
    }
}

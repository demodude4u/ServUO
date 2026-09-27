using Server.Misc;
using Server.Network;
using System;
using System.Runtime.CompilerServices;

namespace Server.SecondRenaissance
{
    [Flags]
    public enum SecondRenaissanceCapability : ulong
    {
        None = 0,
        ExplicitGumpLayout = 1UL << 0,
        ExplicitStaticArtwork = 1UL << 1,
        MetadataNegotiation = 1UL << 2
    }

    public sealed class SecondRenaissanceClientCapabilities
    {
        // This is client-advertised compatibility metadata, never proof of identity or authority.
        internal static readonly SecondRenaissanceClientCapabilities NotNegotiated =
            new SecondRenaissanceClientCapabilities(false, 0, 0, SecondRenaissanceCapability.None);

        internal SecondRenaissanceClientCapabilities(
            bool isNegotiated,
            ushort protocolVersion,
            ulong advertisedCapabilities,
            SecondRenaissanceCapability supportedCapabilities
        )
        {
            IsNegotiated = isNegotiated;
            ProtocolVersion = protocolVersion;
            AdvertisedCapabilities = advertisedCapabilities;
            SupportedCapabilities = supportedCapabilities;
        }

        public bool IsNegotiated { get; }
        public ushort ProtocolVersion { get; }
        public ulong AdvertisedCapabilities { get; }
        public SecondRenaissanceCapability SupportedCapabilities { get; }

        public bool Supports(SecondRenaissanceCapability capability)
        {
            return IsNegotiated
                && capability != SecondRenaissanceCapability.None
                && (capability & ~SecondRenaissanceCapabilityNegotiation.SupportedCapabilities) == 0
                && (SupportedCapabilities & capability) == capability;
        }
    }

    public static class SecondRenaissanceCapabilityNegotiation
    {
        public const byte ProtocolExtensionSubcommand = 0xFD;
        public const uint Magic = 0x46535231; // "FSR1"
        public const ushort ProtocolVersion = 1;
        public const int HeaderLength = 12;
        public const int CapabilityPayloadLength = sizeof(ulong);

        public const SecondRenaissanceCapability SupportedCapabilities =
            SecondRenaissanceCapability.ExplicitGumpLayout
            | SecondRenaissanceCapability.ExplicitStaticArtwork
            | SecondRenaissanceCapability.MetadataNegotiation;

        private sealed class ConnectionCapabilities
        {
            public SecondRenaissanceClientCapabilities Value;
        }

        private static readonly ConditionalWeakTable<NetState, ConnectionCapabilities> _ByConnection =
            new ConditionalWeakTable<NetState, ConnectionCapabilities>();

        public static void Initialize()
        {
            if (ProtocolExtensions.GetHandler(ProtocolExtensionSubcommand) != null)
            {
                throw new InvalidOperationException(
                    "Protocol extension subcommand 0xFD is already registered."
                );
            }

            ProtocolExtensions.Register(ProtocolExtensionSubcommand, true, OnHandshake);
        }

        public static SecondRenaissanceClientCapabilities GetCapabilities(NetState state)
        {
            if (state != null && _ByConnection.TryGetValue(state, out var connection))
            {
                return connection.Value;
            }

            return SecondRenaissanceClientCapabilities.NotNegotiated;
        }

        public static bool Supports(NetState state, SecondRenaissanceCapability capability)
        {
            return GetCapabilities(state).Supports(capability);
        }

        private static void OnHandshake(NetState state, PacketReader reader)
        {
            if (!TryParse(reader.Buffer, reader.Size, out var protocolVersion, out var advertised))
            {
                return;
            }

            var supported = (SecondRenaissanceCapability)advertised & SupportedCapabilities;
            var negotiated = new SecondRenaissanceClientCapabilities(
                true,
                protocolVersion,
                advertised,
                supported
            );

            _ByConnection.GetValue(state, _ => new ConnectionCapabilities()).Value = negotiated;

            SecondRenaissanceMetadataNegotiation.OnCapabilities(
                state,
                (supported & SecondRenaissanceCapability.MetadataNegotiation) != 0
            );

            if ((supported & SecondRenaissanceCapability.ExplicitStaticArtwork) != 0)
            {
                SecondRenaissanceWorldAssets.Resynchronize(state);
            }
        }

        internal static bool TryParse(
            byte[] packet,
            int size,
            out ushort protocolVersion,
            out ulong advertisedCapabilities
        )
        {
            protocolVersion = 0;
            advertisedCapabilities = 0;

            if (
                packet == null
                || size < HeaderLength + CapabilityPayloadLength
                || size > packet.Length
                || packet[0] != 0xF0
                || ReadUInt16(packet, 1) != size
                || packet[3] != ProtocolExtensionSubcommand
                || ReadUInt32(packet, 4) != Magic
            )
            {
                return false;
            }

            protocolVersion = ReadUInt16(packet, 8);
            ushort payloadLength = ReadUInt16(packet, 10);

            if (
                protocolVersion != ProtocolVersion
                || payloadLength != size - HeaderLength
                || payloadLength < CapabilityPayloadLength
            )
            {
                protocolVersion = 0;
                return false;
            }

            advertisedCapabilities = ReadUInt64(packet, HeaderLength);
            return true;
        }

        private static ushort ReadUInt16(byte[] packet, int offset)
        {
            return (ushort)((packet[offset] << 8) | packet[offset + 1]);
        }

        private static uint ReadUInt32(byte[] packet, int offset)
        {
            return (uint)(
                (packet[offset] << 24)
                | (packet[offset + 1] << 16)
                | (packet[offset + 2] << 8)
                | packet[offset + 3]
            );
        }

        private static ulong ReadUInt64(byte[] packet, int offset)
        {
            return ((ulong)ReadUInt32(packet, offset) << 32) | ReadUInt32(packet, offset + 4);
        }
    }
}

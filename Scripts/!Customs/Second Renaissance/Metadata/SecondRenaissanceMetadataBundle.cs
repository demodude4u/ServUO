// SPDX-License-Identifier: BSD-2-Clause

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Server.SecondRenaissance
{
    public enum SecondRenaissanceMetadataReadiness : byte { ArtworkOnly, WorldReady, Deprecated }
    public enum SecondRenaissanceArticlePolicy : byte { None, A, An, The }
    public enum SecondRenaissanceBridgeMode : byte { None, HalfHeight }
    public enum SecondRenaissanceHousingPolicy : byte { None, Allowed, Restricted, Disallowed }

    [Flags]
    public enum SecondRenaissanceStaticMetadataFlags : ulong
    {
        None = 0, Surface = 1UL << 0, Impassable = 1UL << 1, Wet = 1UL << 2,
        Wall = 1UL << 3, Roof = 1UL << 4, Door = 1UL << 5, Window = 1UL << 6,
        BlocksLineOfSight = 1UL << 7, NoDiagonal = 1UL << 8, Stackable = 1UL << 9,
        Container = 1UL << 10, Wearable = 1UL << 11, Damaging = 1UL << 12,
        PartialHue = 1UL << 13, Translucent = 1UL << 14, Transparent = 1UL << 15,
        Foliage = 1UL << 16, Background = 1UL << 17, Internal = 1UL << 18
    }

    public sealed class SecondRenaissanceStaticMetadata
    {
        public SecondRenaissanceAssetReference Reference { get; internal set; }
        public ushort RecordVersion { get; internal set; }
        public SecondRenaissanceMetadataReadiness Readiness { get; internal set; }
        public string LocalizationKey { get; internal set; }
        public string PluralLocalizationKey { get; internal set; }
        public string EditorLabel { get; internal set; }
        public SecondRenaissanceArticlePolicy ArticlePolicy { get; internal set; }
        public ushort Height { get; internal set; }
        public ushort CollisionHeight { get; internal set; }
        public SecondRenaissanceBridgeMode BridgeMode { get; internal set; }
        public SecondRenaissanceStaticMetadataFlags Flags { get; internal set; }
        public uint BaseWeightHundredths { get; internal set; }
        public ushort EquipLayer { get; internal set; }
        public SecondRenaissanceHousingPolicy HousingPolicy { get; internal set; }
        public string AnimationProfile { get; internal set; }
        public string LightProfile { get; internal set; }
    }

    // Independent .NET Framework reader for the canonical SRMETA 1.0 / schema 1.x bundle.
    // It is intentionally not connected to SRItem or any live gameplay system in Stage 2A.
    public sealed class SecondRenaissanceMetadataBundle
    {
        private const int HeaderSize = 96, RecordSize = 56, MaximumCount = 1000000, MaximumStringBytes = 1048576;
        private const ulong KnownFlags = (1UL << 19) - 1;
        private const ulong WorldBehaviorFlags = (1UL << 13) - 1;
        private static readonly byte[] Magic = { 0x53, 0x52, 0x4D, 0x45, 0x54, 0x41, 0, 0 };
        private static readonly UTF8Encoding StrictUtf8 = new UTF8Encoding(false, true);
        private readonly Dictionary<SecondRenaissanceAssetReference, SecondRenaissanceStaticMetadata> _records;

        private SecondRenaissanceMetadataBundle(uint revision, byte[] hash,
            Dictionary<SecondRenaissanceAssetReference, SecondRenaissanceStaticMetadata> records)
        {
            DatasetRevision = revision; DatasetHash = hash; _records = records;
        }

        public uint DatasetRevision { get; private set; }
        public byte[] DatasetHash { get; private set; }
        public IEnumerable<SecondRenaissanceStaticMetadata> Records { get { return _records.Values; } }

        public bool TryGet(SecondRenaissanceAssetReference reference, out SecondRenaissanceStaticMetadata metadata)
        {
            return _records.TryGetValue(reference, out metadata);
        }

        public static SecondRenaissanceMetadataBundle Read(byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException("bytes");
            if (bytes.Length < HeaderSize || !EqualRange(bytes, 0, Magic)) throw Invalid("Invalid metadata magic/header.");
            if (U16(bytes, 8) != 1) throw Invalid("Unsupported metadata format major version.");
            if (U16(bytes, 12) != 1) throw Invalid("Unsupported metadata schema major version.");
            if (U64(bytes, 20) != 0) throw Invalid("Unknown required metadata features.");
            uint count = U32(bytes, 28), staticCount = U32(bytes, 32), recordsOffset = U32(bytes, 36);
            uint stringsOffset = U32(bytes, 40), stringsLength = U32(bytes, 44), declaredLength = U32(bytes, 48);
            if (count > MaximumCount || staticCount != count || recordsOffset != HeaderSize || declaredLength != bytes.Length ||
                stringsOffset != HeaderSize + (ulong)count * RecordSize || stringsOffset > declaredLength ||
                stringsLength != declaredLength - stringsOffset)
                throw Invalid("Invalid metadata counts, offsets, or lengths.");
            for (int i = 52; i < 64; i++) if (bytes[i] != 0) throw Invalid("Nonzero reserved header bytes.");
            byte[] storedHash = new byte[32]; Buffer.BlockCopy(bytes, 64, storedHash, 0, 32);
            if (!FixedEquals(storedHash, ComputeHash(bytes))) throw Invalid("Metadata integrity hash mismatch.");

            string[] strings = ReadStrings(bytes, checked((int)stringsOffset), checked((int)stringsLength));
            var records = new Dictionary<SecondRenaissanceAssetReference, SecondRenaissanceStaticMetadata>();
            ulong previousKey = 0;
            for (int i = 0, offset = HeaderSize; i < count; i++, offset += RecordSize)
            {
                if (U32(bytes, offset) != RecordSize || U16(bytes, offset + 4) != 1 || U16(bytes, offset + 34) != 0)
                    throw Invalid("Unsupported record version/length or nonzero reserved field.");
                var reference = new SecondRenaissanceAssetReference(
                    (SecondRenaissanceAssetNamespace)bytes[offset + 6],
                    (SecondRenaissanceAssetType)bytes[offset + 7], U32(bytes, offset + 8));
                var record = new SecondRenaissanceStaticMetadata
                {
                    Reference = reference, RecordVersion = U16(bytes, offset + 4),
                    Readiness = (SecondRenaissanceMetadataReadiness)bytes[offset + 12],
                    ArticlePolicy = (SecondRenaissanceArticlePolicy)bytes[offset + 13],
                    BridgeMode = (SecondRenaissanceBridgeMode)bytes[offset + 14],
                    HousingPolicy = (SecondRenaissanceHousingPolicy)bytes[offset + 15],
                    Flags = (SecondRenaissanceStaticMetadataFlags)U64(bytes, offset + 16),
                    Height = U16(bytes, offset + 24), CollisionHeight = U16(bytes, offset + 26),
                    BaseWeightHundredths = U32(bytes, offset + 28), EquipLayer = U16(bytes, offset + 32),
                    LocalizationKey = StringAt(strings, U32(bytes, offset + 36), true),
                    PluralLocalizationKey = StringAt(strings, U32(bytes, offset + 40), true),
                    EditorLabel = StringAt(strings, U32(bytes, offset + 44), true),
                    AnimationProfile = StringAt(strings, U32(bytes, offset + 48), true),
                    LightProfile = StringAt(strings, U32(bytes, offset + 52), true)
                };
                Validate(record);
                ulong key = ((ulong)reference.Namespace << 56) | ((ulong)reference.Type << 48) | reference.LogicalId;
                if ((i > 0 && key <= previousKey) || records.ContainsKey(reference)) throw Invalid("Unsorted or duplicate identity.");
                records.Add(reference, record); previousKey = key;
            }
            return new SecondRenaissanceMetadataBundle(U32(bytes, 16), storedHash, records);
        }

        private static string[] ReadStrings(byte[] bytes, int offset, int length)
        {
            int end = checked(offset + length);
            if (length < 4) throw Invalid("Truncated string table.");
            uint count = U32(bytes, offset); offset += 4;
            if (count > MaximumCount) throw Invalid("String count exceeds the format limit.");
            var strings = new string[count]; string previous = null;
            for (int i = 0; i < count; i++)
            {
                if (offset > end - 4) throw Invalid("Truncated string length.");
                uint lengthBytes = U32(bytes, offset); offset += 4;
                if (lengthBytes == 0 || lengthBytes > MaximumStringBytes || lengthBytes > end - offset)
                    throw Invalid("Invalid or truncated metadata string.");
                string value;
                try { value = StrictUtf8.GetString(bytes, offset, (int)lengthBytes); }
                catch (DecoderFallbackException e) { throw new InvalidDataException("Malformed UTF-8 string.", e); }
                if (!value.IsNormalized(NormalizationForm.FormC) || (previous != null && StringComparer.Ordinal.Compare(previous, value) >= 0))
                    throw Invalid("String table is not unique, normalized, and ordinally sorted.");
                strings[i] = value; previous = value; offset += (int)lengthBytes;
            }
            if (offset != end) throw Invalid("Unexpected trailing string-table bytes.");
            return strings;
        }

        private static void Validate(SecondRenaissanceStaticMetadata r)
        {
            if (r.Reference.Namespace != SecondRenaissanceAssetNamespace.SecondRenaissance || r.Reference.Type != SecondRenaissanceAssetType.Static)
                throw Invalid("Only SecondRenaissance:Static identities are supported.");
            if (!Enum.IsDefined(typeof(SecondRenaissanceMetadataReadiness), r.Readiness) ||
                !Enum.IsDefined(typeof(SecondRenaissanceArticlePolicy), r.ArticlePolicy) ||
                !Enum.IsDefined(typeof(SecondRenaissanceBridgeMode), r.BridgeMode) ||
                !Enum.IsDefined(typeof(SecondRenaissanceHousingPolicy), r.HousingPolicy)) throw Invalid("Invalid enum value.");
            ulong flags = (ulong)r.Flags;
            if ((flags & ~KnownFlags) != 0) throw Invalid("Unknown metadata flags.");
            if (r.Readiness != SecondRenaissanceMetadataReadiness.ArtworkOnly && string.IsNullOrWhiteSpace(r.LocalizationKey))
                throw Invalid("Missing localization key.");
            if ((r.Flags & SecondRenaissanceStaticMetadataFlags.Transparent) != 0 &&
                (r.Flags & SecondRenaissanceStaticMetadataFlags.Translucent) != 0) throw Invalid("Conflicting transparency flags.");
            if (r.BridgeMode != SecondRenaissanceBridgeMode.None && (r.Flags & SecondRenaissanceStaticMetadataFlags.Surface) == 0)
                throw Invalid("A bridge must be a surface.");
            bool wearable = (r.Flags & SecondRenaissanceStaticMetadataFlags.Wearable) != 0;
            if (wearable != (r.EquipLayer != 0)) throw Invalid("Wearable and equip-layer declarations disagree.");
            if (r.Readiness == SecondRenaissanceMetadataReadiness.ArtworkOnly &&
                ((flags & WorldBehaviorFlags) != 0 || r.Height != 0 || r.CollisionHeight != 0 || r.BaseWeightHundredths != 0 ||
                 r.EquipLayer != 0 || r.BridgeMode != SecondRenaissanceBridgeMode.None || r.HousingPolicy != SecondRenaissanceHousingPolicy.None))
                throw Invalid("ArtworkOnly records cannot assert world/gameplay behavior.");
        }

        private static string StringAt(string[] strings, uint index, bool optional)
        {
            if (index == uint.MaxValue) { if (optional) return null; throw Invalid("Required string reference is null."); }
            if (index >= strings.Length) throw Invalid("String reference is out of range.");
            return strings[index];
        }
        private static byte[] ComputeHash(byte[] bytes)
        {
            using (SHA256 sha = SHA256.Create())
            {
                sha.TransformBlock(bytes, 0, 64, null, 0);
                sha.TransformFinalBlock(bytes, HeaderSize, bytes.Length - HeaderSize);
                return sha.Hash;
            }
        }
        private static bool FixedEquals(byte[] a, byte[] b)
        {
            int difference = a.Length ^ b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++) difference |= a[i] ^ b[i];
            return difference == 0;
        }
        private static bool EqualRange(byte[] source, int offset, byte[] expected)
        {
            for (int i = 0; i < expected.Length; i++) if (source[offset + i] != expected[i]) return false;
            return true;
        }
        private static ushort U16(byte[] b, int o) { return (ushort)(b[o] | b[o + 1] << 8); }
        private static uint U32(byte[] b, int o) { return (uint)(b[o] | b[o + 1] << 8 | b[o + 2] << 16 | b[o + 3] << 24); }
        private static ulong U64(byte[] b, int o) { return U32(b, o) | ((ulong)U32(b, o + 4) << 32); }
        private static InvalidDataException Invalid(string message) { return new InvalidDataException(message); }
    }
}

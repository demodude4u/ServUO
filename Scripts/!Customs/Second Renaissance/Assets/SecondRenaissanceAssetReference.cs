using System;

namespace Server.SecondRenaissance
{
    public enum SecondRenaissanceAssetNamespace : byte
    {
        Official = 0,
        SecondRenaissance = 1
    }

    public enum SecondRenaissanceAssetType : byte
    {
        Gump = 0,
        Static = 1
    }

    public readonly struct SecondRenaissanceAssetReference : IEquatable<SecondRenaissanceAssetReference>
    {
        public SecondRenaissanceAssetReference(
            SecondRenaissanceAssetNamespace assetNamespace,
            SecondRenaissanceAssetType assetType,
            uint logicalId
        )
        {
            Namespace = assetNamespace;
            Type = assetType;
            LogicalId = logicalId;
        }

        public SecondRenaissanceAssetNamespace Namespace { get; }
        public SecondRenaissanceAssetType Type { get; }
        public uint LogicalId { get; }

        public static SecondRenaissanceAssetReference SecondRenaissanceStatic(uint logicalId)
        {
            return new SecondRenaissanceAssetReference(
                SecondRenaissanceAssetNamespace.SecondRenaissance,
                SecondRenaissanceAssetType.Static,
                logicalId
            );
        }

        public bool Equals(SecondRenaissanceAssetReference other)
        {
            return Namespace == other.Namespace && Type == other.Type && LogicalId == other.LogicalId;
        }

        public override bool Equals(object obj)
        {
            return obj is SecondRenaissanceAssetReference other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)Namespace;
                hash = (hash * 397) ^ (int)Type;
                hash = (hash * 397) ^ (int)LogicalId;
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{Namespace}:{Type}:{LogicalId}";
        }
    }
}

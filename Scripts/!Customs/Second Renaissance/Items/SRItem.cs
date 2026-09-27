using Server.Network;

namespace Server.SecondRenaissance
{
    /// <summary>
    /// A persistent, inert world item whose artwork belongs to the explicit
    /// Second Renaissance Static namespace. The fixed official bootstrap graphic
    /// is only a compatibility carrier for clients that do not negotiate SR support.
    /// </summary>
    public sealed class SRItem : Item
    {
        public const int OfficialBootstrapItemId = 0x0001; // Official "NO DRAW"

        private SecondRenaissanceAssetReference _ArtworkReference;

        [Constructable(AccessLevel.GameMaster)]
        public SRItem(uint logicalId)
            : base(OfficialBootstrapItemId)
        {
            _ArtworkReference = SecondRenaissanceAssetReference.SecondRenaissanceStatic(logicalId);
            Movable = false;
        }

        public SRItem(Serial serial)
            : base(serial)
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public uint SecondRenaissanceLogicalId
        {
            get => _ArtworkReference.LogicalId;
            set
            {
                if (_ArtworkReference.LogicalId == value)
                {
                    return;
                }

                _ArtworkReference = SecondRenaissanceAssetReference.SecondRenaissanceStatic(value);
                InvalidateProperties();
                Delta(ItemDelta.Update);
            }
        }

        public SecondRenaissanceAssetReference ArtworkReference => _ArtworkReference;

        /// <summary>Resolves this item's explicit SR identity through the centralized WorldReady gate.</summary>
        public bool TryGetWorldReadyMetadata(out SecondRenaissanceStaticMetadataView metadata)
        {
            return SecondRenaissanceMetadataConsumption.TryGetWorldReady(_ArtworkReference, out metadata);
        }

        public override string DefaultName => $"Second Renaissance static {_ArtworkReference.LogicalId}";

        public override void SendInfoTo(NetState state, bool sendOplPacket)
        {
            base.SendInfoTo(state, sendOplPacket);
            SecondRenaissanceWorldAssets.SendIdentity(state, this);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
            writer.Write((byte)_ArtworkReference.Namespace);
            writer.Write((byte)_ArtworkReference.Type);
            writer.Write(_ArtworkReference.LogicalId);
            writer.Write((ushort)SecondRenaissanceWorldAssetPacket.MetadataVersion);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            var assetNamespace = (SecondRenaissanceAssetNamespace)reader.ReadByte();
            var assetType = (SecondRenaissanceAssetType)reader.ReadByte();
            uint logicalId = reader.ReadUInt();
            ushort metadataVersion = reader.ReadUShort();

            if (
                version != 0
                || assetNamespace != SecondRenaissanceAssetNamespace.SecondRenaissance
                || assetType != SecondRenaissanceAssetType.Static
                || metadataVersion != SecondRenaissanceWorldAssetPacket.MetadataVersion
            )
            {
                Delete();
                return;
            }

            _ArtworkReference = SecondRenaissanceAssetReference.SecondRenaissanceStatic(logicalId);
            ItemID = OfficialBootstrapItemId;
            Movable = false;
        }
    }
}

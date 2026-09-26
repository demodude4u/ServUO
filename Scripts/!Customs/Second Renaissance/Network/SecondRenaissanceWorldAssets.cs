using Server.Items;
using Server.Misc;
using Server.Network;
using System;

namespace Server.SecondRenaissance
{
    public enum SecondRenaissanceWorldAssetOperation : byte
    {
        Set = 1,
        Clear = 2
    }

    public sealed class SecondRenaissanceWorldAssetPacket : ProtocolExtension
    {
        public const byte ProtocolExtensionSubcommand = 0xFC;
        public const uint Magic = 0x46535232; // "FSR2"
        public const ushort ProtocolVersion = 1;
        public const ushort MetadataVersion = 1;
        public const ushort PayloadLength = 12;

        public SecondRenaissanceWorldAssetPacket(
            Serial serial,
            SecondRenaissanceWorldAssetOperation operation,
            SecondRenaissanceAssetReference artwork
        )
            : base(ProtocolExtensionSubcommand, 20)
        {
            m_Stream.Write(Magic);
            m_Stream.Write(ProtocolVersion);
            m_Stream.Write(PayloadLength);
            m_Stream.Write((byte)operation);
            m_Stream.Write((byte)artwork.Namespace);
            m_Stream.Write((byte)artwork.Type);
            m_Stream.Write((byte)MetadataVersion);
            m_Stream.Write(serial);
            m_Stream.Write(artwork.LogicalId);
        }
    }

    public static class SecondRenaissanceWorldAssets
    {
        public static bool CanSend(NetState state)
        {
            return SecondRenaissanceCapabilityNegotiation.Supports(
                state,
                SecondRenaissanceCapability.ExplicitStaticArtwork
            );
        }

        public static void SendIdentity(NetState state, SRItem item)
        {
            if (state == null || item == null || item.Deleted || !CanSend(state))
            {
                return;
            }

            state.Send(
                new SecondRenaissanceWorldAssetPacket(
                    item.Serial,
                    SecondRenaissanceWorldAssetOperation.Set,
                    item.ArtworkReference
                )
            );
        }

        public static void Resynchronize(NetState state)
        {
            Mobile mobile = state?.Mobile;

            if (mobile == null || mobile.Deleted || mobile.Map == null || !CanSend(state))
            {
                return;
            }

            IPooledEnumerable<Item> items = mobile.GetItemsInRange(state.UpdateRange);

            try
            {
                foreach (Item item in items)
                {
                    if (
                        item is SRItem srItem
                        && !srItem.Deleted
                        && mobile.CanSee(srItem)
                        && mobile.InUpdateRange(srItem.GetWorldLocation())
                    )
                    {
                        // Re-send the ordinary bootstrap packet first so the overlay can never
                        // arrive before the client has created the world object.
                        srItem.SendInfoTo(state);
                    }
                }
            }
            finally
            {
                items.Free();
            }
        }
    }
}

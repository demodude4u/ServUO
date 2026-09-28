// SPDX-License-Identifier: BSD-2-Clause

namespace Server.SecondRenaissance
{
    /// <summary>Stage 8 authoritative movement semantics for eligible SRItems.</summary>
    public static class SecondRenaissanceMovementSemantics
    {
        public static bool TryGetImpassableCollision(Item item, out int collisionHeight)
        {
            collisionHeight = 0;

            SRItem srItem = item as SRItem;
            if (srItem == null
                || !srItem.TryGetWorldReadyMetadata(out SecondRenaissanceStaticMetadataView metadata)
                || (metadata.Flags & SecondRenaissanceStaticMetadataFlags.Impassable) == 0
                || metadata.CollisionHeight == 0)
            {
                return false;
            }

            collisionHeight = metadata.CollisionHeight;
            return true;
        }
    }
}

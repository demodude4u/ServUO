// SPDX-License-Identifier: BSD-2-Clause

using Server.Commands;
using Server.Targeting;

namespace Server.SecondRenaissance
{
    public static class SRItemMetadata
    {
        public static void Initialize()
        {
            CommandSystem.Register("SRItemMetadata", AccessLevel.Administrator, OnCommand);
        }

        [Usage("SRItemMetadata")]
        [Description("Targets an SRItem and reports its read-only metadata binding eligibility.")]
        private static void OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Target a Second Renaissance item.");
            e.Mobile.Target = new InspectTarget();
        }

        private sealed class InspectTarget : Target
        {
            public InspectTarget() : base(-1, false, TargetFlags.None) { }

            protected override void OnTarget(Mobile from, object targeted)
            {
                SRItem item = targeted as SRItem;
                if (item == null)
                {
                    from.SendMessage("That is not an SRItem.");
                    return;
                }

                SecondRenaissanceStaticMetadataView raw;
                bool found = SecondRenaissanceMetadataRuntime.Current.TryGet(item.ArtworkReference, out raw);
                SecondRenaissanceStaticMetadataView worldReady;
                bool eligible = item.TryGetWorldReadyMetadata(out worldReady);
                from.SendMessage("Second Renaissance Item Metadata");
                from.SendMessage("Logical ID: {0}", item.SecondRenaissanceLogicalId);
                from.SendMessage("Identity: {0}", item.ArtworkReference);
                from.SendMessage("Carrier ItemID: 0x{0:X4} (not metadata identity)", item.ItemID);
                from.SendMessage("Record Found: {0}", found ? "Yes" : "No");
                from.SendMessage("Readiness: {0}", found ? raw.Readiness.ToString() : "Missing");
                from.SendMessage("WorldReady: {0}", eligible ? "Yes" : "No");
                from.SendMessage("Metadata Impassable: {0}", found && (raw.Flags & SecondRenaissanceStaticMetadataFlags.Impassable) != 0 ? "Yes" : "No");
                from.SendMessage("Metadata Collision Height: {0}", found ? raw.CollisionHeight.ToString() : "(none)");
                from.SendMessage("Applied Fields: {0}", SecondRenaissanceMovementSemantics.TryGetImpassableCollision(item, out _) ? "Impassable" : "None");
            }
        }
    }
}

using Server.Commands;
using Server.Mobiles;

namespace Server.Items
{
    public class OrcEnmityTestGate : Item
    {
        private const int GateHue = 0x48E;

        public static void Initialize()
        {
            CommandSystem.Register("OrcEnmityTestGate", AccessLevel.GameMaster, ToggleGate_OnCommand);
        }

        [Usage("OrcEnmityTestGate")]
        [Description("Places or removes an Orc enmity testing gate at your location.")]
        private static void ToggleGate_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from.Map == null || from.Map == Map.Internal)
            {
                from.SendMessage("The test gate cannot be placed here.");
                return;
            }

            OrcEnmityTestGate existing = null;
            IPooledEnumerable<Item> items = from.Map.GetItemsInRange(from.Location, 0);

            foreach (Item item in items)
            {
                if (item is OrcEnmityTestGate gate && item.Z == from.Z)
                {
                    existing = gate;
                    break;
                }
            }

            items.Free();

            if (existing != null)
            {
                existing.Delete();
                from.SendMessage("The Orc enmity testing gate has been removed.");
            }
            else
            {
                OrcEnmityTestGate gate = new OrcEnmityTestGate();
                gate.MoveToWorld(from.Location, from.Map);
                from.SendMessage("An Orc enmity testing gate has been placed at your location.");
            }
        }

        [Constructable]
        public OrcEnmityTestGate()
            : base(0x0F6C)
        {
            Name = "an orc enmity cleansing gate";
            Hue = GateHue;
            Movable = false;
            Light = LightType.Circle300;
        }

        public OrcEnmityTestGate(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(500446); // That is too far away.
                return;
            }

            if (from is PlayerMobile player)
            {
                player.ClearEnemyOfOrcs(true);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }
    }
}

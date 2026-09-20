using Server.CharacterCreator;
using Server.Commands;

namespace Server.Items
{
    public class CharacterCreatorTestGate : Item
    {
        private const int GateHue = 0x55F;

        public static void Initialize()
        {
            CommandSystem.Register("CCTestGate", AccessLevel.GameMaster, ToggleGate_OnCommand);
        }

        [Usage("CCTestGate")]
        [Description("Places or removes a Character Creator testing gate at your location.")]
        private static void ToggleGate_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from.Map == null || from.Map == Map.Internal)
            {
                from.SendMessage("The test gate cannot be placed here.");
                return;
            }

            CharacterCreatorTestGate existing = null;
            IPooledEnumerable<Item> items = from.Map.GetItemsInRange(from.Location, 0);

            foreach (Item item in items)
            {
                if (item is CharacterCreatorTestGate gate && item.Z == from.Z)
                {
                    existing = gate;
                    break;
                }
            }

            items.Free();

            if (existing != null)
            {
                existing.Delete();
                from.SendMessage("The Character Creator testing gate has been removed.");
            }
            else
            {
                CharacterCreatorTestGate gate = new CharacterCreatorTestGate();
                gate.MoveToWorld(from.Location, from.Map);
                from.SendMessage("A Character Creator testing gate has been placed at your location.");
            }
        }

        [Constructable]
        public CharacterCreatorTestGate()
            : base(0x0F6C)
        {
            Name = "a character creator testing gate";
            Hue = GateHue;
            Movable = false;
            Light = LightType.Circle300;
        }

        public CharacterCreatorTestGate(Serial serial)
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

            CharacterCreatorSystem.ToggleCharacterCreator(from);
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

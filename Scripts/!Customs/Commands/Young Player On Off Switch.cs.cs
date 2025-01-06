using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace YourServerNamespace
{
    public class NewPlayerToggleItem : Item
    {
        public override string DefaultName { get { return "New Player Toggle"; } }

        [Constructable]
        public NewPlayerToggleItem() : base(0x1093)
        {
            Hue = 2389; // Set the desired hue for the toggle item
        }

        public NewPlayerToggleItem(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendMessage("The toggle item must be in your backpack to use it.");
                return;
            }

            if (!(from is PlayerMobile player))
                return;

            player.Young = !player.Young; // Toggle the new player status

            if (player.Young)
                from.SendMessage("You have toggled ON the new player status.");
            else
                from.SendMessage("You have toggled OFF the new player status.");
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}

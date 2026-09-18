using Server.Mobiles;
using System;

namespace Server.Items
{
    public class HeavyArmor : Item
    {
        public HeavyArmor()
        {

        }
        public override void OnAdded(object parent)
        {
            if (parent is Backpack)
            {
                Backpack pack = parent as Backpack;

                if (pack.Parent != null)
                {
                    if (pack.Parent is PlayerMobile)
                    {
                        PlayerMobile pm = pack.Parent as PlayerMobile;


                        Bag bag = new Bag();
                        bag.Name = "Bag of Heavy Armor";
                        bag.Hue = 11;
                        bag.LootType = LootType.Regular;

                        switch (pm.Race.RaceID)
                        {
                            default://Human
                                {
                                    bag.DropItem(new RingmailChest());
                                    bag.DropItem(new RingmailArms());
                                    bag.DropItem(new RingmailGloves());
                                    bag.DropItem(new PlateGorget());
                                    bag.DropItem(new RingmailLegs());
                                    break;
                                }
                            case 1://Elf
                                {
                                    bag.DropItem(new RingmailChest());
                                    bag.DropItem(new RingmailArms());
                                    bag.DropItem(new RingmailGloves());
                                    bag.DropItem(new PlateGorget());
                                    bag.DropItem(new RingmailLegs());
                                    break;
                                }
                            case 2://Gargoyle
                                {
                                    bag.DropItem(new GargishPlateArms());
                                    bag.DropItem(new GargishPlateChest());
                                    bag.DropItem(new GargishPlateKilt());
                                    bag.DropItem(new GargishPlateLegs());
                                    bag.DropItem(new GargishLeatherWingArmor());
                                    break;
                                }
                            case 3://Orc
                                {
                                    bag.DropItem(new RingmailChest());
                                    bag.DropItem(new RingmailArms());
                                    bag.DropItem(new RingmailGloves());
                                    bag.DropItem(new PlateGorget());
                                    bag.DropItem(new RingmailLegs());
                                    break;
                                }

                        }

                        pm.AddToBackpack(bag);

                        this.Delete();
                    }
                }
            }
        }

        public HeavyArmor(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
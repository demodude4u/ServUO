using Server.Mobiles;
using System;

namespace Server.Items
{
    public class MediumArmor : Item
    {
        public MediumArmor()
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
                        bag.Name = "Bag of Medium Armor";
                        bag.Hue = 11;
                        bag.LootType = LootType.Regular;

                        switch (pm.Race.RaceID)
                        {
                            default://Human
                                {
                                    bag.DropItem(new StuddedChest());
                                    bag.DropItem(new StuddedArms());
                                    bag.DropItem(new StuddedGloves());
                                    bag.DropItem(new StuddedGorget());
                                    bag.DropItem(new StuddedLegs());
                                    break;
                                }
                            case 1://Elf
                                {
                                    bag.DropItem(new StuddedChest());
                                    bag.DropItem(new StuddedArms());
                                    bag.DropItem(new StuddedGloves());
                                    bag.DropItem(new StuddedGorget());
                                    bag.DropItem(new StuddedLegs());
                                    break;
                                }
                            case 2://Gargoyle
                                {
                                    bag.DropItem(new GargishLeatherArms());
                                    bag.DropItem(new GargishLeatherChest());
                                    bag.DropItem(new GargishLeatherKilt());
                                    bag.DropItem(new GargishLeatherLegs());
                                    bag.DropItem(new GargishClothWingArmor());
                                    break;
                                }
                            case 3://Orc
                                {
                                    bag.DropItem(new StuddedChest());
                                    bag.DropItem(new StuddedArms());
                                    bag.DropItem(new StuddedGloves());
                                    bag.DropItem(new StuddedGorget());
                                    bag.DropItem(new StuddedLegs());
                                    break;
                                }

                        }

                        pm.AddToBackpack(bag);

                        this.Delete();
                    }
                }
            }
        }

        public MediumArmor(Serial serial)
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
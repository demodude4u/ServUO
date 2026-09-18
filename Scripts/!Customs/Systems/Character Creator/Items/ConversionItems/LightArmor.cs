using Server.Mobiles;
using System;

namespace Server.Items
{
    public class LightArmor : Item
    {
        public LightArmor()
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
                        bag.Name = "Bag of Light Armor";
                        bag.Hue = 11;
                        bag.LootType = LootType.Regular;

                        switch (pm.Race.RaceID)
                        {
                            default://Human
                                {
                                    bag.DropItem(new LeatherChest());
                                    bag.DropItem(new LeatherArms());
                                    bag.DropItem(new LeatherGloves());
                                    bag.DropItem(new LeatherGorget());
                                    bag.DropItem(new LeatherLegs());
                                    break;
                                }
                            case 1://Elf
                                {
                                    bag.DropItem(new LeatherChest());
                                    bag.DropItem(new LeatherArms());
                                    bag.DropItem(new LeatherGloves());
                                    bag.DropItem(new LeatherGorget());
                                    bag.DropItem(new LeatherLegs());
                                    break;
                                }
                            case 2://Gargoyle
                                {
                                    bag.DropItem(new GargishClothArms());
                                    bag.DropItem(new GargishClothChest());
                                    bag.DropItem(new GargishClothKilt());
                                    bag.DropItem(new GargishClothLegs());
                                    break;
                                }
                            case 3://Orc
                                {
                                    bag.DropItem(new LeatherChest());
                                    bag.DropItem(new LeatherArms());
                                    bag.DropItem(new LeatherGloves());
                                    bag.DropItem(new LeatherGorget());
                                    bag.DropItem(new LeatherLegs());
                                    break;
                                }

                        }

                        pm.AddToBackpack(bag);

                        this.Delete();
                    }
                }
            }
        }

        public LightArmor(Serial serial)
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
using Server.Mobiles;
using System;

namespace Server.Items
{
    public class Axes : Item
    {
        public Axes()
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

                        Item item;


                        switch (pm.Race.RaceID)
                        {
                            default://Human
                                {

                                    item = new Axe();
                                    break;
                                }
                            case 1://Elf
                                {

                                    item = new Axe();
                                    break;
                                }
                            case 2://Gargoyle
                                {

                                    item = new GargishAxe();
                                    break;
                                }
                            case 3://Orc
                                {

                                    item = new Axe();
                                    break;
                                }

                        }

                        pm.Backpack.DropItem(item);

                        this.Delete();
                    }
                }
            }
        }

        public Axes(Serial serial)
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
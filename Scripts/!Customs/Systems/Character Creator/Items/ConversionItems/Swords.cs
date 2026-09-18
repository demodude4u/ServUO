using Server.Mobiles;
using System;

namespace Server.Items
{
    public class Swords : Item
    {
        public Swords()
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

                                    item = new Broadsword();
                                    break;
                                }
                            case 1://Elf
                                {

                                    item = new Broadsword();
                                    break;
                                }
                            case 2://Gargoyle
                                {

                                    item = new GargishKatana();
                                    break;
                                }
                            case 3://Orc
                                {

                                    item = new Broadsword();
                                    break;
                                }

                        }

                        pm.Backpack.DropItem(item);

                        this.Delete();
                    }
                }
            }
        }

        public Swords(Serial serial)
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
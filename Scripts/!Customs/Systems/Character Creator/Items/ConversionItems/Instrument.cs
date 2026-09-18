using Server.Mobiles;
using System;

namespace Server.Items
{
    public class Instrument : Item
    {
        public Instrument()
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

                        switch(Utility.Random(4))
                        {
                            default:
                                {
                                    item = new Lute();
                                    break;
                                }
                            case 0:
                                {
                                    item = new Drums();
                                    break;
                                }
                            case 1:
                                {
                                    item = new LapHarp();
                                    break;
                                }
                            case 2:
                                {
                                    item = new Lute();
                                    break;
                                }
                            case 3:
                                {
                                    item = new Tambourine();
                                    break;
                                }
                        }

                        pm.Backpack.DropItem(item);

                        this.Delete();
                    }
                }
            }
        }

        public Instrument(Serial serial)
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
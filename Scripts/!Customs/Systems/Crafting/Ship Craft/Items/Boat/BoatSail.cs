using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Items
{
    public class BoatSail : Item
    {
        [Constructable]
        public BoatSail() : base(0x123)
        {
            this.Name = "Boat Sail";
            this.Hue = 123;
        }
        public BoatSail(Serial serial) : base(serial) { }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
}

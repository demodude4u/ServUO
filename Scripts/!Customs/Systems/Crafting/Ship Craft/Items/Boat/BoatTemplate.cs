using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Items
{
    public class BoatTemplate : Item
    {
        [Constructable]
        public BoatTemplate() : base(0x123)
        {
            this.Name = "Boat Template";
            this.Hue = 123;
        }
        public BoatTemplate(Serial serial) : base(serial) { }
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

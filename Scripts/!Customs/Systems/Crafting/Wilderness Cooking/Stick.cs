using System;
using Server.Engines.Craft;

namespace Server.Items
{
    public class Stick : BaseTool
    {
        [Constructable]
        public Stick()
            : this(Utility.Random(5,10))
        {
            Hue = 0x21E;
            this.Weight = 1.0;
            this.Name = "stick";
        }

        [Constructable]
        public Stick(int uses)
            : base(uses, 0xF7E)
        {
            this.Weight = 1.0;
        }

        public Stick(Serial serial)
            : base(serial)
        {
        }

        public override CraftSystem CraftSystem
        {
            get
            {
                return DefWildernessCooking.CraftSystem;
            }
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

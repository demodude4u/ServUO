using System;
using Server.Engines.Craft;

namespace Server.Items
{
    [FlipableAttribute(0x0BB5, 0x0BB6)]
    public class ShipCraftTool : BaseTool
    {
        [Constructable]
        public ShipCraftTool()
            : base(0x0BB5)
        {
            this.Weight = 2.0;
        }

        [Constructable]
        public ShipCraftTool(int uses)
            : base(uses, 0x0BB5)
        {
            this.Weight = 2.0;
        }

        public ShipCraftTool(Serial serial)
            : base(serial)
        {
        }

        public override CraftSystem CraftSystem
        {
            get
            {
                return DefShipCraft.CraftSystem;
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

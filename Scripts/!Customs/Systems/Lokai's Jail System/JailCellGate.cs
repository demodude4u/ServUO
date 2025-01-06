using System;
using Server;
using Server.Items;
using Server.Guilds;
using System.Collections;
using System.Reflection;
using Server.Mobiles;

namespace Server.Items
{
    public class JailCellGate : Moongate
    {
        [Constructable]
        public JailCellGate() : base(false) 
		{
			Target = new Point3D(5280, 1182, 0);
			TargetMap = Map.Trammel;
		}

        public JailCellGate(Serial serial) : base(serial) { }

        public override void UseGate(Mobile m)
        {
			if (JailUtility.Convicts.ContainsKey(m.Serial.Value))
			{
				JailStatus js = JailUtility.Convicts[m.Serial.Value];
				js.FredomTime = DateTime.UtcNow + TimeSpan.FromMinutes(js.Minutes);
			}
			base.UseGate(m);
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

            switch (version)
            {
                case 0:
                    {
                        break;
                    }
            }
        }
    }
}
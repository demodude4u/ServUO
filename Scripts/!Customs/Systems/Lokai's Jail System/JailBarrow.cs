using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class JailBarrow : Item
    {
        [Constructable]
        public JailBarrow() : base(41190) { }

        public JailBarrow(Serial serial) : base(serial) { }
		
		public override void OnDoubleClick(Mobile m)
		{
			if (JailUtility.Convicts.ContainsKey(m.Serial.Value))
			{
				JailStatus js = JailUtility.Convicts[m.Serial.Value];
				if (js.FredomTime < DateTime.UtcNow)
				{
					JailUtility.ProcessConvictRelease(m, js);
					m.SendMessage("Your time in Jail has ended!");
				}
				else if (js.CoalCollected >= js.CoalRequired)
				{
					JailUtility.ProcessConvictRelease(m, js);
					m.SendMessage("You have mined enough coal and are released from Jail!");
				}
				else
				{
					int minutes = (int)((TimeSpan)(js.FredomTime - DateTime.UtcNow)).TotalMinutes;
					int coal = js.CoalRequired - js.CoalCollected;
					m.SendMessage("You must wait {0} more minutes or mine {1} more coal.", minutes, coal);
				}
			}
		}

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add("Gather your ore or wait until your time expires, then click here to exit.");
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
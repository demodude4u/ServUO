using System;

namespace Server.Items
{
	public class SouthIcicles : Item
	{
		[Constructable]
		public SouthIcicles() : base( Utility.RandomList( 0x4572 ,0x4573, 0x4574 ))
		{
		}

		public SouthIcicles(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
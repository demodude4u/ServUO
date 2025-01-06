using System;

namespace Server.Items
{
	public class Flowers : Item
	{
		[Constructable]
		public Flowers() : base( Utility.RandomList( 0xCC1, 0x0C83 ,0x0C84, 0x0C86, 0x0C88, 0x0C8A ))
		{
		}

		public Flowers(Serial serial) : base(serial)
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
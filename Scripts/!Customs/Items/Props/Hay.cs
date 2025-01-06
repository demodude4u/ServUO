using System;

namespace Server.Items
{
	public class Hay : Item
	{
		[Constructable]
		public Hay() : base( Utility.RandomList( 0x1036 ,0x1037, 0xF34, 0xF35 ))
		{
		}

		public Hay(Serial serial) : base(serial)
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
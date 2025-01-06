using System;

namespace Server.Items
{
	public class Grasses : Item
	{
		[Constructable]
		public Grasses() : base( Utility.RandomList( 0x0CC6 ,0x0D32, 0x0D33 ))
		{
		}

		public Grasses(Serial serial) : base(serial)
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
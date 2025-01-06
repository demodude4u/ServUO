using System;

namespace Server.Items
{
	public class Hedge : Item
	{
		[Constructable]
		public Hedge() : base( Utility.RandomList( 0x0C8F, 0x0C90 ) )
		{
		}

		public Hedge(Serial serial) : base(serial)
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
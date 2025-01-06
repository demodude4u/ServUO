using System;

namespace Server.Items
{
	public class Leaves : Item
	{
		[Constructable]
		public Leaves() : base( Utility.RandomList( 6943, 6944, 6945, 6946 ))
		{
			Name = "Leaves";
		}

		public Leaves(Serial serial) : base(serial)
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
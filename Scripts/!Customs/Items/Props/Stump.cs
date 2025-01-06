using System;

namespace Server.Items
{
	public class Stump : Item
	{
		[Constructable]
		public Stump() : base( Utility.RandomList( 0x0E57, 0x0E59 ))
		{
			Name = "Stump";
		}

		public Stump(Serial serial) : base(serial)
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
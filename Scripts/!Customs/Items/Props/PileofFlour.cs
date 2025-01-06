using System;

namespace Server.Items
{
	public class PileofFlour : Item
	{
		[Constructable]
		public PileofFlour() : base( Utility.RandomList( 0x187E ,0x187F, 0x1880, 0x1881 ))
		{
			Name = "Pile of flour";
		}

		public PileofFlour(Serial serial) : base(serial)
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
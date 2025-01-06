using System;

namespace Server.Items
{
	public class PileofFish : Item
	{
		[Constructable]
		public PileofFish() : base( Utility.RandomList( 0x0DD6, 0x0DD7, 0x0DD8, 0x0DD9 ))
		{
			Name = "Pile of Fish";
		}

		public PileofFish(Serial serial) : base(serial)
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
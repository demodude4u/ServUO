using System;

namespace Server.Items
{
	public class FruitBush : Item
	{
		[Constructable]
		public FruitBush() : base( Utility.RandomList( 0x0D9E, 0x0DA2, 0x0DA6, 0x0DAA ))
		{
			Name = "Fruit Bush";
		}

		public FruitBush(Serial serial) : base(serial)
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
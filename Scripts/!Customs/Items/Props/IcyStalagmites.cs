using System;

namespace Server.Items
{
	public class IcyStalagmites : Item
	{
		[Constructable]
		public IcyStalagmites() : base( Utility.RandomList( 2272 ,2273, 2276, 2277, 0x08E7, 0x08E9, 0x08EA ))
		{
			Name = "Icy Stalagmites";
			Movable = false;
			Hue = Utility.RandomList(1150, 1151);
		}

		public IcyStalagmites(Serial serial) : base(serial)
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
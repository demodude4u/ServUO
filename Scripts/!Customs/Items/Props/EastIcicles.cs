using System;

namespace Server.Items
{
	public class EastIcicles : Item
	{
		[Constructable]
		public EastIcicles() : base( Utility.RandomList( 0x4575 ,0x4576, 0x4577 ))
		{
		}

		public EastIcicles(Serial serial) : base(serial)
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
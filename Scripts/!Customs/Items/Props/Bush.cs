using System;

namespace Server.Items
{
	public class Bush : Item
	{
		[Constructable]
		public Bush() : base( Utility.RandomList( 0x0D9D, 0x0DA1, 0x0DA5, 0x0DA9 ))
		{
			Name = "Bush";
		}

		public Bush(Serial serial) : base(serial)
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
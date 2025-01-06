using System;

namespace Server.Items
{
	public class WoodCurls : Item
	{
		[Constructable]
		public WoodCurls() : base(0x1038)
		{
			Name = "Wood Curls";
		}

		public WoodCurls(Serial serial) : base(serial)
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
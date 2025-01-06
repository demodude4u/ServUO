using System;

namespace Server.Items
{
	[FlipableAttribute( 0xB3F, 0xB40 )]
	public class Counter : Item
	{
		[Constructable]
		
        public Counter()
            : base(0xB3F)
		{
			Weight = 1.0;
			Name = "Counter";
		}

        public Counter(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
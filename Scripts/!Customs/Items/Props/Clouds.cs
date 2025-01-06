using System;

namespace Server.Items
{
	public class Clouds : Item
	{
		[Constructable]
		public Clouds() : base( Utility.RandomList( 3279, 3282 , 3285, 3292, 3295, 3298, 3301, 3304, 3328, 3331 ))
        {
            this.Name = "Clouds";
            this.Hue = 1150;
		}

		public Clouds(Serial serial) : base(serial)
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
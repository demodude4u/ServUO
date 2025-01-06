using System;

namespace Server.Items
{
	public class Mushrooms : Item
	{
		[Constructable]
		public Mushrooms() : this( Utility.Random( 14 ) )
		{
		}

		[Constructable]
		public Mushrooms( int v ) : base( 0x0D0C )
		{
			if ( v < 0 || v > 7 )
				v = 0;

			ItemID += v;
			Weight = 1.0;
		}

		public Mushrooms(Serial serial) : base(serial)
		{
		}

		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

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
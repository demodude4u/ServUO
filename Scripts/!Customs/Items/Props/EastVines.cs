using System;

namespace Server.Items
{
	public class EastVines : Item
	{
		[Constructable]
		public EastVines() : this( Utility.Random( 4 ) )
		{
		}

		[Constructable]
		public EastVines( int v ) : base( 0x0CEF )
		{
			if ( v < 0 || v > 7 )
				v = 0;

			ItemID += v;
			Weight = 1.0;
		}

		public EastVines(Serial serial) : base(serial)
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
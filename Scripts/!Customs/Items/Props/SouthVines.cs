using System;

namespace Server.Items
{
	public class SouthVines : Item
	{
		[Constructable]
		public SouthVines() : this( Utility.Random( 4 ) )
		{
		}

		[Constructable]
		public SouthVines( int v ) : base( 0x0CEB )
		{
			if ( v < 0 || v > 7 )
				v = 0;

			ItemID += v;
			Weight = 1.0;
		}

		public SouthVines(Serial serial) : base(serial)
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
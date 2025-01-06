using System;
using Server;

namespace Server.Items
{
	public class SmallBedSouthAddon2 : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new SmallBedSouthDeed2(); } }

		[Constructable]
		public SmallBedSouthAddon2()
		{
			AddComponent( new AddonComponent( 2662 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 2664 ), 0, 1, 0 );
		}

		public SmallBedSouthAddon2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SmallBedSouthDeed2 : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new SmallBedSouthAddon2(); } }
		public override int LabelNumber{ get{ return 1044321; } } // small bed (south)

		[Constructable]
		public SmallBedSouthDeed2()
		{
		}

		public SmallBedSouthDeed2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
using System;
using Server;

namespace Server.Items
{
	public class SmallBedSouthAddon3 : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new SmallBedSouthDeed3(); } }

		[Constructable]
		public SmallBedSouthAddon3()
		{
			AddComponent( new AddonComponent( 2663 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 2664 ), 0, 1, 0 );
		}

		public SmallBedSouthAddon3( Serial serial ) : base( serial )
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

	public class SmallBedSouthDeed3 : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new SmallBedSouthAddon3(); } }
		public override int LabelNumber{ get{ return 1044321; } } // small bed (south)

		[Constructable]
		public SmallBedSouthDeed3()
		{
		}

		public SmallBedSouthDeed3( Serial serial ) : base( serial )
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
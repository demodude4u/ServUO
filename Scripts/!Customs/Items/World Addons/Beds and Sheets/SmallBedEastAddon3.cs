using System;
using Server;

namespace Server.Items
{
	public class SmallBedEastAddon3 : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new SmallBedEastDeed3(); } }

		[Constructable]
		public SmallBedEastAddon3()
		{
			AddComponent( new AddonComponent( 2666 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 2667 ), 1, 0, 0 );
		}

		public SmallBedEastAddon3( Serial serial ) : base( serial )
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

	public class SmallBedEastDeed3 : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new SmallBedEastAddon3(); } }
		public override int LabelNumber{ get{ return 1044322; } } // small bed (east)

		[Constructable]
		public SmallBedEastDeed3()
		{
		}

		public SmallBedEastDeed3( Serial serial ) : base( serial )
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
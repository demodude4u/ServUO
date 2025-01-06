using System;
using Server;

namespace Server.Items
{
	public class SmallBedEastAddon2 : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new SmallBedEastDeed2(); } }

		[Constructable]
		public SmallBedEastAddon2()
		{
			AddComponent( new AddonComponent( 2665 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 2667 ), 1, 0, 0 );
		}

		public SmallBedEastAddon2( Serial serial ) : base( serial )
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

	public class SmallBedEastDeed2 : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new SmallBedEastAddon2(); } }
		public override int LabelNumber{ get{ return 1044322; } } // small bed (east)

		[Constructable]
		public SmallBedEastDeed2()
		{
		}

		public SmallBedEastDeed2( Serial serial ) : base( serial )
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
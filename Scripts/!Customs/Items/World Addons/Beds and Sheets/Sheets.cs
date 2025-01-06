using System;
using Server;
using Server.Network;
using Server.Regions;
using Server.Multis;
using Server.Gumps;
using Server.Targeting;
using Server.Items;


namespace Server.Items
{
	public class Sheets : Item, IDyable
	{
		[Constructable]
		public Sheets() : this( 0 )
		{
		}


		[Constructable]
		public Sheets( int hue ) : base( Utility.RandomList( 2706, 2707 ) )
		{
		//	Name = "sheets";
			Weight = 1.0;
			Hue = hue;
			Stackable = false;
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public Sheets( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( !CheckUse( this, from ) )
				return;

			from.Target = new InternalTarget( this );
		}

		public static bool InHouse( Mobile from )
		{
			BaseHouse house = BaseHouse.FindHouseAt( from );

			return ( house != null && house.IsCoOwner( from ) );
		}

		public static bool CheckUse( Sheets tool, Mobile from )
		{
			if ( tool.Deleted || !tool.IsChildOf( from.Backpack ) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			else if ( !InHouse( from ) )
				from.SendLocalizedMessage( 502092 ); // You must be in your house to do this.
			else
				return true;

			return false;
		}

		private class InternalTarget : Target
		{
			private Sheets m_Sheets;

			public InternalTarget( Sheets sheets ) : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;

				m_Sheets = sheets;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item && Sheets.CheckUse( m_Sheets, from ) )
				{
					BaseHouse house = BaseHouse.FindHouseAt( from );
					Item item = (Item)targeted;

					if ( house == null || !house.IsCoOwner( from ) )
					{
						from.SendLocalizedMessage( 502092 ); // You must be in your house to do this.
					}
					else if ( item.Parent != null || !house.IsInside( item ) )
					{
						from.SendLocalizedMessage( 1042270 ); // That is not in your house.
					}
				//	else if ( !house.IsLockedDown( item ) && !house.IsSecure( item ) )
				//	{
				//		from.SendLocalizedMessage( 1042271 ); // That is not locked down.
				//	}
					else
					{
						if ( targeted is AddonComponent )
						{
							AddonComponent bed = (AddonComponent)targeted;

							if ( bed != null && IsBed( bed.ItemID ) )
							{
								bed.Hue = m_Sheets.Hue;
								m_Sheets.Delete();
							}
						}

					}
				}
			}

			private static bool IsBed( int itemid )
			{

				if ( itemid >= 2652 && itemid <= 2653 )
					return true;

				if ( itemid >= 2658 && itemid <= 2659 )
					return true;

				if ( itemid >= 2662 && itemid <= 2667 )
					return true;

				if ( itemid >= 2680 && itemid <= 2691 )
					return true;

				if ( itemid >= 2700 && itemid <= 2705 )
					return true;

				if ( itemid >= 3504 && itemid <= 3509 )
					return true;
					
				if ( itemid >= 4592 && itemid <= 4595 )
					return true;


				return false;
			}
		}
	}
}
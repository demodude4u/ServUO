//================================================//
// Based on winecrafting grounds created by	  //
// dracana, modded by Manu from Splitterwelt.com  //
// for use with carpets					  //
// Desc: For players to place carpets in their	  //
//       houses.  Especially useful for players   //
//       with non-custom housing.
//  Modified for 2.0 by Draco Van Peeble
//================================================//
using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Multis;
using Server.Network;

namespace Server.Items
{
	public class StaffVariableCarpetAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new StaffVariableCarpetAddonDeed(); } }

		#region Constructors
		[Constructable]
		public StaffVariableCarpetAddon( StaffVariableCarpetType type, int width, int height ) : this( (int)type, width, height )
		{
		}

		public StaffVariableCarpetAddon( int type, int width, int height )
		{
			StaffVariableCarpetInfo info = StaffVariableCarpetInfo.GetInfo( type );
			
			AddComponent( new AddonComponent( info.GetItemPart( GroundPosition1.Top ).ItemID ), 0, 0, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( GroundPosition1.Right ).ItemID ), width, 0, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( GroundPosition1.Left ).ItemID ), 0, height, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( GroundPosition1.Bottom ).ItemID ), width, height, 0 );
			
			int w = width - 1;
			int h = height - 1;
			
			for ( int y = 1; y <= h; ++y )
				AddComponent( new AddonComponent( info.GetItemPart( GroundPosition1.West ).ItemID ), 0, y, 0 );
			
			for ( int x = 1; x <= w; ++x )
				AddComponent( new AddonComponent( info.GetItemPart( GroundPosition1.North ).ItemID ), x, 0, 0 );
			
			for ( int y = 1; y <= h; ++y )
				AddComponent( new AddonComponent( info.GetItemPart( GroundPosition1.East ).ItemID ), width, y, 0 );
			
			for ( int x = 1; x <= w; ++x )
				AddComponent( new AddonComponent( info.GetItemPart( GroundPosition1.South ).ItemID ), x, height, 0 );
			
			for ( int x = 1; x <= w; ++x )
				for ( int y = 1; y <= h; ++y )
					AddComponent( new AddonComponent( info.GetItemPart( GroundPosition1.Center ).ItemID ), x, y, 0 );
		}

		public StaffVariableCarpetAddon( Serial serial ) : base( serial )
		{
		}
		#endregion

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), 3 ) )
			{
                from.SendGump(new ConfirmRemovalGumpStaffVariableCarpet( this ));
			}
			else
			{
				from.SendLocalizedMessage( 500295 ); // You are too far away to do that.
			}
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
	
	public enum StaffVariableCarpetType
	{
		BlueStructureBorder,
		BluePlainBorder,
		BlueYellowBorder,
		RedStructureBorder,
		RedPlainBorder,
		YellowStructureBorder
	}
	
	public enum GroundPosition1
	{
		Top,
		Bottom,
		Left,
		Right,
		West,
		North,
		East,
		South,
		Center
	}
	
	public class StaffVariableCarpetInfo
	{
		private GroundItem1Part[] m_Entries;
		
		public GroundItem1Part[] Entries{ get{ return m_Entries; } }
		
		public StaffVariableCarpetInfo( GroundItem1Part[] entries )
		{
			m_Entries = entries;
		}
		
		public GroundItem1Part GetItemPart( GroundPosition1 pos )
		{
			int i = (int)pos;

			if ( i < 0 || i >= m_Entries.Length )
				i = 0;

			return m_Entries[i];
		}
		
		public static StaffVariableCarpetInfo GetInfo( int type )
		{
			if ( type < 0 || type >= m_Infos.Length )
				type = 0;

			return m_Infos[type];
		}
		
		#region StaffVariableCarpetInfo definitions
		private static StaffVariableCarpetInfo[] m_Infos = new StaffVariableCarpetInfo[] {
/* BlueStructureBorder */		new StaffVariableCarpetInfo( new GroundItem1Part[] { 
						new GroundItem1Part( 0xAC3, GroundPosition1.Top, 44, 0 ),
						new GroundItem1Part( 0xAC2, GroundPosition1.Bottom, 44, 68 ),
						new GroundItem1Part( 0xAC4, GroundPosition1.Left, 0, 28 ),
						new GroundItem1Part( 0xAC5, GroundPosition1.Right, 88, 28 ),
						new GroundItem1Part( 0xAF6, GroundPosition1.West, 22, 12 ),
						new GroundItem1Part( 0xAF7, GroundPosition1.North, 66, 12 ),
						new GroundItem1Part( 0xAF8, GroundPosition1.East, 66, 46 ),
						new GroundItem1Part( 0xAF9, GroundPosition1.South, 22, 46 ),
						new GroundItem1Part( 0xABD, GroundPosition1.Center, 44, 24 )
					}),
/* BluePlainBorder */			new StaffVariableCarpetInfo( new GroundItem1Part[] { 
						new GroundItem1Part( 0xAC3, GroundPosition1.Top, 44, 0 ),
						new GroundItem1Part( 0xAC2, GroundPosition1.Bottom, 44, 68 ),
						new GroundItem1Part( 0xAC4, GroundPosition1.Left, 0, 28 ),
						new GroundItem1Part( 0xAC5, GroundPosition1.Right, 88, 28 ),
						new GroundItem1Part( 0xAF6, GroundPosition1.West, 22, 12 ),
						new GroundItem1Part( 0xAF7, GroundPosition1.North, 66, 12 ),
						new GroundItem1Part( 0xAF8, GroundPosition1.East, 66, 46 ),
						new GroundItem1Part( 0xAF9, GroundPosition1.South, 22, 46 ),
						new GroundItem1Part( 0xABE, GroundPosition1.Center, 44, 24 )
					}),
/* BlueYellowBorder */			new StaffVariableCarpetInfo( new GroundItem1Part[] { 
						new GroundItem1Part( 0xAD3, GroundPosition1.Top, 44, 0 ),
						new GroundItem1Part( 0xAD2, GroundPosition1.Bottom, 44, 68 ),
						new GroundItem1Part( 0xAD4, GroundPosition1.Left, 0, 28 ),
						new GroundItem1Part( 0xAD5, GroundPosition1.Right, 88, 28 ),
						new GroundItem1Part( 0xAD6, GroundPosition1.West, 22, 8 ),
						new GroundItem1Part( 0xAD7, GroundPosition1.North, 66, 8 ),
						new GroundItem1Part( 0xAD8, GroundPosition1.East, 66, 46 ),
						new GroundItem1Part( 0xAD9, GroundPosition1.South, 22, 46 ),
						new GroundItem1Part( 0xAD1, GroundPosition1.Center, 44, 24 )
					}),
/* RedStructureBorder 	*/		new StaffVariableCarpetInfo( new GroundItem1Part[] { 
						new GroundItem1Part( 0xACA, GroundPosition1.Top, 44, 0 ),
						new GroundItem1Part( 0xAC9, GroundPosition1.Bottom, 44, 68 ),
						new GroundItem1Part( 0xACB, GroundPosition1.Left, 0, 28 ),
						new GroundItem1Part( 0xACC, GroundPosition1.Right, 88, 28 ),
						new GroundItem1Part( 0xACD, GroundPosition1.West, 22, 10 ),
						new GroundItem1Part( 0xACE, GroundPosition1.North, 66, 12 ),
						new GroundItem1Part( 0xACF, GroundPosition1.East, 66, 46 ),
						new GroundItem1Part( 0xAD0, GroundPosition1.South, 22, 46 ),
						new GroundItem1Part( 0xAC7, GroundPosition1.Center, 44, 24 )
					}),
/* RedPlainBorder */			new StaffVariableCarpetInfo( new GroundItem1Part[] { 
						new GroundItem1Part( 0xACA, GroundPosition1.Top, 44, 0 ),
						new GroundItem1Part( 0xAC9, GroundPosition1.Bottom, 44, 68 ),
						new GroundItem1Part( 0xACB, GroundPosition1.Left, 0, 28 ),
						new GroundItem1Part( 0xACC, GroundPosition1.Right, 88, 28 ),
						new GroundItem1Part( 0xACD, GroundPosition1.West, 22, 10 ),
						new GroundItem1Part( 0xACE, GroundPosition1.North, 66, 12 ),
						new GroundItem1Part( 0xACF, GroundPosition1.East, 66, 46 ),
						new GroundItem1Part( 0xAD0, GroundPosition1.South, 22, 46 ),
						new GroundItem1Part( 0xAC8, GroundPosition1.Center, 44, 24 )
					}),
/* YellowStructureBorder */		new StaffVariableCarpetInfo( new GroundItem1Part[] { 
						new GroundItem1Part( 0xADC, GroundPosition1.Top, 44, 0 ),
						new GroundItem1Part( 0xADB, GroundPosition1.Bottom, 44, 68 ),
						new GroundItem1Part( 0xADD, GroundPosition1.Left, 0, 28 ),
						new GroundItem1Part( 0xADE, GroundPosition1.Right, 88, 28 ),
						new GroundItem1Part( 0xADF, GroundPosition1.West, 22, 8 ),
						new GroundItem1Part( 0xAE0, GroundPosition1.North, 66, 8 ),
						new GroundItem1Part( 0xAE1, GroundPosition1.East, 66, 46 ),
						new GroundItem1Part( 0xAE2, GroundPosition1.South, 22, 46 ),
						new GroundItem1Part( 0xADA, GroundPosition1.Center, 44, 24 )
					}),
/* BlueAndPink */		new StaffVariableCarpetInfo( new GroundItem1Part[] { 
						new GroundItem1Part( 0xAEF, GroundPosition1.Top, 44, 0 ),
						new GroundItem1Part( 0xAEE, GroundPosition1.Bottom, 44, 68 ),
						new GroundItem1Part( 0xAF0, GroundPosition1.Left, 0, 28 ),
						new GroundItem1Part( 0xAF1, GroundPosition1.Right, 88, 28 ),
						new GroundItem1Part( 0xAF2, GroundPosition1.West, 22, 8 ),
						new GroundItem1Part( 0xAF3, GroundPosition1.North, 66, 8 ),
						new GroundItem1Part( 0xAF4, GroundPosition1.East, 66, 46 ),
						new GroundItem1Part( 0xAF5, GroundPosition1.South, 22, 46 ),
						new GroundItem1Part( 0xAFA, GroundPosition1.Center, 44, 24 )
					})
			};
			#endregion
			
		public static StaffVariableCarpetInfo[] Infos{ get{ return m_Infos; } }
	}
	
	public class GroundItem1Part
	{
		private int m_ItemID;
		private  GroundPosition1 m_Info;
		private int m_OffsetX;
		private int m_OffsetY;
		
		public int ItemID
		{
			get{ return m_ItemID; }
		}
		
		public  GroundPosition1 GroundPosition1
		{
			get{ return m_Info; }
		}
		
		// For Gump Rendering
		public int OffsetX
		{
			get{ return m_OffsetX; }
		}
		
		// For Gump Rendering
		public int OffsetY
		{
			get{ return m_OffsetY; }
		}
		
		public GroundItem1Part( int itemID,  GroundPosition1 info, int offsetX, int offsetY )
		{
			m_ItemID = itemID;
			m_Info = info;
			m_OffsetX = offsetX;
			m_OffsetY = offsetY;
		}
	}

    public class ConfirmRemovalGumpStaffVariableCarpet : Gump
    {
        private StaffVariableCarpetAddon m_StaffVariableCarpetAddon;

        public ConfirmRemovalGumpStaffVariableCarpet(StaffVariableCarpetAddon StaffVariableCarpetaddon)
            : base(50, 50)
        {
            m_StaffVariableCarpetAddon = StaffVariableCarpetaddon;

            AddBackground(0, 0, 450, 260, 9270);

            AddAlphaRegion(12, 12, 426, 22);
            AddTextEntry(13, 13, 379, 20, 32, 0, @"Warning!");

            AddAlphaRegion(12, 39, 426, 209);

            AddHtml(15, 50, 420, 185, "<BODY>" +
"<BASEFONT COLOR=YELLOW>You are about to remove this carpet!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>If it is removed, a deed will be placed " +
"<BASEFONT COLOR=YELLOW>in your backpack.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Are you sure that you want to remove this carpet?<BR><BR>" +
                             "</BODY>", false, false);

            AddButton(13, 220, 0xFA5, 0xFA6, 1, GumpButtonType.Reply, 0);
            AddHtmlLocalized(47, 222, 150, 20, 1052072, 0x7FFF, false, false); // Continue

            //AddButton(200, 245, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
            //AddHtmlLocalized(47, 247, 450, 20, 1060051, 0x7FFF, false, false); // CANCEL
            AddButton(350, 220, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
            AddHtmlLocalized(385, 222, 100, 20, 1060051, 0x7FFF, false, false); // CANCEL
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 0 )
                return;

            Mobile from = sender.Mobile;

            from.AddToBackpack(new StaffVariableCarpetAddonDeed());
            m_StaffVariableCarpetAddon.Delete();

            from.SendMessage( "Carpet removed" );
        }
    }
}

using System;
using Server.Mobiles;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server.Gumps;

namespace Server.Items
{
	public class FacialHornChangingDeed : Item
	{

		[Constructable]
		public FacialHornChangingDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
            Name = "Deed of Facial Horn Changing";
			LootType = LootType.Blessed;
		}

        public FacialHornChangingDeed(Serial serial)
            : base(serial)
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
			//dissalow list
			if ( from.Race == Race.Human || from.Race == Race.Elf ) 
               { 
                    from.SendMessage( "Only Gargoyles can use this item!" ); 
                    return; 
               }
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack...
                return;
            }
            if (from.Female)
            {
                from.SendMessage("Only males can use this item!");
                return;
            }
			else
			{
				from.SendGump( new InternalGump( from, this ) );
			}
		}

		private class InternalGump : Gump
		{
			private Mobile m_From;
            private FacialHornChangingDeed m_Deed;

            public InternalGump(Mobile from, FacialHornChangingDeed deed)
                : base(50, 50)
			{
				m_From = from;
				m_Deed = deed;

				from.CloseGump( typeof( InternalGump ) );

				AddBackground( 100, 10, 400, 385, 0xA28 );

				AddHtmlLocalized( 100, 25, 400, 35, 1013008, false, false );
				AddButton( 175, 340, 0xFA5, 0xFA7, 0x0, GumpButtonType.Reply, 0 ); // CANCEL

                AddHtml(210, 342, 90, 35, "Facial Horns Selection Menu", false, false);// <CENTER>Horns Selection Menu</center>

                int[][] RacialData = (from.Race == Race.Gargoyle) ? GargoyleArray : DaemonArray;

				for(int i=1; i<RacialData.Length; i++)
				{
					AddHtmlLocalized( LayoutArray[i][2], LayoutArray[i][3], (i==1) ? 125 : 80, (i==1) ? 70 : 35, (m_From.Female) ? RacialData[i][0] : RacialData[i][1], false, false );
					if ( LayoutArray[i][4] != 0 )
					{
						AddBackground( LayoutArray[i][0], LayoutArray[i][1], 50, 50, 0xA3C );
						AddImage( LayoutArray[i][4], LayoutArray[i][5], (m_From.Female) ? RacialData[i][4] : RacialData[i][5] );
					}
					AddButton( LayoutArray[i][6], LayoutArray[i][7], 0xFA5, 0xFA7, i, GumpButtonType.Reply, 0 );
				}
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if( m_From == null || !m_From.Alive )
					return;

				if ( m_Deed.Deleted )
					return;

				if ( info.ButtonID < 1 || info.ButtonID > 10 )
					return;

                int[][] RacialData = (m_From.Race == Race.Gargoyle) ? GargoyleArray : DaemonArray;

				if ( m_From is PlayerMobile )
				{
					PlayerMobile pm = (PlayerMobile)m_From;

					pm.SetHairMods( 1, 1 ); // clear any hairmods (disguise kit, incognito)
					m_From.HairItemID = (m_From.Female) ? RacialData[info.ButtonID][2] : RacialData[info.ButtonID][3];
					m_Deed.Delete();
				}
			}
/* 
		gump data: bgX, bgY, htmlX, htmlY, imgX, imgY, butX, butY 
*/

			int[][] LayoutArray =
			{
				new int[] { 0 }, /* padding: its more efficient than code to ++ the index/buttonid */
				new int[] { 425, 280, 342, 295, 000, 000, 310, 292 },	// Bald
				new int[] { 235, 060, 150, 075, 145, 015, 118, 073 },	// Horn Style 1
				new int[] { 235, 115, 150, 130, 145, 055, 118, 128 },	// Horn Style 2
				new int[] { 235, 170, 150, 185, 145, 100, 118, 183 },	// Horn Style 3
				new int[] { 235, 225, 150, 240, 145, 155, 118, 238 },	// Horn Style 4
			};

/*
		racial arrays are: cliloc_F, cliloc_M, ItemID_F, ItemID_M, gump_img_F, gump_img_M
*/
			int[][] GargoyleArray =
			{
				new int[] { 0 }, 
				new int[] { 1011064, 1011064, 0, 0, 0, 0  },  // bald 
				new int[] { 1112310, 1112310, 0x4261, 0x42AD, 0X7A0, 0x7A0 }, // Horn Style 1
				new int[] { 1112311, 1112311, 0x4262, 0x42AE, 0X7A1, 0x770 }, // Horn Style 2
				new int[] { 1112312, 1112312, 0x4273, 0x42AF, 0X79E, 0x771 }, // Horn Style 3
				new int[] { 1112313, 1112313, 0x4274, 0x42B0, 0X7A2, 0x772 }, // Horn Style 4
			};
			int[][] DaemonArray = 
			{
				new int[] { 0 }, 
				new int[] { 1011064, 1011064, 0, 0, 0, 0  },  // bald 
				new int[] { 1112310, 1112310, 0x4261, 0x42AD, 0X7A0, 0x7A0 }, // Horn Style 1
				new int[] { 1112311, 1112311, 0x4262, 0x42AE, 0X7A1, 0x770 }, // Horn Style 2
				new int[] { 1112312, 1112312, 0x4273, 0x42AF, 0X79E, 0x771 }, // Horn Style 3
				new int[] { 1112313, 1112313, 0x4274, 0x42B0, 0X7A2, 0x772 }, // Horn Style 4
			};
		}
	}
}
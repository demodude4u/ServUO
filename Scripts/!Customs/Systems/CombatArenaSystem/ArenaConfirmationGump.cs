using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Items;

namespace Server.Gumps
{
    public class ArenaConfirmationGump : Gump
    {
        private const int LabelColor = 0xBFFF;
        private const int EntryColor = 0xBFFF;
        private Mobile m_From;
		private CombatArenaStone m_Stone;

        public ArenaConfirmationGump(Mobile from, CombatArenaStone stone)
            : base(20, 40)
        {
            m_From = from;
			m_Stone = stone;

            Closable = true;
            Disposable = true;
            Dragable = true;

            AddPage(0);
            AddBackground(0, 0, 320, 310, 5054);
            AddImageTiled(10, 10, 300, 290, 2624);
            AddImageTiled(10, 30, 300, 10, 5058);
            AddImageTiled(10, 270, 300, 10, 5058);
            AddAlphaRegion(10, 10, 320, 310);
			
			int count = 0;
			
			bool full = m_Stone.ArenaCreatures.Count > 8;
			
			if (m_Stone.ArenaCreatures.Count <= 0)
			{
				AddButton(44, 66, 0x242, 0x241, 5, GumpButtonType.Reply, 0); // CANCEL
				AddColoredHtml(10, 280, 200, 20, "There are no entries yet.", 0xFFFFFF);
			}
			else
			{
				AddButton(44, 66, 247, 248, 2, GumpButtonType.Reply, 0); // OKAY
				AddButton(144, 66, 242, 241, 5, GumpButtonType.Reply, 0); // CANCEL
			
				AddColoredHtml(10, 12, 280, 20, "ARE YOU SURE? - REMOVE ALL ENTRIES?", LabelColor);
				
				AddBackground(330, 0, 220, 310, 5054);
				AddImageTiled(340, 10, 200, 290, 2624);
				AddImageTiled(340, 30, 200, 10, 5058);
				AddImageTiled(340, 270, 200, 10, 5058);
				AddAlphaRegion(340, 10, 220, 310);
				
				AddColoredHtml(342, 12, 145, 20, "Entries", 0xFFFFFF);
				
				int entrynum = 0;
				foreach (CombatArenaEntry entry in m_Stone.ArenaCreatures)
				{
					AddColoredHtml(368, 40 + (entrynum++ * 24), 160, 20, string.Format("{0} {1}", entry.Num, entry.GetName()), EntryColor);
					if (entrynum > 8) break;
				}

				AddColoredHtml(352, 280, 200, 20, string.Format("# of Entries: {0}", m_Stone.ArenaCreatures.Count), EntryColor);
			}
        }
		
		private void AddColoredHtml(int x, int y, int w, int h, string text, int color)
		{
            AddHtml(x, y, w, h, string.Format("<basefont color=#{0:X}>{1}", color, text), false, false);
		}

        public override void OnResponse(NetState state, RelayInfo info)
        {
			if (info.ButtonID == 2)
			{
				m_Stone.ArenaCreatures = new List<CombatArenaEntry>();
				m_From.SendGump(new ArenaConfigurationGump(m_From, m_Stone)); 
			}
			else if (info.ButtonID == 5)
			{ 
				m_From.SendGump(new ArenaConfigurationGump(m_From, m_Stone));
			}
        }
    }
}
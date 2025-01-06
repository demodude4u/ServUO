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
    public class ArenaActivationGump : Gump
    {
        private const int LabelColor = 0x7FFF;
        private const int EntryColor = 0xBFFF;
        private Mobile m_From;
		private CombatArenaStone m_Stone;

        public ArenaActivationGump(Mobile from, CombatArenaStone stone)
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

            AddHtmlLocalized(10, 12, 160, 20, 1115619, LabelColor, false, false); //<CENTER>Arena Menu - Main</CENTER>
			if (m_Stone.ChargeForUse && m_Stone.Cost > 0)
				AddColoredHtml(165, 12, 145, 20, string.Format("COST TO USE: {0}", m_Stone.Cost), LabelColor);
			
			if (m_Stone.ArenaCreatures.Count <= 0)
				AddLabel(10, 280, 200, "There are no entries yet.");
			else
			{
				
				int entrynum = 0;
				foreach (CombatArenaEntry entry in m_Stone.ArenaCreatures)
				{
					AddButton(18, 40 + (entrynum * 24), 4015, 4016, entrynum + 100, GumpButtonType.Reply, 0);
					AddColoredHtml(58, 40 + (entrynum++ * 24), 160, 20, string.Format("{0} {1}", entry.Num, entry.GetName()), EntryColor);
				}

				AddColoredHtml(12, 280, 200, 20, string.Format("# of Entries: {0}", m_Stone.ArenaCreatures.Count), EntryColor);
			}
        }
		
		private void AddColoredHtml(int x, int y, int w, int h, string text, int color)
		{
            AddHtml(x, y, w, h, string.Format("<basefont color=#{0:X}>{1}", color, text), false, false);
		}

        public override void OnResponse(NetState state, RelayInfo info)
        {
			if (info.ButtonID >= 100)
			{
				if (!m_Stone.ChargeForUse || Banker.Withdraw(m_From, (int)m_Stone.Cost))
				{
					if (m_Stone.ChargeForUse) m_From.SendMessage("{0} Gold was withdrawn from your account.", m_Stone.Cost);
					CombatArenaEntry entry = m_Stone.ArenaCreatures[info.ButtonID - 100];
					Type creature = entry.Creature;
					CombatArenaSpawner spawner = new CombatArenaSpawner(entry.Num, m_Stone.SpawnRadius, creature.Name);
					spawner.MoveToWorld(m_From.Location, m_From.Map);
					spawner.Respawn();
				}
				else
				{
					m_From.SendMessage("This Combat Arena Stone costs {0} Gold to use.", m_Stone.Cost);
				}
			}
        }
    }
}
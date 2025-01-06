using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Items;

namespace Server.Gumps
{
	public enum ArenaCreatures
	{
		airelemental = 1028429,
		ancientlich = 1072450,
		arcticogrelord = 1018227,
		bloodelemental = 1018252,
		brigand = 1078446,
		crystalelemental = 1075931,
		cyclops = 1028493,
		daemon = 1049574,
		direwolf = 1018116,
		dragon = 1018152,
		drake = 1018154,
		efreet = 1018223,
		eldergazer = 1018162,
		ettin = 1018111,
		evilmage = 1072445,
		evilmagelord = 1072447,
		executioner = 1060781,
		fireelemental = 1072486,
		frosttroll = 1072479,
		gargoyle = 1018097,
		gazer = 1028436,
		iceelemental = 1072480,
		icefiend = 1072477,
		iceserpent = 1018182,
		lavalizard = 1072492,
		lavaserpent = 1072434,
		lavasnake = 1072435,
		lich = 1072444,
		lichlord = 1072446,
		mummy = 1018246,
		ogre = 1018094,
		ogrelord = 1018177,
		ophidianarchmage = 1029641,
		ophidianknight = 1018229,
		ophidianmage = 1028498,
		ophidianwarrior = 1028499,
		orc = 1018133,
		orcbomber = 1075906,
		orcbrute = 1075911,
		orccaptain = 1018100,
		orcscout = 1075905,
		poisonelemental = 1018254,
		ratman = 1072421,
		ratmanarcher = 1072423,
		rottingcorpse = 1018247,
		savage = 1079799,
		savagerider = 1075909,
		silverserpent = 1072428,
		skeletalknight = 1018239,
		skeletalmage = 1072448,
		skeleton = 1027569,
		snowelemental = 1072481,
		stonegargoyle = 1018160,
		terathanavenger = 1018244,
		terathanmatriarch = 1028492,
		terathanwarrior = 1028490,
		titan = 1028494,
		waterelemental = 1028459,
		whitewyrm = 1018141,
		wraith = 1075960,
		wyvern = 1018155,
		zombie = 1018096
	}
	
    public class ArenaConfigurationGump : Gump
    {
        private const int LabelColor = 0x7FFF;
        private const int EntryColor = 0xBFFF;
        private Mobile m_From;
		private CombatArenaStone m_Stone;
		private int m_Offset;

        public ArenaConfigurationGump(Mobile from, CombatArenaStone stone)
            : this(from, stone, 0)
        {
		}

        public ArenaConfigurationGump(Mobile from, CombatArenaStone stone, int offset)
            : base(20, 40)
        {
            m_From = from;
			m_Stone = stone;
			m_Offset = offset;
			int creaturenum = Enum.GetNames(typeof(ArenaCreatures)).Length;

            Closable = true;
            Disposable = true;
            Dragable = true;

            AddPage(0);
            AddBackground(0, 0, 320, 310, 5054);
            AddImageTiled(10, 10, 300, 290, 2624);
            AddImageTiled(10, 30, 300, 10, 5058);
            AddImageTiled(10, 270, 300, 10, 5058);
            AddAlphaRegion(10, 10, 320, 310);
			
            AddHtmlLocalized(10, 12, 145, 20, 1149585, LabelColor, false, false); //<CENTER>Arena Menu - Admin</CENTER>
			
			int max = Math.Min(9, creaturenum - offset);
			int count = 0;
			
			bool full = m_Stone.ArenaCreatures.Count > 8;
			
			for (int x = offset; x<offset+max; x++)
			{
				AddHtmlLocalized(58, 40 + (24 * count), 200, 20, (int)ArenaCreaturesVector[x], LabelColor, false, false);
				if (!full) AddButton(18, 40 + (24 * count), 4015, 4016, x + 100, GumpButtonType.Reply, 0);
				count++;
			}
			
			if (offset < creaturenum - 9)
			{
				AddHtmlLocalized(254, 12, 70, 20, 1043353, LabelColor, false, false); // Next
				AddButton(290, 12, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0);
			}
			
			if (offset > 0)
			{
				AddHtmlLocalized(180, 12, 70, 20, 1043354, LabelColor, false, false); // Previous
				AddButton(155, 12, 0x15E3, 0x15E7, 3, GumpButtonType.Reply, 0);
			}
			
			if (m_Stone.ArenaCreatures.Count <= 0)
				AddColoredHtml(10, 280, 200, 20, "There are no entries yet.", 0xFFFFFF);
			else
			{
				AddBackground(330, 0, 220, 310, 5054);
				AddImageTiled(340, 10, 200, 290, 2624);
				AddImageTiled(340, 30, 200, 10, 5058);
				AddImageTiled(340, 270, 200, 10, 5058);
				AddAlphaRegion(340, 10, 220, 310);
				
				AddColoredHtml(342, 12, 145, 20, "Entries", 0xFFFFFF);
				
				int entrynum = 0;
				foreach (CombatArenaEntry entry in m_Stone.ArenaCreatures)
				{
					AddButton(348, 44 + (entrynum * 24), 0x3, 0x4, entrynum + 40, GumpButtonType.Reply, 0);
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
				m_From.SendGump(new ArenaConfigurationGump(m_From, m_Stone, m_Offset + 9)); 
			}
			else if (info.ButtonID == 3)
			{ 
				m_From.SendGump(new ArenaConfigurationGump(m_From, m_Stone, m_Offset - 9));
			}
			else if (info.ButtonID >= 100)
			{
				m_From.SendGump(new EntryConfigurationGump(m_From, m_Stone, m_Offset, info.ButtonID - 100));
			}
			else if (info.ButtonID >= 40)
			{
				m_Stone.ArenaCreatures.RemoveAt(info.ButtonID - 40);
				m_From.SendGump(new ArenaConfigurationGump(m_From, m_Stone, m_Offset));
			}
			
        }

		public static ArenaCreatures[] ArenaCreaturesVector = { ArenaCreatures.airelemental, ArenaCreatures.ancientlich, ArenaCreatures.arcticogrelord, ArenaCreatures.bloodelemental, ArenaCreatures.brigand, ArenaCreatures.crystalelemental, ArenaCreatures.cyclops, ArenaCreatures.daemon, ArenaCreatures.direwolf, ArenaCreatures.dragon, ArenaCreatures.drake, ArenaCreatures.efreet, ArenaCreatures.eldergazer, ArenaCreatures.ettin, ArenaCreatures.evilmage, ArenaCreatures.evilmagelord, ArenaCreatures.executioner, ArenaCreatures.fireelemental, ArenaCreatures.frosttroll, ArenaCreatures.gargoyle, ArenaCreatures.gazer, ArenaCreatures.iceelemental, ArenaCreatures.icefiend, ArenaCreatures.iceserpent, ArenaCreatures.lavalizard, ArenaCreatures.lavaserpent, ArenaCreatures.lavasnake, ArenaCreatures.lich, ArenaCreatures.lichlord, ArenaCreatures.mummy, ArenaCreatures.ogre, ArenaCreatures.ogrelord, ArenaCreatures.ophidianarchmage, ArenaCreatures.ophidianknight, ArenaCreatures.ophidianmage, ArenaCreatures.ophidianwarrior, ArenaCreatures.orc, ArenaCreatures.orcbomber, ArenaCreatures.orcbrute, ArenaCreatures.orccaptain, ArenaCreatures.orcscout, ArenaCreatures.poisonelemental, ArenaCreatures.ratman, ArenaCreatures.ratmanarcher, ArenaCreatures.rottingcorpse, ArenaCreatures.savage, ArenaCreatures.savagerider, ArenaCreatures.silverserpent, ArenaCreatures.skeletalknight, ArenaCreatures.skeletalmage, ArenaCreatures.skeleton, ArenaCreatures.snowelemental, ArenaCreatures.stonegargoyle, ArenaCreatures.terathanavenger, ArenaCreatures.terathanmatriarch, ArenaCreatures.terathanwarrior, ArenaCreatures.titan, ArenaCreatures.waterelemental, ArenaCreatures.whitewyrm, ArenaCreatures.wraith, ArenaCreatures.wyvern, ArenaCreatures.zombie };

		public static Type[] ArenaCreaturesType = { typeof(ArenaAirElemental), typeof(ArenaAncientLich), typeof(ArenaArcticOgreLord), typeof(ArenaBloodElemental), typeof(ArenaBrigand), typeof(ArenaCrystalElemental), typeof(ArenaCyclops), typeof(ArenaDaemon), typeof(ArenaDireWolf), typeof(ArenaDragon), typeof(ArenaDrake), typeof(ArenaEfreet), typeof(ArenaElderGazer), typeof(ArenaEttin), typeof(ArenaEvilMage), typeof(ArenaEvilMageLord), typeof(ArenaExecutioner), typeof(ArenaFireElemental), typeof(ArenaFrostTroll), typeof(ArenaGargoyle), typeof(ArenaGazer), typeof(ArenaIceElemental), typeof(ArenaIceFiend), typeof(ArenaIceSerpent), typeof(ArenaLavaLizard), typeof(ArenaLavaSerpent), typeof(ArenaLavaSnake), typeof(ArenaLich), typeof(ArenaLichLord), typeof(ArenaMummy), typeof(ArenaOgre), typeof(ArenaOgreLord), typeof(ArenaOphidianArchmage), typeof(ArenaOphidianKnight), typeof(ArenaOphidianMage), typeof(ArenaOphidianWarrior), typeof(ArenaOrc), typeof(ArenaOrcBomber), typeof(ArenaOrcBrute), typeof(ArenaOrcCaptain), typeof(ArenaOrcScout), typeof(ArenaPoisonElemental), typeof(ArenaRatman), typeof(ArenaRatmanArcher), typeof(ArenaRottingCorpse), typeof(ArenaSavage), typeof(ArenaSavageRider), typeof(ArenaSilverSerpent), typeof(ArenaSkeletalKnight), typeof(ArenaSkeletalMage), typeof(ArenaSkeleton), typeof(ArenaSnowElemental), typeof(ArenaStoneGargoyle), typeof(ArenaTerathanAvenger), typeof(ArenaTerathanMatriarch), typeof(ArenaTerathanWarrior), typeof(ArenaTitan), typeof(ArenaWaterElemental), typeof(ArenaWhiteWyrm), typeof(ArenaWraith), typeof(ArenaWyvern), typeof(ArenaZombie) };
    }
	
	public class EntryConfigurationGump : Gump
	{
        private const int LabelColor = 0x7FFF;
		
		private Mobile m_From;
		private CombatArenaStone m_Stone;
		private int m_Offset;
		private int m_ButtonID;
		
		public EntryConfigurationGump(Mobile from, CombatArenaStone stone, int offset, int buttonID)
            : base(20, 40)
		{
			m_From = from;
			m_Stone = stone;
			m_Offset = offset;
			m_ButtonID = buttonID;
			int clilocID = (int)ArenaConfigurationGump.ArenaCreaturesVector[buttonID];
			CliLocEntry cliloc = (CliLocEntry)ClilocGump.ClilocHash[clilocID];
			
			
            AddPage(0);
            AddBackground(0, 0, 320, 310, 5054);
            AddImageTiled(10, 10, 300, 290, 2624);
            AddImageTiled(10, 30, 300, 10, 5058);
            AddImageTiled(10, 270, 300, 10, 5058);
            AddAlphaRegion(10, 10, 320, 310);
			
            AddColoredHtml(12, 12, 300, 20, string.Format("How many {0} to spawn?", cliloc.Text), 0xFFFFFF);
			
			AddButton(18, 40, 4015, 4016, 1, GumpButtonType.Reply, 0);
            AddColoredHtml(58, 40, 300, 20, "1", 0xFFFFFF);
			AddButton(18, 64, 4015, 4016, 2, GumpButtonType.Reply, 0);
            AddColoredHtml(58, 64, 300, 20, "2", 0xFFFFFF);
			AddButton(18, 88, 4015, 4016, 3, GumpButtonType.Reply, 0);
            AddColoredHtml(58, 88, 300, 20, "3", 0xFFFFFF);
			AddButton(18, 112, 4015, 4016, 4, GumpButtonType.Reply, 0);
            AddColoredHtml(58, 112, 300, 20, "4", 0xFFFFFF);
			AddButton(18, 136, 4015, 4016, 5, GumpButtonType.Reply, 0);
            AddColoredHtml(58, 136, 300, 20, "5", 0xFFFFFF);
			AddButton(18, 160, 4015, 4016, 6, GumpButtonType.Reply, 0);
            AddColoredHtml(58, 160, 300, 20, "6", 0xFFFFFF);
			AddButton(18, 184, 4015, 4016, 7, GumpButtonType.Reply, 0);
            AddColoredHtml(58, 184, 300, 20, "7", 0xFFFFFF);
			AddButton(18, 208, 4015, 4016, 8, GumpButtonType.Reply, 0);
            AddColoredHtml(58, 208, 300, 20, "8", 0xFFFFFF);
			AddButton(18, 232, 4015, 4016, 10, GumpButtonType.Reply, 0);
            AddColoredHtml(58, 232, 300, 20, "10", 0xFFFFFF);
			
			AddButton(118, 40, 4015, 4016, 12, GumpButtonType.Reply, 0);
            AddColoredHtml(158, 40, 300, 20, "12", 0xFFFFFF);
			AddButton(118, 64, 4015, 4016, 15, GumpButtonType.Reply, 0);
            AddColoredHtml(158, 64, 300, 20, "15", 0xFFFFFF);
			AddButton(118, 88, 4015, 4016, 18, GumpButtonType.Reply, 0);
            AddColoredHtml(158, 88, 300, 20, "18", 0xFFFFFF);
			AddButton(118, 112, 4015, 4016, 20, GumpButtonType.Reply, 0);
            AddColoredHtml(158, 112, 300, 20, "20", 0xFFFFFF);
			AddButton(118, 136, 4015, 4016, 25, GumpButtonType.Reply, 0);
            AddColoredHtml(158, 136, 300, 20, "25", 0xFFFFFF);
			AddButton(118, 160, 4015, 4016, 30, GumpButtonType.Reply, 0);
            AddColoredHtml(158, 160, 300, 20, "30", 0xFFFFFF);
			AddButton(118, 184, 4015, 4016, 35, GumpButtonType.Reply, 0);
            AddColoredHtml(158, 184, 300, 20, "35", 0xFFFFFF);
			AddButton(118, 208, 4015, 4016, 40, GumpButtonType.Reply, 0);
            AddColoredHtml(158, 208, 300, 20, "40", 0xFFFFFF);
			AddButton(118, 232, 4015, 4016, 50, GumpButtonType.Reply, 0);
            AddColoredHtml(158, 232, 300, 20, "50", 0xFFFFFF);
			
			
			AddButton(290, 12, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0);
			
		}
		
		private void AddColoredHtml(int x, int y, int w, int h, string text, int color)
		{
            AddHtml(x, y, w, h, string.Format("<basefont color=#{0:X}>{1}", color, text), false, false);
		}

        public override void OnResponse(NetState state, RelayInfo info)
        {
			if (info.ButtonID >= 1)
			{
				Type creatureType = ArenaConfigurationGump.ArenaCreaturesType[m_ButtonID];
				CombatArenaEntry entry = new CombatArenaEntry(creatureType.Name, info.ButtonID);
				m_Stone.ArenaCreatures.Add(entry);
				
				m_From.SendGump(new ArenaConfigurationGump(m_From, m_Stone, m_Offset));
			}
			
        }
	}
}



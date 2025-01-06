using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Commands;

namespace Server.Mobiles
{
    public class CrimStatsGump : Gump
    {
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;
        private const int BlueHue = 0x777;
		
		private Mobile m_From;

        public CrimStatsGump(Mobile from)
            : base(40, 40)
        {
            Resizable = false;
			m_From = from;
			
			int crimes = JailUtility.Crimes.Count;
			int warrants = JailUtility.Warrants.Count;	
            int convicts = JailUtility.Convicts.Count;
			int excons = JailUtility.ExConvicts.Count;
			
            AddPage(0);
            AddBackground(0, 0, 400, 240, 9350);
			AddLabel(160, 10, BlueHue, "Server Crime Stats");
			
			AddLabel(50, 70, 0, string.Format("Number of Crimes reported by Vendors: {0}", crimes));
			AddButton(20, 71, 1210, 1210, 1, GumpButtonType.Reply, 0);
			
			AddLabel(50, 110, 0, string.Format("Number of Players currently in Jail: {0}", convicts));
			AddButton(20, 111, 1210, 1210, 2, GumpButtonType.Reply, 0);
			
			AddLabel(50, 150, 0, string.Format("Number of Players with outstanding Warrants: {0}", warrants));
			AddButton(20, 151, 1210, 1210, 3, GumpButtonType.Reply, 0);
			
			AddLabel(50, 190, 0, string.Format("Number of formerly incarcerated Players: {0}", excons));
			AddButton(20, 191, 1210, 1210, 4, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
			switch(info.ButtonID)
			{
				case 1:
				{
					m_From.SendGump(new CrimesGump(m_From));
					break;
				}
				case 2:
				{
					m_From.SendGump(new ConvictsGump(m_From));
					break;
				}
				case 3:
				{
					m_From.SendGump(new WarrantsGump(m_From));
					break;
				}
				case 4:
				{
					m_From.SendGump(new ExConvictsGump(m_From));
					break;
				}
			}
        }
    }
	
    public class WarrantsGump : Gump
    {
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;
        private const int BlueHue = 0x777;
		private const int LinesPerPage = 20;
		
		private Mobile m_From;

        public WarrantsGump(Mobile from) : this(from, 0) { }

        public WarrantsGump(Mobile from, int index)
            : base(40, 40)
        {
            Resizable = false;
			m_From = from;

            AddPage(0);
            AddBackground(0, 0, 680, 680, 9350);
			AddLabel(160, 10, BlueHue, "Outstanding Warrants");
			
			int currentPage = 0;
			int i = 0;
			int totalPages = (int)Math.Ceiling((double)JailUtility.Warrants.Count / LinesPerPage);
			
            for (int x = currentPage; x < totalPages; x++)
            {
				i = x * LinesPerPage;
				AddPage(currentPage + 1);
				var batch = JailUtility.Warrants.Skip(i).Take(LinesPerPage);
				if (batch != null)
				{
					foreach (var kvp in batch)
					{
						int y = 60;
						int key = kvp.Key;
						List<Warrant> warrants = kvp.Value;
						Warrant w = warrants[0];
						AddButton(10, y, 1210, 1210, 1000 + key, GumpButtonType.Reply, 0); //Warrants details
						AddLabel(50, y, 0, string.Format("{0} has {1} outstanding Warrants.", w.Criminal.Name, warrants.Count));
						y = y + 25;
					}
				}
				if (currentPage > 0)
					AddButton(20, 10, 2468, 2467, 1, GumpButtonType.Reply, 0); //Previous Page
				if (currentPage < totalPages - 1) 
					AddButton(510, 10, 2471, 2470, 2, GumpButtonType.Reply, 0); //Next Page
            }
			
        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
			if (info.ButtonID >= 1000)
			{
				m_From.SendGump(new WarrantListGump(m_From, info.ButtonID - 1000));
			}
			else
			{
				if (m_From.HasGump(typeof(CrimStatsGump)))
					m_From.CloseGump(typeof(CrimStatsGump));
				m_From.SendGump(new CrimStatsGump(m_From));
			}
        }
    }
	
    public class WarrantListGump : Gump
    {
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;
        private const int BlueHue = 0x777;
		private const int LinesPerPage = 20;
		
		private Mobile m_From;

        public WarrantListGump(Mobile from, int serial)
            : base(40, 40)
        {
            Resizable = false;
			m_From = from;
			List<Warrant> warrants = JailUtility.Warrants[serial];
			if (warrants.Count <= 0) return;
			Warrant w = warrants[0];
			
            AddPage(0);
            AddBackground(0, 0, 680, 680, 9350);
			AddLabel(160, 10, BlueHue, "Outstanding Warrants for " + w.Criminal.Name);
			
			int currentPage = 0;
			int i = 0;
			int totalPages = (int)Math.Ceiling((double)warrants.Count / LinesPerPage);
			
            for (int x = currentPage; x < totalPages; x++)
            {
				i = x * LinesPerPage;
				AddPage(currentPage + 1);
				var batch = warrants.Skip(i).Take(LinesPerPage);
				if (batch != null)
				{
					int y = 60;
					foreach (Warrant warrant in batch)
					{
						int hours = (int)((TimeSpan)(DateTime.UtcNow - warrant.When)).TotalHours;
						AddLabel(20, y, 0, string.Format(
							"{0} is wanted for {1} by {2}. The warrant was issued {3} hours ago.)", 
							warrant.Criminal.Name, warrant.Offense, warrant.AccuserFullName, hours));
						y = y + 25;
					}
				}
				if (currentPage > 0)
					AddButton(20, 10, 2468, 2467, 1, GumpButtonType.Reply, 0); //Previous Page
				if (currentPage < totalPages - 1) 
					AddButton(510, 10, 2471, 2470, 2, GumpButtonType.Reply, 0); //Next Page
            }
			
        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
			if (m_From.HasGump(typeof(WarrantsGump)))
				m_From.CloseGump(typeof(WarrantsGump));
			m_From.SendGump(new WarrantsGump(m_From));
        }
    }
	
    public class ConvictsGump : Gump
    {
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;
        private const int BlueHue = 0x777;
		private const int LinesPerPage = 20;
		
		private Mobile m_From;

        public ConvictsGump(Mobile from) : this(from, 0) { }

        public ConvictsGump(Mobile from, int index)
            : base(40, 40)
        {
            Resizable = false;
			m_From = from;

            AddPage(0);
            AddBackground(0, 0, 680, 680, 9350);
			AddLabel(160, 10, BlueHue, "Current Jail Residents");
			
			int currentPage = 0;
			int i = 0;
			int totalPages = (int)Math.Ceiling((double)JailUtility.Convicts.Count / LinesPerPage);
			
            for (int x = currentPage; x < totalPages; x++)
            {
				i = x * LinesPerPage;
				AddPage(currentPage + 1);
				var batch = JailUtility.Convicts.Skip(i).Take(LinesPerPage);
				if (batch != null)
				{
					foreach (var kvp in batch)
					{
						int y = 60;
						JailStatus js = kvp.Value;
						int minutes = (int)((TimeSpan)(js.FredomTime - DateTime.UtcNow)).TotalMinutes;
						int coal = js.CoalRequired - js.CoalCollected;
						AddLabel(20, y, 0, string.Format(
							"{0} was jailed by {1} and will be released in {2} minutes, or after mining {3} more coal.)", 
							js.Owner.Name, js.Jailor, minutes, coal));
						y = y + 25;
					}
				}
				if (currentPage > 0)
					AddButton(20, 10, 2468, 2467, 1, GumpButtonType.Reply, 0); //Previous Page
				if (currentPage < totalPages - 1) 
					AddButton(510, 10, 2471, 2470, 2, GumpButtonType.Reply, 0); //Next Page
            }
        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
			if (m_From.HasGump(typeof(CrimStatsGump)))
				m_From.CloseGump(typeof(CrimStatsGump));
			m_From.SendGump(new CrimStatsGump(m_From));
        }
    }
	
    public class ExConvictsGump : Gump
    {
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;
        private const int BlueHue = 0x777;
		private const int LinesPerPage = 20;
		
		private Mobile m_From;

        public ExConvictsGump(Mobile from) : this(from, 0) { }

        public ExConvictsGump(Mobile from, int index)
            : base(40, 40)
        {
            Resizable = false;
			m_From = from;

            AddPage(0);
            AddBackground(0, 0, 680, 680, 9350);
			AddLabel(160, 10, BlueHue, "Server Ex-Convicts");
			
			int currentPage = 0;
			int i = 0;
			int totalPages = (int)Math.Ceiling((double)JailUtility.ExConvicts.Count / LinesPerPage);
			
            for (int x = currentPage; x < totalPages; x++)
            {
				i = x * LinesPerPage;
				AddPage(currentPage + 1);
				var batch = JailUtility.ExConvicts.Skip(i).Take(LinesPerPage);
				if (batch != null)
				{
					foreach (var kvp in batch)
					{
						int y = 60;
						int key = kvp.Key;
						int times = kvp.Value;
						string name = JailUtility.GetConvictName(key);
						int hours = (int)((TimeSpan)(DateTime.UtcNow - JailUtility.ExConLastTime[key])).TotalHours;
						AddLabel(20, y, 0, string.Format("{0} has been incarcerated {1} times (last: {2} hours ago)", name, times, hours));
						y = y + 25;
					}
				}
				if (currentPage > 0)
					AddButton(20, 10, 2468, 2467, 1, GumpButtonType.Reply, 0); //Previous Page
				if (currentPage < totalPages - 1) 
					AddButton(510, 10, 2471, 2470, 2, GumpButtonType.Reply, 0); //Next Page
            }
        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
			if (m_From.HasGump(typeof(CrimStatsGump)))
				m_From.CloseGump(typeof(CrimStatsGump));
			m_From.SendGump(new CrimStatsGump(m_From));
        }
    }
	
    public class CrimesGump : Gump
    {
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;
        private const int BlueHue = 0x777;
		private const int LinesPerPage = 20;
		
		private Mobile m_From;

        public CrimesGump(Mobile from) : this(from, 0) { }

        public CrimesGump(Mobile from, int index)
            : base(40, 40)
        {
            Resizable = false;
			m_From = from;

            AddPage(0);
            AddBackground(0, 0, 680, 680, 9350);
			AddLabel(160, 10, BlueHue, "Vendor Complaints");
			
			int currentPage = 0;
			int i = 0;
			int totalPages = (int)Math.Ceiling((double)JailUtility.Crimes.Count / LinesPerPage);
			
            for (int x = currentPage; x < totalPages; x++)
            {
				i = x * LinesPerPage;
				AddPage(currentPage + 1);
				var batch = JailUtility.Crimes.Skip(i).Take(LinesPerPage);
				if (batch != null)
				{
					foreach (var kvp in batch)
					{
						int y = 60;
						string key = kvp.Key;
						Crime crime = kvp.Value;
						int hours = (int)((TimeSpan)(DateTime.UtcNow - crime.When)).TotalHours;
						AddLabel(20, y, 0, string.Format("{0} accused {1} of theft {2} hours ago in {3} at {4}", crime.Victim.Name, crime.Suspect.Name, hours, crime.CrimeMap, crime.CrimeLocation));
						y = y + 25;
					}
				}
				if (currentPage > 0)
					AddButton(20, 10, 2468, 2467, 1, GumpButtonType.Reply, 0); //Previous Page
				if (currentPage < totalPages - 1) 
					AddButton(510, 10, 2471, 2470, 2, GumpButtonType.Reply, 0); //Next Page
            }
			
        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
			if (m_From.HasGump(typeof(CrimStatsGump)))
				m_From.CloseGump(typeof(CrimStatsGump));
			m_From.SendGump(new CrimStatsGump(m_From));
        }
    }
}

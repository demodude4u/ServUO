using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Items;
using Server.Regions;
using Server.Misc;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Targeting;

namespace Server.Mobiles
{
    public class JailUtility
    {
		private static TimeSpan EXPUNGE_TIME = TimeSpan.FromDays(3);
        private static Dictionary<string, Crime> m_Crimes;
        public static Dictionary<string, Crime> Crimes { get { return m_Crimes; } set { m_Crimes = value; } }
        private static Dictionary<int, List<Warrant>> m_Warrants;
        public static Dictionary<int, List<Warrant>> Warrants { get { return m_Warrants; } set { m_Warrants = value; } }
        private static Dictionary<int, JailStatus> m_Convicts;
        public static Dictionary<int, JailStatus> Convicts { get { return m_Convicts; } set { m_Convicts = value; } }
        private static Dictionary<int, int> m_ExConvicts;
        public static Dictionary<int, int> ExConvicts { get { return m_ExConvicts; } set { m_ExConvicts = value; } }
        private static Dictionary<int, DateTime> m_ExConLastTime;
        public static Dictionary<int, DateTime> ExConLastTime { get { return m_ExConLastTime; } set { m_ExConLastTime = value; } }
		private static List<Point3D> m_JailCells;
        public static List<Point3D> JailCells { get { return m_JailCells; } set { m_JailCells = value; } }
		private static Map m_JailMap;
        public static Map JailMap { get { return m_JailMap; } set { m_JailMap = value; } }

        public static void Configure()
        {
            EventSink.WorldLoad += new WorldLoadEventHandler(Load);
            EventSink.WorldSave += new WorldSaveEventHandler(Save);
        }
		
		public static bool ExpungeRecord(Mobile asker, Mobile from)
		{
			bool ExCon = false;
			bool LastDate = false;
			
			if (m_Warrants.ContainsKey(from.Serial.Value))
			{
				int num = 0;
				if (asker != null)
					asker.SendMessage("There are {0} outstanding warrants for {1}. Their record cannot be expunged.", num, from.Name);
				return false;
			}
			else if (m_Convicts.ContainsKey(from.Serial.Value))
			{
				if (asker != null)
					asker.SendMessage("{0} is currently in Jail. Their record cannot be expunged.", from.Name);
				return false;
			}
			else if (m_ExConvicts.ContainsKey(from.Serial.Value))
			{
				if (m_ExConLastTime.ContainsKey(from.Serial.Value))
				{
					if (m_ExConLastTime[from.Serial.Value] + EXPUNGE_TIME < DateTime.UtcNow)
					{
						if (asker != null)
							asker.SendMessage("{0}'s record is now expunged.", from.Name);
						m_ExConvicts.Remove(from.Serial.Value);
						m_ExConLastTime.Remove(from.Serial.Value);
						return true;
					}
					return false;
				}
				else
				{
					if (asker != null)
						asker.SendMessage("They were an ExCon with no record of LastTime. Record Expunged.");
					m_ExConvicts.Remove(from.Serial.Value);
					return true;
				}
				return false;
			}
			else if (m_ExConLastTime.ContainsKey(from.Serial.Value))
			{
				if (asker != null)
					asker.SendMessage("They were in LastTime with no record of being ExCon. Record Expunged.");
				m_ExConLastTime.Remove(from.Serial.Value);
				return true;
			}
			if (asker != null)
				asker.SendMessage("They have no criminal record to expunge.");
			return false;	
		}

        public static void Initialize()
        {
            CommandSystem.Register("Expunge", AccessLevel.Player, new CommandEventHandler(Expunge_OnCommand));
            CommandSystem.Register("Jail", AccessLevel.GameMaster, new CommandEventHandler(Jail_OnCommand));
            CommandSystem.Register("Unjail", AccessLevel.GameMaster, new CommandEventHandler(Unjail_OnCommand));
            CommandSystem.Register("WipeCrime", AccessLevel.Owner, new CommandEventHandler(WipeCrime_OnCommand));
            CommandSystem.Register("Crime", AccessLevel.Administrator, new CommandEventHandler(CrimeStats_OnCommand));
			m_JailCells = new List<Point3D>(){
				new Point3D(5276, 1164, 0),
				new Point3D(5286, 1164, 0),
				new Point3D(5296, 1164, 0),
				new Point3D(5306, 1164, 0),
				new Point3D(5276, 1174, 0),
				new Point3D(5286, 1174, 0),
				new Point3D(5296, 1174, 0),
				new Point3D(5306, 1174, 0),
				new Point3D(5283, 1184, 0)
			};
			m_JailMap = Map.Trammel;
		}

        [Usage("Crime")]
        [Description("Displays crime statistics for the server.")]
        public static void CrimeStats_OnCommand(CommandEventArgs e)
        {
			Mobile caller = e.Mobile;
            if (caller.AccessLevel >= AccessLevel.Administrator)
			{
				if (caller.HasGump(typeof(CrimStatsGump)))
					caller.CloseGump(typeof(CrimStatsGump));
				caller.SendGump(new CrimStatsGump(caller));
            }
        }

        [Usage("WipeCrime")]
        [Description("Clears crime statistics for testing purposes.")]
        public static void WipeCrime_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile.AccessLevel == AccessLevel.Owner)
			{
				e.Mobile.SendMessage("Crime stats have all been reset!");
				m_Crimes 		= new Dictionary<string, Crime>();
				m_Warrants 		= new Dictionary<int, List<Warrant>>();
				m_ExConLastTime = new Dictionary<int, DateTime>();
				m_ExConvicts 	= new Dictionary<int, int>();
				m_Convicts 		= new Dictionary<int, JailStatus>();
            }
        }

        [Usage("Expunge")]
        [Description("Expunge the selected player's criminal record.")]
        public static void Expunge_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile is PlayerMobile)
            {
				if (e.Mobile.AccessLevel == AccessLevel.Player)
				{
					ExpungeRecord(e.Mobile, e.Mobile);
				}
				else
				{
					e.Mobile.Target = new ExpungeTarget();
					e.Mobile.SendLocalizedMessage(3000218);
				}
            }
        }

        [Usage("Unjail")]
        [Description("Release the selected player from jail.")]
        public static void Unjail_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile is PlayerMobile)
            {
                e.Mobile.Target = new JailTarget(true, 0);
                e.Mobile.SendLocalizedMessage(3000218);
            }
        }

        [Usage("Jail [minutes]")]
        [Description("Places the selected player in jail for 30 [or optional #] minutes.")]
        public static void Jail_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile is PlayerMobile)
            {
				int jailtime = 30; //minutes
				if (e.Arguments.Length > 0)
				{
					try{ jailtime = int.Parse(e.Arguments[0]); }
					catch{}
				}
                e.Mobile.Target = new JailTarget(false, jailtime);
                e.Mobile.SendLocalizedMessage(3000218);
            }
        }
		
		public static void CatchThief(Mobile victim, Mobile thief)
		{
			string key = victim.Name + "+" + thief.Name;
			bool callGuards = false;
			if(!m_Crimes.ContainsKey(key))
			{
				m_Crimes.Add(key, new Crime(victim, thief, DateTime.UtcNow, victim.Location, victim.Map));
				Console.WriteLine("Caught times: {0}",m_Crimes[key].TimesCaught);
			}
			else
			{
				m_Crimes[key].TimesCaught++;
				Console.WriteLine("Caught times: {0}",m_Crimes[key].TimesCaught);
				if (m_Crimes[key].TimesCaught >= Crime.TIMES_CAUGHT_BEFORE_GUARDS)
				{
					m_Crimes[key].TransformTime = DateTime.UtcNow;
					callGuards = true;
				}
			}
			
			if (!callGuards)
			{
				victim.Direction = victim.GetDirectionTo(thief);
				thief.Direction = thief.GetDirectionTo(victim);
				victim.Animate(31, 5, 1, true, false, 0);
				victim.Say(Utility.RandomList(1005560, 1013046, 1079127, 1013038, 1013039, 1010634));
				thief.Animate(20, 5, 1, true, false, 0);
				thief.Damage(Math.Max((Utility.Random(3) + 3), (int)(thief.Hits / (Utility.Random(8) + 8))));
			}
			else
			{
				try
				{
					object[] guardParams = new object[1];
					guardParams[0] = thief;
					TheftGuard newGuard = Activator.CreateInstance(typeof(TheftGuard), guardParams) as TheftGuard;
					m_Crimes[key].Guard = newGuard;
					newGuard.VendorName = victim.Name;
					Console.WriteLine("Created Guard: {0}", newGuard.Name);
				} 
				catch 
				{
					Console.WriteLine("Unable to create guard.");
				}
			}
		}
		
		public static void IssueWarrant(Mobile criminal, Mobile accuser, string offense, DateTime when, Point3D location, Map map)
		{
			Console.WriteLine("Warrant has been issued.");
			if (!m_Warrants.ContainsKey(criminal.Serial.Value))
			{
				m_Warrants.Add(criminal.Serial.Value, new List<Warrant>());
			}
			m_Warrants[criminal.Serial.Value].Add(new Warrant(criminal, accuser, offense, when, location, map));
		}
		
		public static void ProcessConvictRelease(Mobile m, JailStatus js)
		{
			m.Location = js.ReleaseLocation;
			m.Map = js.ReleaseMap;
			if (m.AccessLevel == AccessLevel.Player) m.Blessed = false;
			
			if (!m_ExConvicts.ContainsKey(m.Serial.Value))
			{
				m_ExConvicts.Add(m.Serial.Value, 0);
			}
			m_ExConvicts[m.Serial.Value]++;
			
			if (!m_ExConLastTime.ContainsKey(m.Serial.Value))
			{
				m_ExConLastTime.Add(m.Serial.Value, DateTime.UtcNow);
			}
			else
				m_ExConLastTime[m.Serial.Value] = DateTime.UtcNow;
			
			m_Convicts.Remove(m.Serial.Value);
		}
		
		private static void LoadInitialValues()
		{
			m_Crimes = new Dictionary<string, Crime>();
			m_Warrants = new Dictionary<int, List<Warrant>>();
			m_ExConLastTime = new Dictionary<int, DateTime>();
			m_ExConvicts = new Dictionary<int, int>();
			m_Convicts = new Dictionary<int, JailStatus>();
		}

        public static void Load()
        {
			LoadInitialValues();
            string filePath = Path.Combine("Saves/JailSystem", "JailSystem.bin");
            if (!File.Exists(filePath))
            {
                if (Core.Debug) Console.WriteLine("JailSystem.bin does not exist so exit Load().");
                return;
            }
            BinaryFileReader reader = null;
            FileStream fs = null;
            try // READER CREATION
            {
                fs = new FileStream(filePath, (FileMode)3, (FileAccess)1, (FileShare)1);
                reader = new BinaryFileReader(new BinaryReader(fs));
            }
            catch (Exception e)
            {
                if (Core.Debug) Console.WriteLine("Failed at READER CREATION");
                Console.WriteLine(e.Message);
                return;
            }

            int version = 0;

            if (reader != null)
            {
                try // VALUES INSERTION start
                {
                    version = reader.ReadInt();
                    if (Core.Debug) Console.WriteLine("Jail System version is: {0}", version);
                    switch (version)
                    {
						case 3:
						{
							int numCrimes = reader.ReadInt();
							if (Core.Debug) Console.WriteLine("Number of Convicts (numCrimes) is: {0}", numCrimes);
							if (numCrimes > 0)
							{
								for (int x = 0; x < numCrimes; x++)
								{
									try
									{
										m_Crimes.Add(reader.ReadString(), new Crime(reader));
									}
									catch { continue; }
								}
							}
							goto case 2;
						}
                        case 2:
						{
							// Read Warrants dictionary
							int numWarrants = reader.ReadInt();
							if (Core.Debug) Console.WriteLine("Number of Warrants is: {0}", numWarrants);
							if (numWarrants > 0)
							{
								for (int i = 0; i < numWarrants; i++)
								{
									int serial = reader.ReadInt();
									int numSerialWarrants = reader.ReadInt();
									if (numSerialWarrants > 0)
									{
										List<Warrant> warrants = new List<Warrant>();
										for (int j = 0; j < numSerialWarrants; j++)
										{
											warrants.Add( new Warrant(reader) );
										}
										m_Warrants.Add(serial, warrants);
									}
								}
							}
							int numECLT = reader.ReadInt();
							if (Core.Debug) Console.WriteLine("Number of ExConLastTime records is: {0}", numECLT);
							if (numECLT > 0)
							{
								for (int x = 0; x < numECLT; x++)
								{
									try
									{
										m_ExConLastTime.Add(reader.ReadInt(), reader.ReadDateTime());
									}
									catch { continue; }
								}
							}
							int numEx = reader.ReadInt();
							if (Core.Debug) Console.WriteLine("Number of ExConvicts records is: {0}", numEx);
							if (Core.Debug && numECLT != numEx)
								Console.WriteLine("Number of ExConvicts and ExConLastTime records is not the same!");
							if (numEx > 0)
							{
								for (int x = 0; x < numEx; x++)
								{
									try
									{
										m_ExConvicts.Add(reader.ReadInt(), reader.ReadInt());
									}
									catch { continue; }
								}
							}
							goto case 1;
						}
                        case 1:
						{
							int num = reader.ReadInt();
							if (Core.Debug) Console.WriteLine("Number of Convicts (num) is: {0}", num);
							if (num > 0)
							{
								for (int x = 0; x < num; x++)
								{
									try
									{
										m_Convicts.Add(reader.ReadInt(), new JailStatus(reader));
									}
									catch { continue; }
								}
							}
							break;
						}
                        case 0:
						{
							break;
						}
                    }
                } //// VALUES INSERTION end
                catch (Exception e)
                {
                    if (Core.Debug) Console.WriteLine("Failed at VALUES INSERTION"); Console.WriteLine(e.Message);
                }

            }
        }

        public static void Save(WorldSaveEventArgs e)
        {
            if (!Directory.Exists("Saves/JailSystem"))
                Directory.CreateDirectory("Saves/JailSystem");
            string filePath = Path.Combine("Saves/JailSystem", "JailSystem.bin");
            BinaryFileWriter writer = null;

            try
            {
                writer = new BinaryFileWriter(filePath, true);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                return;
            }
            writer.Write((int)3); //version
			
			int position = 0;
            try
            {
                writer.Write((int)m_Crimes.Count);
                if (m_Crimes.Count > 0)
                {
					position = 400;
                    foreach (Crime c in m_Crimes.Values)
                    {
						position++;
                        if (c.Victim == null) continue;
                        writer.Write((string)(c.Victim.Name + "+" + c.Suspect.Name));
                        c.Serialize(writer);
                    }
                }
				writer.Write(m_Warrants.Count);
				if (m_Warrants.Count > 0)
				{
					position = 100;
					foreach (KeyValuePair<int, List<Warrant>> kvp in m_Warrants)
					{
						position++;
						writer.Write(kvp.Key);
						writer.Write(kvp.Value.Count);
						foreach (Warrant warrant in kvp.Value)
						{
							warrant.Serialize(writer);
						}
					}
				}
                writer.Write((int)m_ExConLastTime.Count);
                if (m_ExConLastTime.Count > 0)
                {
					position = 200;
                    foreach (KeyValuePair<int, DateTime> kvp in m_ExConLastTime)
                    {
						position++;
                        writer.Write((int)kvp.Key);
                        writer.Write((DateTime)kvp.Value);
                    }
                }
                writer.Write((int)m_ExConvicts.Count);
                if (m_ExConvicts.Count > 0)
                {
					position = 300;
                    foreach (KeyValuePair<int, int> kvp in m_ExConvicts)
                    {
						position++;
                        writer.Write((int)kvp.Key);
                        writer.Write((int)kvp.Value);
                    }
                }
                writer.Write((int)m_Convicts.Count);
                if (m_Convicts.Count > 0)
                {
					position = 400;
                    foreach (JailStatus js in m_Convicts.Values)
                    {
						position++;
                        if (js.Owner == null) continue;
                        writer.Write((int)js.Owner.Serial.Value);
                        js.Serialize(writer);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Failed at position {0}", position);
                Console.WriteLine(err.ToString());
            }
            writer.Close();
        }

        public static void NewJailStatus(Mobile jailor, Mobile m, int minutes)
        {
			JailStatus js = null;
			int key = m.Serial.Value;
			int totalminutes = minutes;
			if (JailUtility.Convicts.ContainsKey(key)) js = JailUtility.Convicts[key];
            if (js == null)
            {
				int murderCount = 0;
				bool murder = (jailor is MurderGuard);
				if (murder) murderCount = 1;
				if (Warrants.ContainsKey(key))
				{
					foreach (Warrant w in Warrants[key])
					{
						switch (w.Offense)
						{
							case "Theft": { if (!murder) { totalminutes += 20; } break; }
							case "Assault": { if (!murder) { totalminutes += 30; } break; }
							case "Murder": { murderCount += 1; break; }
						}
					}
					Warrants[key] = null;
					Warrants.Remove(key);
				}
				if (murder) 
				{
					totalminutes = murderCount * 60;
				}
				string jailorName = jailor.Name;
				if (jailor is PlayerMobile && jailor.AccessLevel > m.AccessLevel)
					jailorName = "Staff-member " + jailorName;
				if (jailor is CrimeGuard)
					jailorName = jailorName + " the Guard";
                js = new JailStatus(m, totalminutes, jailorName);
				Point3D cell = (Point3D)JailCells[((new System.Random()).Next(0, JailCells.Count - 1))];
				m.Location = cell;
				m.Map = JailMap;
				m.Blessed = true;
				JailUtility.Convicts.Add(key, js);
				if (jailor is CrimeGuard)
				{
					string victim = (jailor as CrimeGuard).VendorName;
					Console.WriteLine("Here is the key: {0}", victim + "+" + m.Name);
					if (Crimes.ContainsKey(victim + "+" + m.Name))
					{
						Console.WriteLine("Remove the Crimes for {0}.", victim + "+" + m.Name);
						Crimes.Remove(victim + "+" + m.Name);
					}
				}
            }
            else
            {
				if (jailor is PlayerMobile) jailor.SendMessage("{0} is already jailed", m.Name);
                return;
            }
        }
		
		public static string GetConvictName(int serial)
		{
			foreach (var player in World.Mobiles.Values.Where(mobile => mobile is PlayerMobile).Cast<PlayerMobile>())
			{
				if (player.Serial.Value == serial) return player.Name;
			}
			return "Name not found";
		}
	}

    public class ExpungeTarget : Target
    {
        public ExpungeTarget()
            : base(-1, false, TargetFlags.None)
        {
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
			if (targeted is PlayerMobile)
			{
				PlayerMobile player = targeted as PlayerMobile;
				if (JailUtility.ExpungeRecord(from, player))
				{
					from.SendMessage("Expunge Successful.");
					if (player.NetState != null)
						player.SendMessage("Your criminal record has been expunged.");
				}
			}
		}
	}

    public class JailTarget : Target
    {
        bool m_Releasing = false;
		int m_Jailtime = 30;

        public JailTarget(bool releasing, int jailtime)
            : base(-1, false, TargetFlags.None)
        {
            m_Releasing = releasing;
			m_Jailtime = jailtime;
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            if (from is PlayerMobile && targeted is PlayerMobile)
            {
                string jail = "jail";
                Mobile m = (Mobile)targeted;
                if (from.AccessLevel < m.AccessLevel)
                {
                    from.SendMessage("{0} has a higher access level than you and you can not do that.", m.Name);
                    if (m_Releasing) jail = "release";
                    m.SendMessage(from.Name + " tried to " + jail + " you");
                }
                else
                {//jailor has a higher (or equal) access level than the target				
                    if (m_Releasing)
                    {
						JailStatus js = null;
						if (JailUtility.Convicts.ContainsKey(m.Serial.Value))
							js = JailUtility.Convicts[m.Serial.Value];
                        if (js == null)
                        {
                            from.SendMessage(m.Name + " is not in jail.");
                            return;
                        }
						else
						{
							JailUtility.ProcessConvictRelease(m, js);
							m.SendLocalizedMessage(501659);
						}
                    }
                    else
                    {
                        JailUtility.NewJailStatus(from, m, m_Jailtime);
                    }
                }
            }
            else
            {
                from.SendLocalizedMessage(503312);
            }
        }
    }
	
	public class Crime
	{
		public static int	TIMES_CAUGHT_BEFORE_GUARDS = 3;
        private Mobile 		m_Victim;
		private Mobile		m_Suspect;
		private DateTime 	m_When;
		private Point3D		m_CrimeLocation;
		private Map			m_CrimeMap;
		private int			m_TimesCaught;
		private DateTime	m_TransformTime;
		private Mobile		m_Guard;
		
        public Mobile Victim { get { return m_Victim; } set { m_Victim = value; } }
        public Mobile Suspect { get { return m_Suspect; } set { m_Suspect = value; } }
        public DateTime When { get { return m_When; } set { m_When = value; } }
        public Point3D CrimeLocation { get { return m_CrimeLocation; } set { m_CrimeLocation = value; } }
        public Map CrimeMap { get { return m_CrimeMap; } set { m_CrimeMap = value; } }
        public int TimesCaught { get { return m_TimesCaught; } set { m_TimesCaught = value; } }
        public DateTime TransformTime { get { return m_TransformTime; } set { m_TransformTime = value; } }
        public Mobile Guard { get { return m_Guard; } set { m_Guard = value; } }
		
        public Crime(Mobile victim, Mobile suspect, DateTime when, Point3D location, Map map)
        {
			m_Victim 		= victim;
			m_Suspect 		= suspect;
			m_When 			= when;
			m_CrimeLocation = location;
			m_CrimeMap 		= map;
			m_TimesCaught	= 1;
		}
		
        public Crime(GenericReader reader)
        {
            m_Victim 		= reader.ReadMobile();
            m_Suspect 		= reader.ReadMobile();
			m_When 			= reader.ReadDateTime();
			m_CrimeLocation = reader.ReadPoint3D();
			m_CrimeMap 		= reader.ReadMap();
			m_TimesCaught	= reader.ReadInt();
			m_TransformTime	= reader.ReadDateTime();
            m_Guard 		= reader.ReadMobile();
		}
		
        public void Serialize(GenericWriter writer)
        {
            writer.Write((Mobile)	m_Victim 		);
            writer.Write((Mobile)	m_Suspect 		);
            writer.Write((DateTime)	m_When 			);
			writer.Write(			m_CrimeLocation );
			writer.Write(			m_CrimeMap 		);
            writer.Write((int)		m_TimesCaught 	);
            writer.Write((DateTime)	m_TransformTime	);
            writer.Write((Mobile)	m_Guard 		);
		}
	}
	
	public class Warrant
	{
        private Mobile 		m_Criminal;
		private Mobile		m_Accuser;
		private string 		m_Offense;
		private DateTime 	m_When;
		private Point3D		m_CrimeLocation;
		private Map			m_CrimeMap;
		
        public Mobile Criminal { get { return m_Criminal; } set { m_Criminal = value; } }
        public Mobile Accuser { get { return m_Accuser; } set { m_Accuser = value; } }
        public string Offense { get { return m_Offense; } set { m_Offense = value; } }
        public DateTime When { get { return m_When; } set { m_When = value; } }
        public Point3D CrimeLocation { get { return m_CrimeLocation; } set { m_CrimeLocation = value; } }
        public Map CrimeMap { get { return m_CrimeMap; } set { m_CrimeMap = value; } }
		public string AccuserFullName { 
			get {
				try { return (Accuser is CrimeGuard) ? Accuser.Name + " the Guard" : Accuser.Name; }
				catch { return "a Guard"; }
			} 
		}
		
        public Warrant(Mobile criminal, Mobile accuser, string offense, DateTime when, Point3D location, Map map)
        {
			m_Criminal 		= criminal;
			m_Accuser 		= accuser;
			m_Offense 		= offense;
			m_When 			= when;
			m_CrimeLocation = location;
			m_CrimeMap 		= map;
		}
		

        public Warrant(GenericReader reader)
        {
            m_Criminal 		= reader.ReadMobile();
            m_Accuser 		= reader.ReadMobile();
			m_Offense 		= reader.ReadString();
			m_When 			= reader.ReadDateTime();
			m_CrimeLocation = reader.ReadPoint3D();
			m_CrimeMap 		= reader.ReadMap();
		}

        public void Serialize(GenericWriter writer)
        {
            writer.Write((Mobile)	m_Criminal 		);
            writer.Write((Mobile)	m_Accuser 		);
            writer.Write((string)	m_Offense 		);
            writer.Write((DateTime)	m_When 			);
			writer.Write(			m_CrimeLocation );
			writer.Write(			m_CrimeMap 		);
		}
	}
	
    public class JailStatus
    {
        private Mobile 		m_Owner;
		private string		m_Jailor;
		private int 		m_Minutes;
		private DateTime 	m_FredomTime;
		private int 		m_CoalRequired;
		private int 		m_CoalCollected;
		private Point3D		m_ReleaseLocation;
		private Map			m_ReleaseMap;
		
        public Mobile Owner { get { return m_Owner; } set { m_Owner = value; } }
        public string Jailor { get { return m_Jailor; } set { m_Jailor = value; } }
        public int Minutes { get { return m_Minutes; } set { m_Minutes = value; } }
        public DateTime FredomTime { get { return m_FredomTime; } set { m_FredomTime = value; } }
        public int CoalRequired { get { return m_CoalRequired; } set { m_CoalRequired = value; } }
        public int CoalCollected { get { return m_CoalCollected; } set { m_CoalCollected = value; } }
        public Point3D ReleaseLocation { get { return m_ReleaseLocation; } set { m_ReleaseLocation = value; } }
        public Map ReleaseMap { get { return m_ReleaseMap; } set { m_ReleaseMap = value; } }
		
        public JailStatus(Mobile owner, int minutes, string jailor)
        {
            m_Owner = owner;
			m_Jailor = jailor;
			m_Minutes = minutes;
			m_FredomTime = DateTime.UtcNow + TimeSpan.FromMinutes(minutes);
			m_CoalRequired = minutes;
			m_CoalCollected = 0;
			m_ReleaseLocation = owner.Location;
			m_ReleaseMap = owner.Map;
		}

        public JailStatus(GenericReader reader)
        {
            m_Owner = reader.ReadMobile();
            m_Jailor = reader.ReadString();
			m_Minutes = reader.ReadInt();
			m_FredomTime = reader.ReadDateTime();
			m_CoalRequired = reader.ReadInt();
			m_CoalCollected = reader.ReadInt();
			m_ReleaseLocation = reader.ReadPoint3D();
			m_ReleaseMap = reader.ReadMap();
		}

        public void Serialize(GenericWriter writer)
        {
            writer.Write((Mobile)m_Owner);
            writer.Write((string)m_Jailor);
			writer.Write((int)m_Minutes);
            writer.Write((DateTime)m_FredomTime);
            writer.Write((int)m_CoalRequired);
            writer.Write((int)m_CoalCollected);
			writer.Write(m_ReleaseLocation);
			writer.Write(m_ReleaseMap);
		}
	}
	
	public delegate void ConfirmWarrantGumpCallback( Mobile guard, Mobile from, bool okay );

	public class ConfirmWarrantGump : Gump
	{
		private ConfirmWarrantGumpCallback m_Callback;
		private Mobile m_Guard;

		public ConfirmWarrantGump( Mobile guard, string header, int headerColor, string content, int contentColor, int width, int height, ConfirmWarrantGumpCallback callback ) : base( (640 - width) / 2, (480 - height) / 2 )
		{
			m_Callback = callback;
			m_Guard = guard;

			Closable = false;

			AddPage( 0 );

			AddBackground( 0, 0, width, height, 5054 );

			AddImageTiled( 10, 10, width - 20, 20, 2624 );
			AddAlphaRegion( 10, 10, width - 20, 20 );
			AddHtml( 10, 10, width - 20, 20, String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", headerColor, header ), false, false );

			AddImageTiled( 10, 40, width - 20, height - 80, 2624 );
			AddAlphaRegion( 10, 40, width - 20, height - 80 );

			AddHtml( 10, 40, width - 20, height - 80, String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", contentColor, content ), false, false );

			AddImageTiled( 10, height - 30, width - 20, 20, 2624 );
			AddAlphaRegion( 10, height - 30, width - 20, 20 );

			AddButton( 10, height - 30, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtml( 40, height - 30, 170, 20, String.Format( "<BASEFONT COLOR=#{0:X6}>Yes, I give up!</BASEFONT>", contentColor ), false, false );

			AddButton( 10 + ((width - 20) / 2), height - 30, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddHtml( 40 + ((width - 20) / 2), height - 30, 170, 20, String.Format( "<BASEFONT COLOR=#{0:X6}>No, do your worst!</BASEFONT>", contentColor ), false, false );
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Mobile m = sender.Mobile;
			if ( info.ButtonID == 1 && m_Callback != null )
				m_Callback( m_Guard, m, true );
			else if ( m_Callback != null )
				m_Callback( m_Guard, m, false );
		}
	}
}
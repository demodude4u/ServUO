using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;

namespace Server.Mobiles
{
    public class CombatArenaEntry
    {
        private string m_CreatureName;
		private int m_Num;
		
		public Type Creature
		{
			get
			{
				for (int x=0; x<ArenaConfigurationGump.ArenaCreaturesType.Length; x++)
				{
					if (ArenaConfigurationGump.ArenaCreaturesType[x].Name == m_CreatureName)
					{
						return ArenaConfigurationGump.ArenaCreaturesType[x];
					}
				}
				return null;
			}
		}
		
		public int Num { get { return m_Num; } set { m_Num = value; } }
		
		public string GetName()
		{
			for (int x=0; x<ArenaConfigurationGump.ArenaCreaturesType.Length; x++)
			{
				if (ArenaConfigurationGump.ArenaCreaturesType[x].Name == m_CreatureName)
				{
					int vect = (int)ArenaConfigurationGump.ArenaCreaturesVector[x];
					return ((CliLocEntry)ClilocGump.ClilocHash[vect]).Text;
				}
			}
			return "name not found";
		}

        public CombatArenaEntry(string creature, int num)
        {
			m_CreatureName = creature;
			m_Num = num;
        }

        public void Serialize(GenericWriter writer)
        {
            writer.WriteEncodedInt(0); // version
			
			writer.Write((string)m_CreatureName);
            writer.WriteEncodedInt(m_Num);
        }

        public CombatArenaEntry(GenericReader reader)
        {
            int version = reader.ReadEncodedInt();
			
			m_CreatureName = reader.ReadString();
			m_Num = reader.ReadEncodedInt();
        }
    }
}
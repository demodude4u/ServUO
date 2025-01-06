using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
    public class CombatArenaStone : Item
    {
        public override int LabelNumber { get { return -1; } }
		
		private int m_SpawnRadius;
		private List<CombatArenaEntry> m_ArenaCreatures;
		private bool m_ChargeForUse;
		private int m_Cost;
		
		[CommandProperty(AccessLevel.GameMaster)]
		public int SpawnRadius { get { return m_SpawnRadius; } set { m_SpawnRadius = value; } }
		
		public List<CombatArenaEntry> ArenaCreatures { get { return m_ArenaCreatures; } set { m_ArenaCreatures = value; } }
		
		[CommandProperty(AccessLevel.GameMaster)]
		public bool ChargeForUse { get { return m_ChargeForUse; } set { m_ChargeForUse = value; InvalidateProperties(); } }
		
		[CommandProperty(AccessLevel.GameMaster)]
		public int Cost { get { return m_Cost; } set { m_Cost = value; InvalidateProperties(); } }

        [Constructable]
        public CombatArenaStone()
            : base(0x1173)
        {
			Name = "Combat Arena Stone";
			Movable = false;
			m_ArenaCreatures = new List<CombatArenaEntry>();
			m_SpawnRadius = 15;
			m_ChargeForUse = false;
			m_Cost = 2000;
        }
		
		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			string cost = m_ChargeForUse ? string.Format(";  Usage Cost: {0} Gold.", m_Cost) : "";
			list.Add("Entries: {0}{1}", m_ArenaCreatures.Count, cost);
		}

        private delegate void ArenaCallback(Mobile from);

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            if (from.Alive && from.InRange(this.GetWorldLocation(), 5))
            {
                if (from.AccessLevel >= AccessLevel.GameMaster)
                {
                    list.Add(new ArenaMenuItem(new ArenaCallback(Configuration), 2132));
                    list.Add(new ArenaMenuItem(new ArenaCallback(ClearList), 5146));
                }
                list.Add(new ArenaMenuItem(new ArenaCallback(Activate), 6170));
            }
        }

        private class ArenaMenuItem : ContextMenuEntry
        {
            private ArenaCallback m_Callback;

            public ArenaMenuItem(ArenaCallback callback, int number)
                : base(number, 5)
            {
                m_Callback = callback;
            }

            public override void OnClick()
            {
                Mobile from = Owner.From;

                if (from.CheckAlive())
                    m_Callback(from);
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(this.GetWorldLocation(), 5))
            {
                if (from.AccessLevel >= AccessLevel.GameMaster) { Configuration(from); }
                else { Activate(from); }
            }
            else
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
            }
        }

        public void Configuration(Mobile from)
        {
			from.SendGump( new ArenaConfigurationGump(from, this) );
        }

        public void ClearList(Mobile from)
        {
			from.SendGump( new ArenaConfirmationGump(from, this) );
        }

        public void Activate(Mobile from)
        {
			from.SendGump( new ArenaActivationGump(from, this) );
        }

        public CombatArenaStone(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(3); // version
			
			writer.Write(m_ChargeForUse);
			writer.Write(m_Cost);
			writer.Write(m_SpawnRadius);
			
			writer.Write(m_ArenaCreatures.Count);
			if (m_ArenaCreatures.Count > 0)
			{
				foreach (CombatArenaEntry entry in m_ArenaCreatures)
				{
					entry.Serialize(writer);
				}
			}
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
			
			m_ArenaCreatures = new List<CombatArenaEntry>();
			
			switch(version)
			{
				case 3: 
				{
					m_ChargeForUse = reader.ReadBool();
					m_Cost = reader.ReadInt();
					goto case 2;
				}
				case 2: 
				{
					m_SpawnRadius = reader.ReadInt();
					goto case 1;
				}
				case 1: 
				{
					int count = reader.ReadInt();
					
					if (count > 0)
					{
						for (int x=0; x<count; x++)
						{
							m_ArenaCreatures.Add(new CombatArenaEntry(reader));
						}
					}
					break;
				}
				case 0: { break; }
			}
        }
    }
}
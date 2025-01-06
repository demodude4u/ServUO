using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;

namespace Server.Mobiles
{
    public class CombatArenaSpawner : XmlSpawner
    {
		private Timer myTimer;
		
        public CombatArenaSpawner(int num, int radius, string name) : base(num, 9999998, 9999999, 0, radius, name)
        {
			myTimer = new CombatArenaTimer(this);
			myTimer.Start();
        }
		
		public void OnTick()
		{
			if (CurrentCount == 0)
			{
				this.Delete();
			}
		}
		
		public CombatArenaSpawner(Serial serial) : base(serial)
		{
		}

        public void Serialize(GenericWriter writer)
        {
            writer.WriteEncodedInt(0); // version
        }

        public void Deserialize(GenericReader reader)
        {
            int version = reader.ReadEncodedInt();
			myTimer = new CombatArenaTimer(this);
			myTimer.Start();
        }
    }
	
    public class CombatArenaTimer : Timer
    {
        private readonly CombatArenaSpawner m_Spawn;
        public CombatArenaTimer(CombatArenaSpawner spawn)
            : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
        {
            this.m_Spawn = spawn;
            this.Priority = TimerPriority.OneSecond;
        }

        protected override void OnTick()
        {
            this.m_Spawn.OnTick();
        }
    }
}
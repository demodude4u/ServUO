using System;
using System.Collections.Generic;
using Server.Engines.CannedEvil;
using Server.Items;
using Server.Services.Virtues;

namespace Server.Mobiles
{
    public abstract class BaseMiniChampion : BaseCreature
    {
		private string m_RewardBagItem;
		private int m_BankGoldReward;
		[CommandProperty(AccessLevel.GameMaster)]
		public string RewardBagItem
		{
			get{ return m_RewardBagItem; }
			set{ m_RewardBagItem = value; }
		}
		[CommandProperty(AccessLevel.GameMaster)]
		public int BankGoldReward
		{
			get{ return m_BankGoldReward; }
			set{ m_BankGoldReward = value; }
		}
        public BaseMiniChampion(AIType aiType)
            : this(aiType, FightMode.Closest)
        {
        }

        public BaseMiniChampion(AIType aiType, FightMode mode)
            : base(aiType, mode, 18, 1, 0.1, 0.2)
        {
        }

        public BaseMiniChampion(Serial serial)
            : base(serial)
        {
        }
		
		public override bool CanBeParagon { get { return false; } }

        public static void GiveReward(Mobile m, Item item, BaseMiniChampion champ)
        {
            if (m == null)	//sanity
                return;

            if (m.Alive)
			{
                m.AddToBackpack(item);
				m.SendMessage(0, "You have received a reward for your valor!");
			}
			else if (m.Corpse != null)
			{
				m.Corpse.DropItem(item);
				m.SendMessage(0, "As you were not alive when the champion was defeated, a reward has been placed on your corpse!");
			}
			if ( champ.BankGoldReward > 0 )
			{
				BankCheck check = new BankCheck(champ.BankGoldReward);
				if (m.Alive)
				{
					m.AddToBackpack(check);
					m.SendMessage(0, "You have received a bank check worth " +champ.BankGoldReward+ " gold!");
				}
				else if (m.Corpse != null)
				{
					m.Corpse.DropItem(check);
					m.SendMessage(0, "As you were not alive when the champion was defeated, a bank check worth " +champ.BankGoldReward+ " gold has been placed on your corpse!");
				}
			}
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
			writer.Write((int)m_BankGoldReward);
			writer.Write((string)m_RewardBagItem);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
			m_BankGoldReward = reader.ReadInt();
			m_RewardBagItem = reader.ReadString();
        }

        public virtual void GiveRewardBag()
        {
            List<Mobile> toGive = new List<Mobile>();
            List<DamageStore> rights = GetLootingRights();
			Type type = ScriptCompiler.FindTypeByName(this.m_RewardBagItem);
			if (type == null)
			{
				this.Say("Failed to generate reward!");
				return;
			}
			Item reward = Activator.CreateInstance(type) as Item;
			if (reward == null)
				return;

            for (int i = rights.Count - 1; i >= 0; --i)
            {
                DamageStore ds = rights[i];

                if (ds.m_HasRight && InRange(ds.m_Mobile, 100) && ds.m_Mobile.Map == this.Map)
                    toGive.Add(ds.m_Mobile);
            }

            if (toGive.Count == 0)
                return;

            for (int i = 0; i < toGive.Count; i++)
            {
                Mobile m = toGive[i];

                if (!(m is PlayerMobile))
                    continue;

                bool gainedPath = false;

                int pointsToGain = 400;

                if (VirtueHelper.Award(m, VirtueName.Valor, pointsToGain, ref gainedPath))
                {
                    if (gainedPath)
                        m.SendLocalizedMessage(1054032); // You have gained a path in Valor!
                    else
                        m.SendLocalizedMessage(1054030); // You have gained in Valor!
                    //No delay on Valor gains
                }
            }
			
            for (int i = 0; i < toGive.Count; ++i)
            {
				Mobile m = toGive[i];
				if ( m != null)
					GiveReward(m, reward, this);
            }
        }

        public override bool OnBeforeDeath()
        {
            this.GiveRewardBag();
			this.PlaySound(0x5B4);

            return base.OnBeforeDeath();
        }
    }
}
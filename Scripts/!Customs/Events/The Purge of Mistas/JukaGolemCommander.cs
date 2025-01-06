using System;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("a juka corpse")] 
    public class JukaGolemCommander : BaseMiniChampion
    {
        private Mobile m_Summon1;
		private Mobile m_Summon2;
		private Mobile m_Summon3;
		private Mobile m_Summon4;
		private int m_TotalSummons;
		private DateTime m_LastTimeDamaged;
		private TimeSpan m_ResetDelay = TimeSpan.FromMinutes(2.0);//this time span indicates the amount of time it takes the boss to re-set to max health and unsummon all constructs.
		
        [Constructable]
        public JukaGolemCommander()
            : base(AIType.AI_Mage, FightMode.Closest)
        {
            Name = "a juka golem commander";
            Body = 765;
			BankGoldReward = 10000;
			RewardBagItem = "ArcaneGem";

            SetStr(125, 150);
            SetDex(125, 150);
            SetInt(125, 150);

            SetHits(10000);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 50, 50);
            SetResistance(ResistanceType.Fire, 50, 50);
            SetResistance(ResistanceType.Cold, 50, 50);
            SetResistance(ResistanceType.Poison, 50, 50);
            SetResistance(ResistanceType.Energy, 0, 0);

            SetSkill(SkillName.Anatomy, 60.0, 60.0);
            SetSkill(SkillName.EvalInt, 95.0, 95.0);
            SetSkill(SkillName.Magery, 100.0, 100.0);
            SetSkill(SkillName.Tactics, 75.0, 75.0);
            SetSkill(SkillName.Wrestling, 90.0, 90.0);

            Fame = 25000;
            Karma = -25000;

            VirtualArmor = 30;

            Container bag = new Bag();

            int count = Utility.RandomMinMax(10, 20);

            for (int i = 0; i < count; ++i)
            {
                Item item = Loot.RandomReagent();

                if (item == null)
                    continue;

                if (!bag.TryDropItem(this, item, false))
                    item.Delete();
            }

            PackItem(bag);

            PackItem(new ArcaneGem(count));

            if (Core.ML)
                PackItem(Engines.Plants.Seed.RandomPeculiarSeed(2));
        }
		
		public override void OnDamage(int amount, Mobile from, bool willkill)
		{
			this.m_LastTimeDamaged = DateTime.UtcNow;
			
			base.OnDamage(amount, from, willkill);
		}
		
		public override void OnThink()
		{
			if (DateTime.UtcNow > this.m_LastTimeDamaged + this.m_ResetDelay && this.Hits < this.HitsMax)
				this.DoReset();
			else if ( this.Hits <= 9000 && this.m_TotalSummons <= 0 )
			{
				this.Say("Here's a little something fresh out of the workshop!");
				this.DoMinorSummon();
			}
			else if (this.Hits <= 7500 && this.m_TotalSummons <= 1)
			{
				this.Say("More backup needed! Send something FINISHED this time!");
				this.DoMinorSummon();
			}
			else if (this.Hits <= 5000 && this.m_TotalSummons <= 2)
			{
				this.Say("This had better work this time!");
				this.DoMinorSummon();
			}
			else if (this.Hits <= 2500 && this.m_TotalSummons <= 3)
			{
				this.Say("I need another soldier! Send it now!");
				this.DoMinorSummon();
			}
			else if (this.Hits <= 1000 && this.m_TotalSummons <= 4)
			{
				this.Say("Send everything! Send it all!");
				this.DoMassSummon();
			}
			
			base.OnThink();
		}
		
		public void DoMinorSummon()
		{
			this.m_TotalSummons += 1;
			SummonedJukaGolem golem = new SummonedJukaGolem(this);
			if (this.m_Summon1 == null)
				this.m_Summon1 = golem;
			else if (this.m_Summon2 == null)
				this.m_Summon2 = golem;
			else if (this.m_Summon3 == null)
				this.m_Summon3 = golem;
			else if (this.m_Summon4 == null)
				this.m_Summon4 = golem;
			Moongate item = new Moongate();
			item.Hue = 32;
			item.Dispellable = false;
			item.Movable = false;
			item.MoveToWorld(this.Location, this.Map);
			Effects.PlaySound(this.Location, this.Map, 0x20E);
			string toSay = "Curses! Another defective minion!";
			switch(Utility.Random(1, 4))
			{
				case 0:
				{
					toSay = ("Curses! Another defective minion!");
					break;
				}
				case 1:
				{
					toSay = ("Curses! Another defective minion!");
					break;
				}
				case 2:
				{
					toSay = ("The engineer who sent this garbage through is DEAD!");
					break;
				}
				case 3:
				{
					toSay = ("Did they just grab this off the scrap heap!?");
					break;
				}
				case 4:
				{
					toSay = ("You are ALL FIRED!");
					break;
				}
			}
			Timer.DelayCall(TimeSpan.FromSeconds(5.0), delegate{this.Say(toSay); golem.MoveToWorld(item.Location, this.Map); item.Delete(); golem.Combatant = this.Combatant;});
		}
		
		public void DoMassSummon()
		{
			if ( this.m_Summon1 != null )
				this.m_Summon1.Delete();
			if ( this.m_Summon2 != null )
				this.m_Summon2.Delete();
			if ( this.m_Summon3 != null )
				this.m_Summon3.Delete();
			if ( this.m_Summon4 != null )
				this.m_Summon4.Delete();
			SummonedJukaGolem golem1 = new SummonedJukaGolem(this);
			this.m_Summon1 = golem1;
			SummonedJukaGolem golem2 = new SummonedJukaGolem(this);
			this.m_Summon2 = golem2;
			SummonedJukaGolem golem3 = new SummonedJukaGolem(this);
			this.m_Summon3 = golem3;
			SummonedJukaGolem golem4 = new SummonedJukaGolem(this);
			this.m_Summon4 = golem4;
			this.m_TotalSummons += 4;
			Moongate item1 = new Moongate(new Point3D(this.X + 2, this.Y, this.Z), this.Map);
			item1.Hue = 32;
			item1.Dispellable = false;
			item1.Movable = false;
			item1.MoveToWorld(new Point3D(this.X + 2, this.Y, this.Z), this.Map);
			Moongate item2 = new Moongate(new Point3D(this.X - 2, this.Y, this.Z), this.Map);
			item2.Name = "Moongate";
			item2.Hue = 32;
			item2.ItemID = 0xF6C;
			item2.Movable = false;
			item2.MoveToWorld(new Point3D(this.X - 2, this.Y, this.Z), this.Map);
			Moongate item3 = new Moongate(new Point3D(this.X, this.Y + 2, this.Z), this.Map);
			item3.Name = "Moongate";
			item3.Hue = 32;
			item3.ItemID = 0xF6C;
			item3.Movable = false;
			item3.MoveToWorld(new Point3D(this.X, this.Y + 2, this.Z), this.Map);
			Moongate item4 = new Moongate(new Point3D(this.X, this.Y - 2, this.Z), this.Map);
			item4.Name = "Moongate";
			item4.Hue = 32;
			item4.ItemID = 0xF6C;
			item4.Movable = false;
			item4.MoveToWorld(new Point3D(this.X, this.Y - 2, this.Z), this.Map);
			Effects.PlaySound(this.Location, this.Map, 0x20E);
			Timer.DelayCall(TimeSpan.FromSeconds(5.0), delegate{this.Say("Oh, blast it all! GET THEM!"); golem1.MoveToWorld(item1.Location, this.Map); golem1.Combatant = this.Combatant; golem2.MoveToWorld(item2.Location, item2.Map); golem2.Combatant = this.Combatant; golem3.MoveToWorld(item3.Location, this.Map); golem3.Combatant = this.Combatant; golem4.MoveToWorld(item4.Location, this.Map); golem4.Combatant = this.Combatant; item1.Delete(); item2.Delete(); item3.Delete(); item4.Delete();});
		}
		
		public void DoReset()
		{
			if ( this.m_Summon1 != null )
				this.m_Summon1.Delete();
			if ( this.m_Summon2 != null )
				this.m_Summon2.Delete();
			if ( this.m_Summon3 != null )
				this.m_Summon3.Delete();
			if ( this.m_Summon4 != null )
				this.m_Summon4.Delete();
			this.m_TotalSummons = 0;
			this.Hits = this.HitsMax;
			this.Say("Who wishes to challenge me next!?");
		}

        public JukaGolemCommander(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }
		
		public override int TreasureMapLevel { get { return 5; } }
		
		public override bool Unprovokable
        {
            get
            {
                return true;
            }
        }
        public override bool ReacquireOnMovement
        {
            get
            {
                return true;
            }
        }
        public override bool Uncalmable
        {
            get
            {
                return true;
            }
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosSuperBoss, 4);
            AddLoot(LootPack.HighScrolls, 10);
			AddLoot(LootPack.Gems, 15);
        }

        public override int GetIdleSound()
        {
            return 0x1AC;
        }

        public override int GetAngerSound()
        {
            return 0x1CD;
        }

        public override int GetHurtSound()
        {
            return 0x1D0;
        }

        public override int GetDeathSound()
        {
            return 0x28D;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
			writer.Write((Mobile)m_Summon1);
			writer.Write((Mobile)m_Summon2);
			writer.Write((Mobile)m_Summon3);
			writer.Write((Mobile)m_Summon4);
			writer.Write((int)m_TotalSummons);
			writer.Write((TimeSpan)m_ResetDelay);
			writer.Write((DateTime)m_LastTimeDamaged);
			
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
			m_Summon1 = reader.ReadMobile();
			m_Summon2 = reader.ReadMobile();
			m_Summon3 = reader.ReadMobile();
			m_Summon4 = reader.ReadMobile();
			m_TotalSummons = reader.ReadInt();
			m_ResetDelay = reader.ReadTimeSpan();
			m_LastTimeDamaged = reader.ReadDateTime();
        }
    }
}
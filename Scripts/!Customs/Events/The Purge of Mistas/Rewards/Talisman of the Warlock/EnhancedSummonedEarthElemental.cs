using System;
using Server;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("an earth elemental corpse")]
    public class EnhancedSummonedEarthElemental : SummonedEarthElemental
    {
		private DateTime m_NextAbility;
        [Constructable]
        public EnhancedSummonedEarthElemental()
            : base()
        {
            this.Name = "a greater earth elemental";
            this.Body = 829;
            this.BaseSoundID = 268;

            this.SetStr(250);
            this.SetDex(100);
            this.SetInt(100);

            this.SetHits(750);

            this.SetDamage(10, 15);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 80, 80);
            this.SetResistance(ResistanceType.Fire, 50, 50);
            this.SetResistance(ResistanceType.Cold, 50, 50);
            this.SetResistance(ResistanceType.Poison, 50, 50);
            this.SetResistance(ResistanceType.Energy, 50, 50);

            this.SetSkill(SkillName.MagicResist, 100.0);
            this.SetSkill(SkillName.Tactics, 120.0);
            this.SetSkill(SkillName.Wrestling, 120.0);
			this.SetSkill(SkillName.Anatomy, 100.0);
			this.SetSkill(SkillName.Parry, 100.0);

            this.VirtualArmor = 50;
            this.ControlSlots = 4;
        }
		
		public override int GetAttackSound()
        {
            return 0x627;
        }

        public override int GetHurtSound()
        {
            return 0x629;
        }
		
		public override void OnThink()
		{
			//taunt ability
			if (DateTime.UtcNow > this.m_NextAbility && this.Combatant != null)
			{
				this.Say("*smashes the earth*");
				this.m_NextAbility = DateTime.UtcNow + TimeSpan.FromSeconds(30.0);
				foreach (var id in SpellHelper.AcquireIndirectTargets(this, this.Location, this.Map, 10))
                {
					Mobile m = id as Mobile;
                    int damage = Utility.RandomMinMax(20, 30);
                    this.DoHarmful(id);
					this.MovingEffect(m, 0x1363, 12, 1, false, true, 0, 0);
					this.PlaySound(0x64B);
                    SpellHelper.Damage(TimeSpan.FromSeconds(2.0), m, damage);
					if ( m is BaseCreature )
						m.Combatant = this;
				}
			}
			
			base.OnThink();
		}

        public EnhancedSummonedEarthElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty
        {
            get
            {
                return 200.0;
            }
        }
        public override double DispelFocus
        {
            get
            {
                return 90.0;
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
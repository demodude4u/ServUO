using System;
using Server;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("an air elemental corpse")]
    public class EnhancedSummonedAirElemental : SummonedAirElemental
    {
		private DateTime m_NextAbility;
        [Constructable]
        public EnhancedSummonedAirElemental()
            : base()
        {
            this.Name = "a greater air elemental";
            this.Body = 4;
			Hue = 0x4001;
            this.BaseSoundID = 655;

            this.SetStr(125);
            this.SetDex(125);
            this.SetInt(200);

            this.SetHits(500);
            this.SetStam(50);
			this.SetMana(300);

            this.SetDamage(5, 10);

            this.SetDamageType(ResistanceType.Physical, 50);
            this.SetDamageType(ResistanceType.Energy, 50);

            this.SetResistance(ResistanceType.Physical, 40, 50);
            this.SetResistance(ResistanceType.Fire, 30, 40);
            this.SetResistance(ResistanceType.Cold, 35, 45);
            this.SetResistance(ResistanceType.Poison, 50, 60);
            this.SetResistance(ResistanceType.Energy, 70, 80);

            this.SetSkill(SkillName.Meditation, 90.0);
            this.SetSkill(SkillName.EvalInt, 100.0);
            this.SetSkill(SkillName.Magery, 100.0);
            this.SetSkill(SkillName.MagicResist, 90.0);
            this.SetSkill(SkillName.Tactics, 100.0);
            this.SetSkill(SkillName.Wrestling, 90.0);

            this.VirtualArmor = 40;
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
			//mana restore ability
			if (DateTime.UtcNow > this.m_NextAbility && this.ControlMaster != null && this.ControlMaster.Mana < this.ControlMaster.ManaMax / 2)
			{
				this.Say("*calls forth a rejuvinating wind*");
				this.m_NextAbility = DateTime.UtcNow + TimeSpan.FromSeconds(30.0);
				this.ControlMaster.Mana += this.ControlMaster.ManaMax / 20;
				if ( this.ControlMaster is PlayerMobile)
					this.ControlMaster.SendMessage(0, "You feel your mana rapidly recovering.");
				this.Mana += 100;
				this.ControlMaster.FixedEffect(0x374A, 10, 20);
                this.ControlMaster.PlaySound(0x5C9);
				for ( int i = 0; i < 4; i++)
				{
					Timer.DelayCall(TimeSpan.FromSeconds(i * 2), delegate{ this.ControlMaster.Mana += this.ControlMaster.ManaMax / 20; this.ControlMaster.FixedEffect(0x374A, 10, 20); this.ControlMaster.PlaySound(0x5C9);});
				}
			}
			
			base.OnThink();
		}

        public EnhancedSummonedAirElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty
        {
            get
            {
                return 180.0;
            }
        }
        public override double DispelFocus
        {
            get
            {
                return 80.0;
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
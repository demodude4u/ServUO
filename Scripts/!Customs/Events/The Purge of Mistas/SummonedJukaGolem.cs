using System;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a golem corpse")]
    public class SummonedJukaGolem : Golem
    {
		private JukaGolemCommander m_Summoner;
		
        [Constructable]
        public SummonedJukaGolem(JukaGolemCommander summoner)
            : base()
        {    
			switch(Utility.Random(1, 3))
			{
				case 0:
				{
					Name = "a rusty iron golem";
					Body = 752;
					break;
				}
				case 1:
				{
					Name = "a rusty iron golem";
					Body = 752;
					break;
				}
				case 2:
				{
					Name = "a crumbling stone golem";
					Body = 829;
					break;
				}
				case 3:
				{
					Name = "a faulty exodus construct";
					Body = 0x2F5;
					break;
				}
			}
			
			m_Summoner = summoner;
			Summoned = true;
			SummonMaster = summoner;
			Controlled = true;
			ControlMaster = summoner;
			
			SetStr(125, 125);
            SetDex(125, 125);
            SetInt(125, 125);

            SetDamage(15, 20);
			SetHits(200);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 50, 50);
            SetResistance(ResistanceType.Cold, 50, 50);
            SetResistance(ResistanceType.Energy, 0);
			SetResistance(ResistanceType.Poison, 50, 50);
			SetResistance(ResistanceType.Fire, 50, 50);
			
			SetSkill(SkillName.Anatomy, 80.0, 90.0);
            SetSkill(SkillName.Tactics, 95.0, 100.0);
            SetSkill(SkillName.Wrestling, 95.0, 100.0);

            SetSpecialAbility(SpecialAbility.ColossalBlow);
        }

        public SummonedJukaGolem(Serial serial)
            : base(serial)
        {
        }
        public override bool DeleteOnRelease { get { return true; } }
        public override bool BleedImmune { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public override int GetAngerSound()
        {
			if (this.Body == 0x2F5)
				return 0x26C;
			else
				return 541;
        }

        public override int GetIdleSound()
        {
            if (this.Body == 0x2F5)
				return 0x211;
			else
                return 542;
        }

        public override int GetDeathSound()
        {
            if (this.Body == 0x2F5)
				return 0x211;
			else
                return 545;
        }

        public override int GetAttackSound()
        {
			if (this.Body == 829)
				return 0x627;
			else if (this.Body == 0x2F5)
				return 0x232;
			else
				return 562;
        }

        public override int GetHurtSound()
        {
            if (this.Body == 829)
				return 0x629;
			else if (this.Body == 0x2F5)
				return 0x140;
			else
                return 320;
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            base.OnDamage(amount, from, willKill);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
			writer.Write((Mobile)m_Summoner);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
			m_Summoner = reader.ReadMobile() as JukaGolemCommander;
        }
    }
}

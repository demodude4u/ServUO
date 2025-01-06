using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Items;

namespace Server.Mobiles
{
    public abstract class CrimeGuard : BaseHire
    {
		private bool m_IsBribable;
		public bool IsBribable { get { return m_IsBribable; } set { m_IsBribable = value; } }
		
        public CrimeGuard(Mobile target)
        {
            GuardImmune = true;

            if (target != null)
            {
				if (this is TheftGuard)
				{
					InitStats(125, 150, 75);
					m_IsBribable = Utility.Random(100) > 35;
				}
				else if (this is AssaultGuard || this is SEAssaultGuard)
				{
					InitStats(150, 175, 75);
					m_IsBribable = Utility.Random(100) > 55;
				}
				else if (this is MurderGuard)
				{
					InitStats(200, 150, 125);
					m_IsBribable = Utility.Random(100) > 85;
				}
                Location = target.Location;
                Map = target.Map;

                Effects.SendLocationParticles(EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 5023);
            }
        }

        public CrimeGuard(Serial serial)
            : base(serial)
        {
        }
		
        public override bool Payday(BaseHire m) 
        {
			return false;
		}

        public override void OnSpeech(SpeechEventArgs e) 
        {
			
		}
		
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
			list.Add(new BribeEntry(from, this));
		}

        private class BribeEntry : ContextMenuEntry
        {
            private Mobile m_From;
            private CrimeGuard m_Guard;

            public BribeEntry(Mobile from, CrimeGuard guard)
                : base(1152294, 2)
            {
                m_From = from;
                m_Guard = guard;
            }

            public override void OnClick()
            {
                if (!m_From.InRange(m_Guard.Location, 2) || !(m_From is PlayerMobile))
                    return;
				
				if (!m_Guard.IsBribable)
				{
					m_Guard.Say("Who do you think you are dealing with?!?");
                    return;
				}
				
				int bribe = Utility.Random(500) + 500;
				
				if (m_Guard is MurderGuard) bribe = bribe * 3;
				
				if (m_From is PlayerMobile && m_From == m_Guard.Focus)
				{
					Container pack = m_From.Backpack;
					if (pack != null)
					{
						if (pack.ConsumeTotal(typeof(Gold), bribe))
						{
							m_Guard.Say("Nice doing business with you, friend!");
							// do the thing...
						}
						else
						{
							m_Guard.Say("What do I look like to you, a rock, fella?");
						}
					}
				}
            }
        }

        public abstract Mobile Focus { get; set; }
		
        [CommandProperty(AccessLevel.GameMaster)]
		public string VendorName { get; set; }

        public override bool CanBeHarmful(IDamageable target, bool message, bool ignoreOurBlessedness)
        {
            if (target is Mobile && ((Mobile)target).GuardImmune)
            {
                return false;
            }

            return base.CanBeHarmful(target, message, ignoreOurBlessedness);
        }

        public static void Spawn(Mobile caller, Mobile target)
        {
            Spawn(caller, target, 1, false);
        }

        public static void Spawn(Mobile caller, Mobile target, int amount, bool onlyAdditional)
        {
            if (target == null || target.Deleted || target.GuardImmune)
                return;

            IPooledEnumerable eable = target.GetMobilesInRange(15);

            foreach (Mobile m in eable)
            {
                if (m is CrimeGuard)
                {
                    CrimeGuard g = (CrimeGuard)m;

                    if (g.Focus == null) // idling
                    {
                        g.Focus = target;

                        --amount;
                    }
                    else if (g.Focus == target && !onlyAdditional)
                    {
                        --amount;
                    }
                }
            }

            eable.Free();

            while (amount-- > 0)
                caller.Region.MakeGuard(target);
        }

        public override bool OnBeforeDeath()
        {
            Effects.SendLocationParticles(EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);

            PlaySound(0x1FE);

            Delete();

            return false;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
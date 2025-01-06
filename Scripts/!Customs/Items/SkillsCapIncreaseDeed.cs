using Server.Mobiles;
using System;

namespace Server.Items
{
    public class SkillsCapIncreaseDeed : Item
    {
        private int m_NewValue;

        [CommandProperty(AccessLevel.Administrator)]
        public int NewValue { get { return m_NewValue; } set { m_NewValue = value; } }

        [Constructable]
        public SkillsCapIncreaseDeed() : this(200)
        {
        }

        public override string DefaultName
        {
            get { return string.Format("a Custom Skills Cap Deed: {0:F1}", (double)(m_NewValue / 10)); }
        }

        [Constructable]
        public SkillsCapIncreaseDeed(int newvalue) : base(5360)
        {
            LootType = LootType.Blessed;
            Hue = 1161;
            m_NewValue = newvalue;
        }

        public SkillsCapIncreaseDeed(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this.IsChildOf(from.Backpack))
            {
                if (from is PlayerMobile)
                {
                    PlayerMobile pm = from as PlayerMobile;
                    if (pm._SkillsCapCustomValue > 0) pm._SkillsCapCustomValue += m_NewValue;
                    else pm._SkillsCapCustomValue = from.SkillsCap + m_NewValue;

                    from.SkillsCap = pm._SkillsCapCustomValue;
                    from.SendMessage("Your skills cap has increased by {0} to {1}!", m_NewValue, from.SkillsCap);
                }
                Delete();
            }
            else
            {
                from.SendMessage(1173, "This deed must be in your backpack to be used.");
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write((int)m_NewValue);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_NewValue = reader.ReadInt();
        }
    }
}


using Server.Gumps;
using Server.Mobiles;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Server.Items
{
    public class CustomSkillsCapDeed : Item
    {
        private int m_NewValue;

        [CommandProperty(AccessLevel.Administrator)]
        public int NewValue { get { return m_NewValue; } set { m_NewValue = value; } }

        [Constructable]
        public CustomSkillsCapDeed() : this(9200)
        {
        }

        public override string DefaultName
        {
            get {  return string.Format("a Custom Skills Cap Deed: {0:F1}", (double)(m_NewValue / 10)); }
        }

        [Constructable]
        public CustomSkillsCapDeed(int newvalue) : base(5360)
        {
            LootType = LootType.Blessed;
            Hue = 1161;
            m_NewValue = newvalue;
        }

        public CustomSkillsCapDeed(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.SkillsCap == m_NewValue)
            {
                from.SendMessage("Your Skills cap is already at the level of this deed.");
            }
            else if (this.IsChildOf(from.Backpack))
            {
                string change = (from.SkillsCap > m_NewValue) ? "LOWERED" : "INCREASED";
                from.SendGump(new ConfirmGump(string.Format("Your skills cap will be {0} from {1} to {2}!", change, from.SkillsCap, m_NewValue),
                    new ConfirmGumpCallback(ChangeSkillsCap_Callback)));
            }
            else
            {
                from.SendMessage(1173, "This deed must be in your backpack to be used.");
            }
        }

        public void ChangeSkillsCap_Callback(Mobile from, bool okay)
        {
            if (okay)
            {
                if (from is PlayerMobile)
                    ((PlayerMobile)from)._SkillsCapCustomValue = m_NewValue;
                from.SkillsCap = m_NewValue;
                from.SendMessage("Your skills cap has been changed to {0}!", m_NewValue.ToString());
                Delete();
            }
            else
                from.SendMessage("You decide not to use the deed.");
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


using System;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
    public class HumanBeardDye : Item
    {
        [Constructable]
        public HumanBeardDye()
            : base(0x1848)
        {
	        Name = "Human Beard Dye";
	        Hue = 51;
            this.Weight = 1.0;
            this.LootType = LootType.Newbied;
        }

        public HumanBeardDye(Serial serial)
            : base(serial)
        {
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

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(this.GetWorldLocation(), 1))
            {
                from.CloseGump(typeof(HumanBeardDyeGump));
                from.SendGump(new HumanBeardDyeGump(this));
            }
            else
            {
                from.LocalOverheadMessage(MessageType.Regular, 906, 1019045); // I can't reach that.
            }
        }
    }

    public class HumanBeardDyeGump : Gump
    {
        private static readonly HumanBeardDyeEntry[] m_Entries = new HumanBeardDyeEntry[]
        {
            new HumanBeardDyeEntry("Human 1", 0x44E, 24),
            new HumanBeardDyeEntry("Human 2", 0x466, 24)
        };
        private readonly HumanBeardDye m_HumanBeardDye;
        public HumanBeardDyeGump(HumanBeardDye dye)
            : base(0, 0)
        {
            this.m_HumanBeardDye = dye;

            this.AddPage(0);
            this.AddBackground(150, 60, 350, 358, 2600);
            this.AddBackground(170, 104, 110, 270, 5100);
            this.AddHtmlLocalized(230, 75, 200, 20, 1011013, false, false);		// Hair Color Selection Menu
            this.AddHtmlLocalized(235, 380, 300, 20, 1013007, false, false);		// Dye my beard this color!
            this.AddButton(200, 380, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0);        // DYE HAIR

            for (int i = 0; i < m_Entries.Length; ++i)
            {
                this.AddLabel(180, 109 + (i * 22), m_Entries[i].HueStart - 1, m_Entries[i].Name);
                this.AddButton(257, 110 + (i * 22), 5224, 5224, 0, GumpButtonType.Page, i + 1);
            }

            for (int i = 0; i < m_Entries.Length; ++i)
            {
                HumanBeardDyeEntry e = m_Entries[i];

                this.AddPage(i + 1);

                for (int j = 0; j < e.HueCount; ++j)
                {
                    this.AddLabel(328 + ((j / 16) * 80), 102 + ((j % 16) * 17), e.HueStart + j - 1, "*****");
                    this.AddRadio(310 + ((j / 16) * 80), 102 + ((j % 16) * 17), 210, 211, false, (i * 100) + j);
                }
            }
        }

        public override void OnResponse(NetState from, RelayInfo info)
        {
            if (this.m_HumanBeardDye.Deleted)
                return;

            Mobile m = from.Mobile;
            int[] switches = info.Switches;

            if (!this.m_HumanBeardDye.IsChildOf(m.Backpack))
            {
                m.SendLocalizedMessage(1042010); //You must have the objectin your backpack to use it.
                return;
            }

            if (info.ButtonID != 0 && switches.Length > 0)
            {
                if (m.FacialHairItemID == 0)
                {
                    m.SendLocalizedMessage(502623);	// You have no hair to dye and cannot use this
                }
                else
                {
                    // To prevent this from being exploited, the hue is abstracted into an internal list
                    int entryIndex = switches[0] / 100;
                    int hueOffset = switches[0] % 100;

                    if (entryIndex >= 0 && entryIndex < m_Entries.Length)
                    {
                        HumanBeardDyeEntry e = m_Entries[entryIndex];

                        if (hueOffset >= 0 && hueOffset < e.HueCount)
                        {
                            int hue = e.HueStart + hueOffset;

                            m.FacialHairHue = hue;

                            m.SendLocalizedMessage(501199);  // You dye your hair
							this.m_HumanBeardDye.Delete();
                            m.PlaySound(0x4E);
                        }
                    }
                }
            }
            else
            {
                m.SendLocalizedMessage(501200); // You decide not to dye your hair
            }
        }

        private class HumanBeardDyeEntry
        {
            private readonly string m_Name;
            private readonly int m_HueStart;
            private readonly int m_HueCount;
            public HumanBeardDyeEntry(string name, int hueStart, int hueCount)
            {
                this.m_Name = name;
                this.m_HueStart = hueStart;
                this.m_HueCount = hueCount;
            }

            public string Name
            {
                get
                {
                    return this.m_Name;
                }
            }
            public int HueStart
            {
                get
                {
                    return this.m_HueStart;
                }
            }
            public int HueCount
            {
                get
                {
                    return this.m_HueCount;
                }
            }
        }
    }
}
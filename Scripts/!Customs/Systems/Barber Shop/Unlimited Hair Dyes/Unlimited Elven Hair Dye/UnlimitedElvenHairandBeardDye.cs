using System;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
    public class UnlimitedElvenHairandBeardDye : Item
    {
        [Constructable]
        public UnlimitedElvenHairandBeardDye()
            : base(0x1847)
        {
	        Name = "Elven Hair and Beard Dye [Unlimited Use]";
            this.Weight = 1.0;
        }

        public UnlimitedElvenHairandBeardDye(Serial serial)
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
                from.CloseGump(typeof(UnlimitedElvenHairandBeardDyeGump));
                from.SendGump(new UnlimitedElvenHairandBeardDyeGump(this));
            }
            else
            {
                from.LocalOverheadMessage(MessageType.Regular, 906, 1019045); // I can't reach that.
            }
        }
    }

    public class UnlimitedElvenHairandBeardDyeGump : Gump
    {
        private static readonly UnlimitedElvenHairandBeardDyeEntry[] m_Entries = new UnlimitedElvenHairandBeardDyeEntry[]
        {
            new UnlimitedElvenHairandBeardDyeEntry("Elven Hair 1", 0x034, 10),
            new UnlimitedElvenHairandBeardDyeEntry("Elven Hair 2", 0x058, 10),
            new UnlimitedElvenHairandBeardDyeEntry("Elven Hair 3", 0x08e, 10),
            new UnlimitedElvenHairandBeardDyeEntry("Elven Hair 4", 0x090, 10),
            new UnlimitedElvenHairandBeardDyeEntry("Elven Hair 5", 0x101, 10),
            new UnlimitedElvenHairandBeardDyeEntry("Elven Hair 6", 0x159, 10),
            new UnlimitedElvenHairandBeardDyeEntry("Elven Hair 7", 0x15A, 10),
            new UnlimitedElvenHairandBeardDyeEntry("Elven Hair 8", 0x320, 10)
        };
        private readonly UnlimitedElvenHairandBeardDye m_UnlimitedElvenHairandBeardDye;
        public UnlimitedElvenHairandBeardDyeGump(UnlimitedElvenHairandBeardDye dye)
            : base(50, 50)
        {
            this.m_UnlimitedElvenHairandBeardDye = dye;

            this.AddPage(0);

            this.AddBackground(100, 10, 350, 355, 2600);
            this.AddBackground(120, 54, 110, 270, 5100);

            this.AddHtmlLocalized(70, 25, 400, 35, 1011013, false, false); // <center>Hair Color Selection Menu</center>

            this.AddButton(149, 328, 4005, 4007, 1, GumpButtonType.Reply, 0);
            this.AddHtmlLocalized(185, 329, 250, 35, 1011014, false, false); // Dye my hair this color!

            for (int i = 0; i < m_Entries.Length; ++i)
            {
                this.AddLabel(130, 59 + (i * 22), m_Entries[i].HueStart - 1, m_Entries[i].Name);
                this.AddButton(207, 60 + (i * 22), 5224, 5224, 0, GumpButtonType.Page, i + 1);
            }

            for (int i = 0; i < m_Entries.Length; ++i)
            {
                UnlimitedElvenHairandBeardDyeEntry e = m_Entries[i];

                this.AddPage(i + 1);

                for (int j = 0; j < e.HueCount; ++j)
                {
                    this.AddLabel(278 + ((j / 16) * 80), 52 + ((j % 16) * 17), e.HueStart + j - 1, "*****");
                    this.AddRadio(260 + ((j / 16) * 80), 52 + ((j % 16) * 17), 210, 211, false, (i * 100) + j);
                }
            }
        }

        public override void OnResponse(NetState from, RelayInfo info)
        {
            if (this.m_UnlimitedElvenHairandBeardDye.Deleted)
                return;

            Mobile m = from.Mobile;
            int[] switches = info.Switches;

            if (!this.m_UnlimitedElvenHairandBeardDye.IsChildOf(m.Backpack)) 
            {
                m.SendLocalizedMessage(1042010); //You must have the objectin your backpack to use it.
                return;
            }

            if (info.ButtonID != 0 && switches.Length > 0)
            {
                if (m.HairItemID == 0 && m.FacialHairItemID == 0)
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
                        UnlimitedElvenHairandBeardDyeEntry e = m_Entries[entryIndex];

                        if (hueOffset >= 0 && hueOffset < e.HueCount)
                        {
                            int hue = e.HueStart + hueOffset;

                            m.HairHue = hue;
                            m.FacialHairHue = hue;

                            m.SendLocalizedMessage(501199);  // You dye your hair
//							this.m_UnlimitedElvenHairandBeardDye.Delete();
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

        private class UnlimitedElvenHairandBeardDyeEntry
        {
            private readonly string m_Name;
            private readonly int m_HueStart;
            private readonly int m_HueCount;
            public UnlimitedElvenHairandBeardDyeEntry(string name, int hueStart, int hueCount)
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
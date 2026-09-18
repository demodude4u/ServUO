//THIS
//HAIR
//FACE HAIR
// SKILLS
// SKILL
// ADVANCED SKILLS
// EQUIP
// MAP
using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Misc;
using Server.Mobiles;
using Server.Accounting;
using Server.Items;
using Server.CharacterCreator;
using System.Collections.Generic;

namespace Server.Gumps
{
    public class CharacterCreatorGump : Gump
    {
        int switchid = 10000;
        int buttonid = 20000;
        int selectedColor = 0;
        int[] colors = new int[] { };
        int shoesHue = 0;
        int pantsHue = 0;
        int shirtHue = 0;
        int skinHue = 0;
        int raceID = 0;
        bool IsFemale = false;
        int colorcount = 0;
            private readonly int _DisplayPage;
        private int[] HumanSkinHues = new int[]
            {1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018,
             1019, 1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 1030, 1031, 1032, 1033, 1034, 1035,
             1036, 1037, 1038, 1039, 1040, 1041, 1042, 1043, 1044, 1045, 1046, 1047, 1048};

        private int[] ElfSkinHues = new int[]
            {191, 589, 590, 591, 851, 865, 871, 884, 885, 886, 897, 898, 899, 900, 901, 905, 990, 997, 998, 999,
            1000, 1001, 1072, 1191, 1246, 1309, 1343, 1401, 1899, 1900, 2101, 2307};

        private int[] GargoyleSkinHues = new int[]
            {1755, 1756, 1757, 1758, 1759, 1760, 1761, 1762, 1763, 1764, 1765, 1766, 1767, 1768, 1769, 1770, 1771,
             1772, 1773, 1774, 1775, 1776, 1777, 1778, 1779, 1256, 1257, 1258, 1259};

        private int[] OrcSkinHues = new int[]
            {2202, 2203, 2204, 2205, 2206, 2207, 2208, 2209, 2210, 2211, 2214, 2215, 2216, 2217, 2218, 2220, 2221,
             2222, 2223, 2224};

        public CharacterCreatorGump(Mobile from) : this(50, 50, from, false, 0, "", 0, 0, "", 0, 0, 0) { }
        public CharacterCreatorGump(Mobile from, bool isfemale, int raceid, string playername, int selectedcolor, int hue, string message, int shirthue, int pantshue, int shoehue, int displayPage = 1)
            : this(50, 50, from, isfemale, raceid, playername, selectedcolor, hue, message, shirthue, pantshue, shoehue, displayPage)
        {
        }

        public CharacterCreatorGump(int xPosition, int yPosition, Mobile from, bool isfemale, int raceid, string playername, int selectedcolor, int hue, string message, int shirthue, int pantshue, int shoehue, int displayPage = 1) : base(50, 50)
        {
            _DisplayPage = displayPage;
            IsFemale = isfemale;
            raceID = raceid;
            selectedColor = selectedcolor;

            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;

            AddPage(0);
            AddBackground(48, 5, 700, 500, 9270);
            AddImage(108, 112, 2501);

            if (raceid == 0)
                AddImage(280, 68, 1800);//Human Backdrop

            else if (raceid == 1)
                AddImage(280, 68, 1800);//Elf Backdrop

            else if (raceid == 2)
                AddImage(280, 68, 1800);//Gargoyle Backdrop

            else if (raceid == 3)
                AddImage(280, 68, 1800);//Orc Backdrop

            AddImage(716, 16, 10441);
            AddLabel(340, 36, 1153, @"Character Creator");
            AddButton(405, 460, 12009, 12011, (int)Buttons.ContinueButton, GumpButtonType.Reply, 0);
            AddBackground(521, 68, 199, 357, 9200);
            AddLabel(556, 87, 1153, @"Choose thy skin tone");
            AddImage(2, 16, 10440);
            AddImage(612, 133, 10152);
            AddImage(612, 148, 10151);
            AddImage(611, 162, 10151);
            AddImage(611, 177, 10151);
            AddImage(611, 192, 10151);
            AddImage(611, 207, 10151);
            AddImage(611, 222, 10151);
            AddImage(611, 237, 10151);
            AddImage(611, 252, 10151);
            AddImage(611, 267, 10151);
            AddImage(611, 282, 10151);
            AddImage(611, 297, 10151);
            AddImage(611, 311, 10151);
            AddImage(611, 326, 10151);
            AddImage(611, 341, 10151);
            AddImage(611, 356, 10154);

            AddLabel(118, 77, 1153, @"What is thy name?");
            AddTextEntry(121, 112, 118, 20, 0, 0, playername);

            AddLabel(112, 172, 1153, @"What is thy gender?");
            AddButton(95, 202, 1808, 1809, (int)Buttons.GenderMaleButton, GumpButtonType.Reply, 0);
            AddButton(185, 202, 1805, 1806, (int)Buttons.GenderFemaleButton, GumpButtonType.Reply, 0);

            AddLabel(113, 252, 1153, @"What race art thou?");

            AddLabel(128, 288, 1153, @"Human");
            AddButton(95, 282, 2151, 2151, (int)Buttons.HumanButton, GumpButtonType.Reply, 0);

            AddLabel(228, 288, 1153, @"Elf");
            AddButton(195, 282, 2151, 2151, (int)Buttons.ElfButton, GumpButtonType.Reply, 0);

            AddLabel(128, 318, 1153, @"Gargoyle");
            AddButton(95, 312, 2151, 2151, (int)Buttons.GargoyleButton, GumpButtonType.Reply, 0);

            AddLabel(228, 318, 1153, @"Orc");
            AddButton(195, 312, 2151, 2151, (int)Buttons.OrcButton, GumpButtonType.Reply, 0);
            /*
            AddButton(95, 232, 1794, 1795, (int)Buttons.HumanButton, GumpButtonType.Reply, 0);
            AddButton(185, 232, 1797, 1798, (int)Buttons.ElfButton, GumpButtonType.Reply, 0);
            AddButton(75, 262, 2003, 2004, (int)Buttons.GargoyleButton, GumpButtonType.Reply, 0);
            AddButton(185, 262, 11997, 11998, (int)Buttons.OrcButton, GumpButtonType.Reply, 0);
            
            AddBackground(75, 320, 180, 150, 3000);
            if(raceID == 0)
            AddHtml(85, 320, 170, 150,
                @"<basefont COLOR=#111111>Strong Back: Can carry an additional 60 stones of weight
 ----------------------
Tough: Receives +2 Hit Point Regeneration
 ----------------------
Workhorse: +1 Logs in Trammel, +2 Logs in Felucca, +1 Ore when mining at random, additional hides when skinning animals
 ----------------------
Jack of All Trades: 20% minimum on all skills (does not appear on skills menu) </font>", (bool)false, (bool)true);

            else if (raceID == 1)
                AddHtml(85, 320, 170, 150, @"<basefont COLOR=#111111>Night Sight: Night Sight is always enabled for Elves
 ----------------------
Infused with Magic: +5 Energy Resistance cap (75 total)
 ----------------------
Knowledge of Nature: Increased chance of acquiring colored ore and special boards
 ----------------------
Difficult to Track: Elves are more difficult to track than Humans
 ----------------------
Perception: Elves are better at detect hidden
 ----------------------
Wisdom: +20 Mana Increase </font>", (bool)false, (bool)true);

            else if (raceID == 2)
                AddHtml(85, 320, 170, 150, @"<basefont COLOR=#111111>Flying: Gargoyles cannot ride mounts, but can fly as fast as one can run
 ----------------------
Berserk: 15% Damage Bonus and +3% Spell Damage Increase for each 20% health lost from max health
 ----------------------
Master Artisan: Bonus magic unraveling and imbuing chance
 ----------------------
Deadly Aim: Minimum Throwing skill is passively 20% (does not appear on skills menu)
 ----------------------
Mystic Insight: Minimum Mysticism is 30% and Gargoyles receive +2 Mana Regeneration which stacks with Meditation and Focus</font>", (bool)false, (bool)true);

            else if (raceID == 3)
                AddHtml(85, 320, 170, 150, @"<basefont COLOR=#111111>
Swamp Dweller: Immune to Swamp Poison and has additional +5 Poison Resistance cap (75 total)
 ----------------------
Natural Born Killers: Adept in combat, bonus 20% skill in tactics
 ----------------------
Strong Back: Orcs receive a 10% weight reduction to all carried items
 ----------------------
Berserk: 15% Damage Bonus and +3% Spell Damage Increase for each 20% health lost from max health</font>", (bool)false, (bool)true);
*/
            AddLabel(112, 137, 38, message);

            //Left Columns
            //Left Radio 536, 142
            //Right Radio, 650, 142
            //22 spaces vertical distance (Y)
            //32 spaces horizontal between block/radio (X)

            //Button Next 692,394
            //Button Back 533,394
            //594,394 page text

            int leftx = 536;
            int rightx = 650;
            int origionaly = 142;
            int ypos = 142;
            int xpos = 536;
            int verticalspace = 22;
            int horizontalspace = 32;
            int pagenum = 2;
            int count = 0;

            if (shirthue == 0)
                shirtHue = Utility.RandomDyedHue();
            else
                shirtHue = shirthue;
            if (pantshue == 0)
                pantsHue = Utility.RandomDyedHue();
            else
                pantsHue = pantshue;
            if (shoehue == 0)
                shoesHue = Utility.RandomDyedHue();
            else
                shoesHue = shoehue;

            Point2D backbuttonlocation = new Point2D(533, 394);
            Point2D nextpagebuttonlocation = new Point2D(692, 394);
            Point2D pagetextlocation = new Point2D(594, 394);

            if (hue == 0)
            {
                switch (raceID)
                {
                    case 0:
                        {
                            skinHue = HumanSkinHues[0];
                            break;
                        }
                    case 1:
                        {
                            skinHue = ElfSkinHues[0];
                            break;
                        }
                    case 2:
                        {
                            skinHue = GargoyleSkinHues[0];
                            break;
                        }
                    case 3:
                        {
                            skinHue = OrcSkinHues[0];
                            break;
                        }
                }
            }
            else
            {
                skinHue = hue;
            }

            switch (raceID)
            {
                case 0://Human
                    {
                        if (!IsFemale)
                        {
                            //clothing
                            AddImage(282, 63, 1889, skinHue - 1); //character body
                            AddImage(282, 63, 1849, shirtHue); //shirt
                            AddImage(282, 63, 1848, pantsHue);//pants
                            AddImage(282, 63, 1890, shoesHue);//shoes

                        }
                        else//Female
                        {
                            //clothing
                            AddImage(282, 63, 1888, skinHue - 1); //character body
                            AddImage(282, 63, 1812, shirtHue); //shirt
                            AddImage(282, 63, 1891, shoesHue);//shoes
                            AddImage(282, 63, 1892, pantsHue);//skirt

                        }

                        colors = HumanSkinHues;

                        break;
                    }
                case 1://Elf
                    {
                        if (!IsFemale)
                        {
                            //clothing
                            AddImage(282, 63, 1894, skinHue - 1); //character body
                            AddImage(282, 63, 1849, shirtHue); //shirt
                            AddImage(282, 63, 1848, pantsHue);//pants
                            AddImage(282, 63, 1890, shoesHue);//shoes
                        }
                        else//female
                        {     //clothing
                            AddImage(282, 63, 1893, skinHue - 1); //character body
                            AddImage(282, 63, 1812, shirtHue); //shirt
                            AddImage(282, 63, 1891, shoesHue);//shoes
                            AddImage(282, 63, 1892, pantsHue);//skirt
                        }
                        colors = ElfSkinHues;
                        break;
                    }
                case 2://Gargoyle
                    {
                        if (!IsFemale)
                        {
                            //clothing
                            AddImage(282, 63, 1899, skinHue - 1); //character body
                            AddImage(282, 63, 1912, shirtHue); //robe
                        }
                        else//female
                        {
                            //clothing
                            AddImage(282, 63, 1898, skinHue - 1); //character body
                            AddImage(282, 63, 1912, shirtHue); //robe
                        }
                        colors = GargoyleSkinHues;
                        break;
                    }
                case 3://Orc
                    {
                        if (!IsFemale)
                        {
                            //clothing
                            AddImage(282, 63, 1889, skinHue - 1); //character body
                            AddImage(304, 74, 60416, skinHue - 1); //face
                            AddImage(282, 63, 1849, shirtHue); //shirt
                            AddImage(282, 63, 1848, pantsHue);//pants
                            AddImage(282, 63, 1890, shoesHue);//shoes
                        }
                        else//female
                        {
                            //clothing
                            AddImage(282, 63, 1888, skinHue - 1); //character body
                            AddImage(304, 74, 60416, skinHue - 1); //face
                            AddImage(282, 63, 1812, shirtHue); //shirt
                            AddImage(282, 63, 1891, shoesHue);//shoes
                            AddImage(282, 63, 1892, pantsHue);//skirt
                        }
                        colors = OrcSkinHues;
                        break;
                    }
            }
            AddPage(CharacterCreatorSystem.MapPage(1, _DisplayPage));
            for (int x = 0; x < colors.Length; x++)
            {

                count++;
                colorcount++;

                if (colorcount == 21 || colorcount == 41 || colorcount == 61)
                {
                    AddButton(nextpagebuttonlocation.X, nextpagebuttonlocation.Y, 5601, 5605, buttonid + pagenum, GumpButtonType.Page, CharacterCreatorSystem.MapPage(pagenum, _DisplayPage));
                    buttonid++;
                    if (pagenum == 2)
                        AddLabel(pagetextlocation.X, pagetextlocation.Y, 1153, "Page 1");
                    AddPage(CharacterCreatorSystem.MapPage(pagenum, _DisplayPage));
                    AddLabel(pagetextlocation.X, pagetextlocation.Y, 1153, "Page " + pagenum.ToString());
                    AddButton(backbuttonlocation.X, backbuttonlocation.Y, 5603, 5607, buttonid + pagenum, GumpButtonType.Page, CharacterCreatorSystem.MapPage(pagenum - 1, _DisplayPage));
                    buttonid++;
                    pagenum++;
                }
                if (selectedColor == x)
                {
                    AddButton(xpos, ypos, 209, 209, switchid + x, GumpButtonType.Reply, 0);
                }
                else
                {
                    AddButton(xpos, ypos, 208, 209, switchid + x, GumpButtonType.Reply, 0);
                }
                AddImage(xpos + horizontalspace, ypos, 210, colors[x] - 1);

                ypos += verticalspace;



                if (count == 20)
                {
                    count = 0;
                    xpos = leftx;
                    ypos = origionaly;

                }
                else if (count == 10)
                {
                    xpos = rightx;
                    ypos = origionaly;
                }

            }
        }

        public enum Buttons
        {
            CancelButton,
            GenderMaleButton,
            GenderFemaleButton,
            GargoyleButton,
            OrcButton,
            ContinueButton,
            HumanButton,
            ElfButton
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            TextRelay entry0 = info.GetTextEntry(0);
            string text0 = (entry0 == null ? "" : entry0.Text.Trim());

            if (info.ButtonID >= 10000)
            {
                int colornum = info.ButtonID - 10000;
                from.SendGump(new CharacterCreatorGump(this.X, this.Y, from, IsFemale, raceID, text0, colornum, colors[colornum], "", shirtHue, pantsHue, shoesHue, CharacterCreatorSystem.GetSelectionPage(colornum)));

            }
            else
            {
                switch (info.ButtonID)
                {
                    default:
                        {
                            //Nothing for close
                            break;
                        }
                    case (int)Buttons.GenderMaleButton:
                        {
                            from.SendGump(new CharacterCreatorGump(this.X, this.Y, from, false, raceID, text0, 0, 0, "", shirtHue, pantsHue, shoesHue));
                            break;
                        }
                    case (int)Buttons.GenderFemaleButton:
                        {
                            //from.Female = true;
                            from.SendGump(new CharacterCreatorGump(this.X, this.Y, from, true, raceID, text0, 0, 0, "", shirtHue, pantsHue, shoesHue));
                            break;
                        }
                    case (int)Buttons.GargoyleButton:
                        {
                            from.SendGump(new CharacterCreatorGump(this.X, this.Y, from, IsFemale, 2, text0, 0, 0, "", shirtHue, pantsHue, shoesHue));
                            break;
                        }
                    case (int)Buttons.OrcButton:
                        {
                            from.SendGump(new CharacterCreatorGump(this.X, this.Y, from, IsFemale, 3, text0, 0, 0, "", shirtHue, pantsHue, shoesHue));
                            break;
                        }
                    case (int)Buttons.ContinueButton:
                        {

                            if (text0 != "")
                            {
                                if (CheckDupe(from, text0))
                                {
                                    from.SendGump(new CharacterCreatorHairGump(this.X, this.Y, from, selectedColor, text0, IsFemale, skinHue, raceID, shirtHue, pantsHue, shoesHue, 0, 0));
                                }
                                else
                                {
                                    from.SendGump(new CharacterCreatorGump(this.X, this.Y, from, IsFemale, raceID, "", selectedColor, skinHue, "Name is already taken!!", shirtHue, pantsHue, shoesHue));
                                }
                            }
                            else
                            {
                                from.SendGump(new CharacterCreatorGump(this.X, this.Y, from, IsFemale, raceID, "", selectedColor, skinHue, "Name cannot be blank!!", shirtHue, pantsHue, shoesHue));
                            }

                            break;
                        }
                    case (int)Buttons.HumanButton:
                        {
                            from.SendGump(new CharacterCreatorGump(this.X, this.Y, from, IsFemale, 0, text0, 0, 0, "", shirtHue, pantsHue, shoesHue));
                            break;
                        }
                    case (int)Buttons.ElfButton:
                        {
                            from.SendGump(new CharacterCreatorGump(this.X, this.Y, from, IsFemale, 1, text0, 0, 0, "", shirtHue, pantsHue, shoesHue));
                            break;
                        }
                }
            }
        }

        #region Static Methods
        public static void AddGenericCharacter(Account a)
        {

            Mobile newChar = CreateMobile(a);

            newChar.Player = true;
            newChar.AccessLevel = AccessLevel.Player;
            newChar.Female = false;
            //newChar.Body = newChar.Female ? 0x191 : 0x190;
            newChar.Race = Race.DefaultRace;

            //newChar.Hue = Utility.ClipSkinHue( args.Hue & 0x3FFF ) | 0x8000;
            newChar.Hue = 1;

            newChar.Hunger = 20;

            bool young = false;

            if (newChar is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)newChar;

                if (pm.IsPlayer() && ((Account)pm.Account).Young)
                    young = pm.Young = true;
            }

            newChar.Name = "Generic Player";

            Backpack pack = new Backpack();
            pack.Movable = false;
            newChar.EquipItem(pack);


            if (young)
            {
                NewPlayerTicket ticket = new NewPlayerTicket();
                ticket.Owner = newChar;
                newChar.BankBox.DropItem(ticket);
            }

            newChar.MoveToWorld(new Point3D(5272, 1076, 0), Map.Trammel);

            //XmlAttach.AttachTo(newChar, new XmlMobFactions());

            newChar.CantWalk = true;
            newChar.Frozen = true;
            newChar.Squelched = true;
            newChar.BodyValue = 1;
        }
        public static bool CheckDupe(Mobile m, string name)
        {
            if (m == null || name == null || name.Length == 0)
                return false;

            name = name.Trim(); //Trim the name and re-assign it

            if (!NameVerification.Validate(name, 2, 16, true, true, true, 1, NameVerification.SpaceDashPeriodQuote))
                return false;
            else
                return true;
        }
        private static Mobile CreateMobile(Account a)
        {
            if (a.Count >= a.Limit)
                return null;

            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] == null)
                    return (a[i] = new PlayerMobile());
            }

            return null;
        }
        #endregion

    }
}

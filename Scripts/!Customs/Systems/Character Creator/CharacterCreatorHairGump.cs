using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Items;

using Server.CharacterCreator;

namespace Server.Gumps
{
    public class CharacterCreatorHairGump : Gump
    {
        private readonly int _DisplayPage;

        #region Hair Stlye/Facial Hair Stlye/Color
        #region Human
        private int[] HumanHairHues = new int[]
        {
            1102, 1103, 1104, 1105, 1106, 1107, 1108, 1109, 1110, 1111, 1112, 1113, 1114, 1115, 1116, 1117, 1118,
            1119, 1120, 1121, 1122, 1123, 1124, 1125, 1126, 1127, 1128, 1129, 1130, 1131, 1132, 1133, 1134, 1135,
            1136, 1137, 1138, 1139, 1140, 1141, 1142, 1143, 1144, 1145, 1146, 1147, 1148
        };
        private int[] HumanMaleHairStyles = new int[] { 1875, 1876, 1879, 1877, 1871, 1874, 1873, 1878, 1870, 0 };
        private int[] HumanFemaleHairStyles = new int[] { 1847, 1837, 1845, 1843, 1844, 1840, 1839, 1836, 1841, 0 };
        #endregion

        #region Elf
        private int[] ElfHairHues = new int[]
        {
            53, 54, 55, 56, 57, 58, 89, 143, 144, 145, 146, 147, 258, 346, 347, 348, 349, 350, 351, 297,
            304, 211, 485, 500, 520, 530, 531, 570, 594, 714, 798, 799, 800, 801, 802, 803, 804, 805, 806, 807,
            874, 903, 904, 905, 906, 907, 910, 1438, 1721, 1830, 2019
        };
        private int[] ElfMaleHairStyles = new int[]
        {
            1784, 1785, 1786, 1787, 0, 1789, 1790, 1791, 1793
        };
        private int[] ElfFemaleHairStyles = new int[]
        {
            1775, 1776, 1777, 1778, 0, 1780, 1781, 1782, 1783
        };
        #endregion
        #region Gargoyle
        private int[] GargoyleHairHues = new int[]
        {
            1802, 1803, 1804, 1805, 1806, 1807, 1808, 1809, 1810, 1811, 1894, 1895, 1896, 1897, 1898, 1899, 1900, 1901, 1902, 1903,
            1760, 1761, 1762, 1763, 1764, 1765, 1766, 1767, 1768, 1769, 1770, 1771, 1772, 1773, 1774, 1775, 1776, 1777, 1778, 1779
        };
        private int[] GargoyleMaleHairStyles = new int[]
        {
            1900, 1901, 1907, 1902, 0, 1908, 1909, 1910, 1911
        };
        private int[] GargoyleFemaleHairStyles = new int[]
        {
            1952, 1953, 1950, 1954, 0, 1951, 1916, 1917, 1918
        };
        #endregion
        #region Orc
        private int[] OrcHairHues = new int[]
        {
            1202, 1203, 1204, 1205, 1206, 1302, 1303, 1304, 1305, 1306, 1402, 1403, 1404, 1405, 1406, 1502, 1503, 1504, 1505, 1506,
            1602, 1603, 1604, 1605, 1606, 1702, 1703, 1704, 1705, 1706
        };
        private int[] OrcMaleHairStyles = new int[]
        {
            1784, 1785, 1786, 1787, 0, 1789, 1790, 1791, 1793
        };
        private int[] OrcFemaleHairStyles = new int[]
        {
            1775, 1776, 1777, 1778, 0, 1780, 1781, 1782, 1783
        };
        #endregion
        #endregion

        string _PlayerName;
        int _SkinHue;
        int _RaceID;
        int _ShirtHue;
        int _ShoeHue;
        int _PantsHue;
        int[] Hues = new int[] { };
        bool _IsFemale;
        int SelectedCharacterCreatorColor;
        int[] HairStyles = new int[] { };
        int switchid = 10000;
        int buttonid = 20000;
        int _SelectedColor = 0;
        int _HairStyleChoosen = 0;
        public CharacterCreatorHairGump(int xposs , int yposs,Mobile from,int selectedcharactercreatorcolor,string playername, bool isfemale,int skinHue, int raceID, int shirthue,int pantshue,int shoehue, int selectedcolor, int hairstylechoosen, int displayPage = 1)
            : base(50, 50)
        {
            _DisplayPage = displayPage;
            _HairStyleChoosen = hairstylechoosen;
            _SelectedColor = selectedcolor;
            SelectedCharacterCreatorColor = selectedcharactercreatorcolor;
            _IsFemale = isfemale;
            _ShirtHue = shirthue;
            _PantsHue = pantshue;
            _ShoeHue = shoehue;
            _PlayerName = playername;
            _SkinHue = skinHue;
            _RaceID = raceID;


            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;

            AddPage(0);
            AddBackground(48, 5, 700, 500, 9270);

            if (raceID == 0)
                AddImage(280, 68, 1800);//Human Backdrop

            else if (raceID == 1)
                AddImage(280, 68, 1800);//Elf Backdrop

            else if (raceID == 2)
                AddImage(280, 68, 1800);//Gargoyle Backdrop

            else if (raceID == 3)
                AddImage(280, 68, 1800);//Orc Backdrop

            AddImage(716, 16, 10441);
            AddLabel(330, 26, 1153, @"Character Creator");
            AddButton(308, 460, 12015, 12017, (int)Buttons.CancelButton, GumpButtonType.Reply, 0);
            AddButton(405, 460, 12009, 12011, (int)Buttons.OkayButton, GumpButtonType.Reply, 0);
            AddBackground(521, 68, 199, 357, 9200);
            AddLabel(546, 82, 1153, @"Choose thy hair color");
            AddImage(2, 16, 10440);
            AddBackground(73, 68, 199, 19, 9200);
            AddLabel(97, 67, 1153, @"Choose thy hairstyle");
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

            // AddBackground(185, 382, 63, 65, 2620);

            switch (_RaceID)
            {
                case 0://Human
                    {
                        if (!_IsFemale)
                        {
                            //clothing
                            AddImage(282, 63, 1889, _SkinHue - 1); //character body
                            AddImage(282, 63, 1849, _ShirtHue); //shirt
                            AddImage(282, 63, 1848, _PantsHue);//pants
                            AddImage(282, 63, 1890, _ShoeHue);//shoes
                            HairStyles = HumanMaleHairStyles;

                        }
                        else//Female
                        {
                            //clothing
                            AddImage(282, 63, 1888, _SkinHue - 1); //character body
                            AddImage(282, 63, 1812, _ShirtHue); //shirt
                            AddImage(282, 63, 1891, _ShoeHue);//shoes
                            AddImage(282, 63, 1892, _PantsHue);//pants
                            HairStyles = HumanFemaleHairStyles;
                        }

                        Hues = HumanHairHues;
                        

                        break;
                    }
                case 1://Elf
                    {
                        if (!_IsFemale)
                        {
                            //clothing
                            AddImage(282, 63, 1894, _SkinHue - 1); //character body
                            AddImage(282, 63, 1849, _ShirtHue); //shirt
                            AddImage(282, 63, 1848, _PantsHue);//pants
                            AddImage(282, 63, 1890, _ShoeHue);//shoes
                            HairStyles = ElfMaleHairStyles;
                        }
                        else//female
                        {     //clothing
                            AddImage(282, 63, 1893, _SkinHue - 1); //character body
                            AddImage(282, 63, 1812, _ShirtHue); //shirt
                            AddImage(282, 63, 1891, _ShoeHue);//shoes 
                            AddImage(282, 63, 1892, _PantsHue);//pants
                            HairStyles = ElfFemaleHairStyles;
                        }
                        Hues = ElfHairHues;
                        
                        break;
                    }
                case 2://Gargoyle
                    {
                        if (!_IsFemale)
                        {
                            //clothing
                            AddImage(282, 63, 1899, _SkinHue - 1); //character body
                            AddImage(282, 63, 1912, _ShirtHue); //robe
                            HairStyles = GargoyleMaleHairStyles;
                        }
                        else//female
                        {
                            //clothing
                            AddImage(282, 63, 1898, _SkinHue - 1); //character body
                            AddImage(282, 63, 1912, _ShirtHue); //robe
                            HairStyles = GargoyleFemaleHairStyles;
                        }
                        Hues = GargoyleHairHues;
                        
                        break;
                    }
                case 3://Orc
                    {
                        if (!_IsFemale)
                        {
                            //clothing
                            AddImage(282, 63, 1889, _SkinHue - 1); //character body
                            AddImage(304, 74, 60416, skinHue - 1); //face
                            AddImage(282, 63, 1849, _ShirtHue); //shirt
                            AddImage(282, 63, 1848, _PantsHue);//pants
                            AddImage(282, 63, 1890, _ShoeHue);//shoes
                            HairStyles = OrcMaleHairStyles;
                        }
                        else//female
                        {
                            //clothing
                            AddImage(282, 63, 1888, _SkinHue - 1); //character body
                            AddImage(304, 74, 60416, skinHue - 1); //face
                            AddImage(282, 63, 1812, _ShirtHue); //shirt
                            AddImage(282, 63, 1891, _ShoeHue);//shoes
                            AddImage(282, 63, 1892, _PantsHue);//pants
                            HairStyles = OrcFemaleHairStyles;
                        }
                        Hues = OrcHairHues;
                        
                        break;
                    }
            }

            if (_RaceID == 2)
            {
                if (HairStyles[_HairStyleChoosen] != 0)
                {
                    AddImage(282, 63, HairStyles[_HairStyleChoosen], Hues[_SelectedColor] - 1);//HAIR/HORN
                }
            }
            else
            {
                if (HairStyles[_HairStyleChoosen] != 0)
                AddImage(282, 63, HairStyles[_HairStyleChoosen], Hues[_SelectedColor] - 1);//HAIR/HORN
            }


            int leftx = 104;
            int rightx = 185;
            int origionaly = 99;
            int ypos = 99;
            int xpos = 104;
            int verticalspace = 70;
            int horizontalspace = 81;
            int buttonxleft = 79;
            int buttonxright = 253;
            int buttonx = buttonxleft;
            int colorcount = 0;
            
            for (int x = 0; x < HairStyles.Length && x <= 9; x++)
            {
                AddBackground(xpos, ypos, 63, 65, 2620);

                if (_RaceID == 2)
                {
                    if(HairStyles[x] != 0)
                        AddImage(xpos - 80, ypos - 40, HairStyles[x]);
                }
                else
                {
                    if (HairStyles[x] != 0)
                        AddImage(xpos - 80, ypos - 50, HairStyles[x]);
                }

                if (hairstylechoosen == x)
                {
                    AddButton(buttonx, ypos, 1895, 1895, buttonid + x, GumpButtonType.Reply, 0);
                }
                else
                {
                    AddButton(buttonx, ypos, 1896, 1896, buttonid + x, GumpButtonType.Reply, 0);
                }

                
                if(x == 4)
                {
                    xpos = rightx;
                    ypos = origionaly;
                    buttonx = buttonxright;
                }
                else
                {
                    ypos += verticalspace;
                }
            }

             leftx = 536;
             rightx = 650;
             origionaly = 142;
             ypos = 142;
             xpos = 536;
             verticalspace = 22;
             horizontalspace = 32;
            int pagenum = 2;
            int count = 0;

            Point2D backbuttonlocation = new Point2D(533, 394);
            Point2D nextpagebuttonlocation = new Point2D(692, 394);
            Point2D pagetextlocation = new Point2D(594, 394);   

            AddPage(CharacterCreatorSystem.MapPage(1, _DisplayPage));
            for (int x = 0; x < Hues.Length; x++)
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

                if (_SelectedColor == x)
                {
                    AddButton(xpos, ypos, 209, 209, switchid + x, GumpButtonType.Reply, 0);
                }
                else
                {
                    AddButton(xpos, ypos, 208, 209, switchid + x, GumpButtonType.Reply, 0);
                }
                AddImage(xpos + horizontalspace, ypos, 210, Hues[x] - 1);

    
                ypos += verticalspace;

                if(count == 20)
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
            OkayButton
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            if (info.ButtonID >= 20000)//Button
            {
                int colornum = info.ButtonID - 20000;
                from.SendGump(new CharacterCreatorHairGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, _SelectedColor, colornum, CharacterCreatorSystem.GetSelectionPage(_SelectedColor)));

            }
            else if(info.ButtonID >= 10000)//Switch
            {
                int colornum = info.ButtonID - 10000;
                from.SendGump(new CharacterCreatorHairGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, colornum, _HairStyleChoosen, CharacterCreatorSystem.GetSelectionPage(colornum)));
            }
            else
            {
                switch (info.ButtonID)
                {
                    case (int)Buttons.CancelButton:
                        {
                            from.SendGump(new CharacterCreatorGump(this.X, this.Y, from, _IsFemale, _RaceID, _PlayerName, SelectedCharacterCreatorColor, _SkinHue, "", _ShirtHue, _PantsHue, _ShoeHue));
                            break;
                        }
                    case (int)Buttons.OkayButton:
                        {
                            if(!_IsFemale && _RaceID != 1)
                                from.SendGump(new CharacterCreatorFacialHairGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, _SelectedColor,_HairStyleChoosen,Hues[_SelectedColor],HairStyles[_HairStyleChoosen],0,0));
                            else
                                from.SendGump(new CharacterCreatorSkillsGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, _SelectedColor, _HairStyleChoosen, Hues[_SelectedColor], HairStyles[_HairStyleChoosen], 0, 0,0,0));
                            
                            break;
                        }
                }
            }
        }
    }
}

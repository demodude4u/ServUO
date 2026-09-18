
using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Items;

using Server.CharacterCreator;

namespace Server.Gumps
{
    public class CharacterCreatorFacialHairGump : Gump
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
        private int[] HumanFacialHairStyles = new int[] { 0, 1884, 1881, 1887, 1885, 1886, 1883, 1882 };
        #endregion

        #region Elf
        private int[] ElfHairHues = new int[]
        {
            1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018,
             1019, 1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 1030, 1031, 1032, 1033, 1034, 1035,
             1036, 1037, 1038, 1039, 1040, 1041, 1042, 1043, 1044, 1045, 1046, 1047, 1048
        };
        private int[] ElfFacialHairStyles = new int[]
        {
            0
        };
        #endregion
        #region Gargoyle
        private int[] GargoyleHairHues = new int[]
        {
            1802, 1803, 1804, 1805, 1806, 1807, 1808, 1809, 1810, 1811, 1894, 1895, 1896, 1897, 1898, 1899, 1900, 1901, 1902, 1903,
            1760, 1761, 1762, 1763, 1764, 1765, 1766, 1767, 1768, 1769, 1770, 1771, 1772, 1773, 1774, 1775, 1776, 1777, 1778, 1779
        };
        private int[] GargoyleFacialHairStyles = new int[]
        {
            0, 1903, 1904, 1905, 1906
        };
        #endregion
        #region Orc
        private int[] OrcHairHues = new int[]
        {
            1202, 1203, 1204, 1205, 1206, 1302, 1303, 1304, 1305, 1306, 1402, 1403, 1404, 1405, 1406, 1502, 1503, 1504, 1505, 1506,
            1602, 1603, 1604, 1605, 1606, 1702, 1703, 1704, 1705, 1706
        };
        private int[] OrcFacialHairStyles = new int[]
        {
            0, 1881, 1882, 1883, 1884, 1885, 1886, 1887
        };
        #endregion
        #endregion


        int switchid = 10000;
        int buttonid = 20000;

        string _PlayerName;
        int _SkinHue;
        int _RaceID;
        int _ShirtHue;
        int _ShoeHue;
        int _PantsHue;
        bool _IsFemale;
        int SelectedCharacterCreatorColor;
        int HairGumpHairChoosen;
        int HairGumpHairColorChoosen;
        int _HairStyle;
        int _HairHue;

        int[] Hues = new int[] { };


        int[] FacialHairStyles = new int[] { };
        int _SelectedColor = 0;
        int _FacialHairStyleChoosen = 0;

        public CharacterCreatorFacialHairGump(int xposs, int yposs, Mobile from, int selectedcharactercreatorcolor, string playername, bool isfemale, int skinHue, int raceID, int shirthue, int pantshue, int shoehue, int hairgumpselectedcolor, int hairgumphairstylechoosen, int hairhue, int hairid, int selectedcolor, int facialhairstyleselected, int displayPage = 1)
            : base(50, 50)
        {
            _DisplayPage = displayPage;
            HairGumpHairChoosen = hairgumphairstylechoosen;
            HairGumpHairColorChoosen = hairgumpselectedcolor;
            _FacialHairStyleChoosen = facialhairstyleselected;
            _SelectedColor = selectedcolor;
            SelectedCharacterCreatorColor = selectedcharactercreatorcolor;
            _IsFemale = isfemale;
            _ShirtHue = shirthue;
            _PantsHue = pantshue;
            _ShoeHue = shoehue;
            _PlayerName = playername;
            _SkinHue = skinHue;
            _RaceID = raceID;
            _HairStyle = hairid;
            _HairHue = hairhue;


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
            AddLabel(543, 82, 1153, @"Choose thy facial hair color");
            AddImage(2, 16, 10440);
            AddBackground(73, 68, 199, 19, 9200);
            AddLabel(95, 67, 1153, @"Choose thy facial hairstyle");
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

            switch (_RaceID)
            {
                case 0://Human
                    {
                        //clothing
                        AddImage(282, 63, 1889, _SkinHue - 1); //character body
                        AddImage(282, 63, 1849, _ShirtHue); //shirt
                        AddImage(282, 63, 1848, _PantsHue);//pants
                        AddImage(282, 63, 1890, _ShoeHue);//shoes
                        FacialHairStyles = HumanFacialHairStyles;

                        Hues = HumanHairHues;


                        break;
                    }
                case 1://Elf
                    {
                        //clothing
                        AddImage(282, 63, 1894, _SkinHue - 1); //character body
                        AddImage(282, 63, 1849, _ShirtHue); //shirt
                        AddImage(282, 63, 1848, _PantsHue);//pants
                        AddImage(282, 63, 1890, _ShoeHue);//shoes
                        FacialHairStyles = ElfFacialHairStyles;
                        Hues = ElfHairHues;

                        break;
                    }
                case 2://Gargoyle
                    {

                        //clothing
                        AddImage(282, 63, 1899, _SkinHue - 1); //character body
                        AddImage(282, 63, 1912, _ShirtHue); //robe
                        FacialHairStyles = GargoyleFacialHairStyles;

                        Hues = GargoyleHairHues;

                        break;
                    }
                case 3://Orc
                    {

                        //clothing
                        AddImage(282, 63, 1889, _SkinHue - 1); //character body
                        AddImage(304, 74, 60416, skinHue - 1); //face
                        AddImage(282, 63, 1849, _ShirtHue); //shirt
                        AddImage(282, 63, 1848, _PantsHue);//pants
                        AddImage(282, 63, 1890, _ShoeHue);//shoes
                        FacialHairStyles = OrcFacialHairStyles;

                        Hues = OrcHairHues;

                        break;
                    }
            }

            if (_RaceID == 2)
            {
                if(_HairStyle != 0)
                AddImage(282, 63, _HairStyle, _HairHue - 1);//HAIR/HORN

                if (FacialHairStyles[_FacialHairStyleChoosen] != 0)
                {
                    AddImage(282, 63, FacialHairStyles[_FacialHairStyleChoosen], Hues[_SelectedColor] - 1);//Facial HAIR/HORN
                }
            }
            else
            {
                if (_HairStyle != 0)
                    AddImage(282, 63, _HairStyle, _HairHue - 1);//HAIR/HORN

                if (FacialHairStyles[_FacialHairStyleChoosen] != 0)
                    AddImage(282, 63, FacialHairStyles[_FacialHairStyleChoosen], Hues[_SelectedColor] - 1);//Facial HAIR/HORN
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

            for (int x = 0; x < FacialHairStyles.Length && x <= 9; x++)
            {
                AddBackground(xpos, ypos, 63, 65, 2620);

                if (_RaceID == 2)
                {
                    if (FacialHairStyles[x] != 0)
                        AddImage(xpos - 80, ypos - 50, FacialHairStyles[x]);
                }
                else
                {
                    if (FacialHairStyles[x] != 0)
                        AddImage(xpos - 80, ypos - 50, FacialHairStyles[x]);
                }

                if (_FacialHairStyleChoosen == x)
                {
                    AddButton(buttonx, ypos, 1895, 1895, buttonid + x, GumpButtonType.Reply, 0);
                }
                else
                {
                    AddButton(buttonx, ypos, 1896, 1896, buttonid + x, GumpButtonType.Reply, 0);
                }


                if (x == 4)
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
                if (_SelectedColor == x)
                {
                    AddButton(xpos, ypos, 209, 209, switchid + x, GumpButtonType.Reply, 0);
                }
                else
                {
                    AddButton(xpos, ypos, 208, 209, switchid + x, GumpButtonType.Reply, 0);
                }
                AddImage(xpos + horizontalspace, ypos, 210, Hues[x] - 1);

                count++;
                ypos += verticalspace;

                if (count == 20)
                {
                    count = 0;
                    xpos = leftx;
                    ypos = origionaly;

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
                from.SendGump(new CharacterCreatorFacialHairGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, colornum, CharacterCreatorSystem.GetSelectionPage(_SelectedColor)));
                //from.SendGump(new CharacterCreatorHairGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, _SelectedColor, colornum));

            }
            else if (info.ButtonID >= 10000)//Switch
            {
                int colornum = info.ButtonID - 10000;
                from.SendGump(new CharacterCreatorFacialHairGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, colornum, _FacialHairStyleChoosen, CharacterCreatorSystem.GetSelectionPage(colornum)));
            }
            else
            {
                switch (info.ButtonID)
                {
                    case (int)Buttons.CancelButton:
                        {
                            // from.SendGump(new CharacterCreatorGump(from, _IsFemale, _RaceID, _PlayerName, SelectedCharacterCreatorColor, _SkinHue, "", _ShirtHue, _PantsHue, _ShoeHue));
                            from.SendGump(new CharacterCreatorHairGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen));
                            break;
                        }
                    case (int)Buttons.OkayButton:
                        {
                            from.SendGump(new CharacterCreatorSkillsGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen,Hues[_SelectedColor],FacialHairStyles[_FacialHairStyleChoosen]));
                            break;
                        }
                }
            }
        }
    }
}

using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using System.Collections.Generic;
using Server.Items;
using Server.CharacterCreator;

namespace Server.Gumps
{
    public class CharacterCreatorMapGump : Gump
    {

        int SkillValue = 0;

        int switchid = 20000;
        int buttonid = 10000;

        Gold gold = new Gold(1000); // Starting gold can be customized here

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

        int SelectedGumpID = 1210;
        int UnSelectedGumpID = 1209;
        int WhiteTextHue = 1153;
        int RedTextHue = 37;

        #region MAP INFO

        string[] Facets = new string[]
        {
            "Felucca (PVP Allowed)","Trammel","Ilshenar","Malas","Tokuno Islands","Ter Mur"
        };

        string[] FelNames = new string[]
        {
            "Yew","Britain","Skara Brae","Trinsic","Jhelom","Minoc","Vesper","Moonglow","Ocllo"
        };
        Point2D[] FelButtonLocations = new Point2D[]
        {
            new Point2D(381,132), // Yew Button
            new Point2D(450,202), // Britain Button
            new Point2D(374,254), // Skara Brae Button
            new Point2D(474,310), // Trinsic Button
            new Point2D(435,402), // Jhelom Button
            new Point2D(514,94), // Minoc Button
            new Point2D(544,128), // Vesper Button
            new Point2D(666,160), // Moonglow Button
            new Point2D(605,290) // Ocllo Button
        };
        Point2D[] FelLabelLocations = new Point2D[]
        {
            new Point2D(398,129), // Yew Label
            new Point2D(467,199), // Britain Label
            new Point2D(391,251), // Skara Brae Label
            new Point2D(491,307), // Trinsic Label
            new Point2D(452,399), // Jhelom Label
            new Point2D(530,91), // Minoc Label
            new Point2D(561,125), // Vesper Label
            new Point2D(607,157), // Moonglow Label
            new Point2D(622,287) // Ocllo Label
        };
        Point3D[] FelCityLocations = new Point3D[]
        {
            new Point3D(531,990,0), // Yew City Location
            new Point3D(1495,1629,10), // Britain City Location
            new Point3D(635,2234,0), // Skara Brae City Location
            new Point3D(1843,2747,0), // Trinsic City Location
            new Point3D(1378,3817,0), // Jhelom City Location
            new Point3D(2518,562,0), // Minoc City Location
            new Point3D(2899,676,0),  // Vesper City Location
            new Point3D(4442,1172,0), // Moonglow City Location
            new Point3D(3668,2628,0) // Ocllo City Location
        };
        string[] TramNames = new string[]
        {
            "Yew","Britain","Skara Brae","Trinsic","Jhelom","Minoc","Vesper","Moonglow","New Haven"
        };
        Point2D[] TramButtonLocations = new Point2D[]
        {
            new Point2D(381,132), // Yew Button
            new Point2D(450,202), // Britain Button
            new Point2D(374,254), // Skara Brae Button
            new Point2D(474,310), // Trinsic Button
            new Point2D(435,402), // Jhelom Button
            new Point2D(514,94), // Minoc Button
            new Point2D(544,128), // Vesper Button
            new Point2D(666,160), // Moonglow Button
            new Point2D(605,290) // New Haven Button
        };
        Point2D[] TramLabelLocations = new Point2D[]
        {
            new Point2D(398,129), // Yew Label
            new Point2D(467,199), // Britain Label
            new Point2D(391,251), // Skara Brae Label
            new Point2D(491,307), // Trinsic Label
            new Point2D(452,399), // Jhelom Label
            new Point2D(530,91), // Minoc Label
            new Point2D(561,125), // Vesper Label
            new Point2D(607,157), // Moonglow Label
            new Point2D(622,287) // New Haven Label
        };
        Point3D[] TramCityLocations = new Point3D[]
        {
            new Point3D(531,990,0), // Yew City Location
            new Point3D(1495,1629,10), // Britain City Location
            new Point3D(635,2234,0), // Skara Brae City Location
            new Point3D(1843,2747,0), // Trinsic City Location
            new Point3D(1378,3817,0), // Jhelom City Location
            new Point3D(2518,562,0), // Minoc City Location
            new Point3D(2899,676,0),  // Vesper City Location
            new Point3D(4442,1172,0), // Moonglow City Location
            new Point3D(3499,2571,14) // New Haven City Location
        };
        string[] IlshNames = new string[]
        {
            "Gypsy Camp","Mistas","Lakeshire","Ver Lor Reg"
        };
        Point2D[] IlshButtonLocations = new Point2D[]
        {                
            new Point2D(562,173), // Gypsy Camp Button
            new Point2D(492,298), // Mistas Button
            new Point2D(558,323), // Lakeshire Button
            new Point2D(493,200) // Ver Lor Reg Button
        };
        Point2D[] IlshLabelLocations = new Point2D[]
        {
            new Point2D(579,170), // Gypsy Camp Label
            new Point2D(509,295), // Mistas Label
            new Point2D(575,320), // Lakeshire Camp Label
            new Point2D(510,197) // Ver Lor Reg Label
        };
        Point3D[] IlshCityLocations = new Point3D[]
        {
            new Point3D(1611,546,-16), // Gypsy Camp City Location
            new Point3D(818,1073,-30), // Mistas City Location
            new Point3D(1203,1124,-25), // Lakeshire City Location
            new Point3D(836,641,-20) // Ver Lor Reg City Location
        };
        string[] MalasNames = new string[]
        {
            "Luna","Umbra"
        };
        Point2D[] MalasButtonLocations = new Point2D[]
        {
            new Point2D(417,144), // Luna Button
            new Point2D(613,301) // Umbra Button
        };
        Point2D[] MalasLabelLocations = new Point2D[]
        {
            new Point2D(434,141), // Luna Label
            new Point2D(630,298) // Umbra Label
        };
        Point3D[] MalasCityLocations = new Point3D[]
        {
            new Point3D(989,520,-50), // Luna City Location
            new Point3D(2037,1360,-85) // Umbra City Location
        };
        string[] TokunoNames = new string[]
        {
            "Zento"
        };
        Point2D[] TokunoButtonLocations = new Point2D[]
        {
            new Point2D(525,380) // Zento Button
        };
        Point2D[] TokunoLabelLocations = new Point2D[]
        {
            new Point2D(542,377) // Zento Label
        };
        Point3D[] TokunoCityLocations = new Point3D[]
        {
            new Point3D(736,1256,30) // Zento City Location
        };
        string[] TerMurNames = new string[]
        {
            "Royal City","Holy City"
        };
        Point2D[] TerMurButtonLocations = new Point2D[]
        {
            new Point2D(513, 242), // Royal City Button
            new Point2D(606, 365) // Holy City Button
        };
        Point2D[] TerMurLabelLocations = new Point2D[]
        {
            new Point2D(530, 239), // Royal City Label
            new Point2D(623, 362) // Holy City Label
        };
        Point3D[] TerMurCityLocations = new Point3D[]
        {
            new Point3D(750,3441,-20), // Royal City Location
            new Point3D(996,3855,-42) // Holy City Location
        };
        #endregion

        #region Hair Stlye/Facial Hair Stlye/Color
        #region Human
        private int[] HumanHairHues = new int[]
        {
            1102, 1103, 1104, 1105, 1106, 1107, 1108, 1109, 1110, 1111, 1112, 1113, 1114, 1115, 1116, 1117, 1118,
            1119, 1120, 1121, 1122, 1123, 1124, 1125, 1126, 1127, 1128, 1129, 1130, 1131, 1132, 1133, 1134, 1135,
            1136, 1137, 1138, 1139, 1140, 1141, 1142, 1143, 1144, 1145, 1146, 1147, 1148
        };
        private int[] HumanFacialHairStyles = new int[]
        {
            1881, 1882, 1883, 1884, 0, 1885, 1886, 1887
        };
        int[] HumanMaleHairItemIDs = new int[]
        {
            8251, 8252, 8253, 8260, 8261, 8266, 8263, 8264, 8265, 0
        };
        int[] HumanMaleFacilHairItemIDs = new int[]
        {
            0, 8257, 8256, 8269, 8255, 8267, 8254, 8268
        };
        int[] HumanFemaleHairItemIDs = new int[]
        {
            8251, 8252, 8253, 8260, 8261, 8266, 8263, 8265, 8262, 0
        };
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
            50801,50802,50804
        };
        int[] ElfMaleHairItemIDs = new int[]
        {
            12223, 12224, 12225, 12226, 0, 12237, 12238, 12239, 12241
        };
        int[] ElfFemaleHairItemIDs = new int[]
        {
            12224, 12225, 12226, 12236, 0, 12238, 12239, 12240, 12241
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
            1903, 1904, 1905, 1906, 0
        };
        int[] GargoyleMaleHairItemIDs = new int[]
        {
            16984, 16985, 16986, 16987, 16992, 16988, 16989, 16990, 16991
        };
        int[] GargoyleMaleFacilHairItemIDs = new int[]
        {
            17068, 17069, 17070, 17071, 17072
        };
        int[] GargoyleFemaleHairItemIDs = new int[]
        {
            16993, 16994, 17011, 17012, 17014, 17013, 17066, 17067, 17073
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
            1881, 1882, 1883, 1884, 0, 1885, 1886, 1887
        };
        int[] OrcMaleHairItemIDs = new int[]
        {
            12223, 12224, 12225, 12226, 0, 12237, 12238, 12239, 12241
        };
        int[] OrcMaleFacilHairItemIDs = new int[]
        {
            0, 8257, 8256, 8269, 8255, 8267, 8254, 8268
        };
        int[] OrcFemaleHairItemIDs = new int[]
        {
            12224, 12225, 12226, 12236, 0, 12238, 12239, 12240, 12241
        };
        #endregion
        #endregion

        int[] FacialHairStyles = new int[] { };
        int _SelectedColor = 0;
        int _FacialHairStyleChoosen = 0;
        int _FacialHairHue;
        int _FacialHarID;

        int _Map = 1;
        int _StartLocation;
        string _CityName;

        int _Strength;
        int _Dexterity;
        int _Intellegence;
        List<int> EquipmentSelectedList;
        List<Skill> SkillsSelectedList;
        bool _IsAdvancedChar;
        int _StatNum = 0;
        public CharacterCreatorMapGump(int map, int city, int statnum, bool isAdvanced, bool humble, List<int> equipselected, List<Skill> skillsselectedlist, int strength, int intellegence, int dexterity, int xposs, int yposs, Mobile from, int selectedcharactercreatorcolor, string playername, bool isfemale, int skinHue, int raceID, int shirthue, int pantshue, int shoehue, int hairgumpselectedcolor, int hairgumphairstylechoosen, int hairhue, int hairid, int selectedcolor, int facialhairstyleselected, int facialhairhue, int facialhairid)
            : base(50, 50)
        {



            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;

            EquipmentSelectedList = equipselected;
            _Map = map;
            _StartLocation = city;

            _StatNum = statnum;
            _IsAdvancedChar = isAdvanced;
            SkillsSelectedList = skillsselectedlist;
            _Strength = strength;
            _Dexterity = dexterity;
            _Intellegence = intellegence;

            _FacialHarID = facialhairid;
            _FacialHairHue = facialhairhue;
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

            AddPage(0);
            AddBackground(48, 5, 700, 500, 9270);
            AddImage(716, 16, 10441);
            AddLabel(330, 26, 1153, @"Character Creator");
            AddImage(2, 16, 10440);
            AddBackground(84, 50, 222, 181, 9200);
            AddHtml(96, 64, 200, 159, @"Thou art about to take thy final step in your journey to a new life in Britannia. But one choice remains: Where shalt thy new life begin?", (bool)true, (bool)true);
            AddBackground(84, 262, 222, 181, 9200);

            AddButton(405, 460, 12009, 12011, (int)Buttons.OkayButton, GumpButtonType.Reply, 0);



            Point2D backbuttonlocation = new Point2D(533, 394);
            Point2D nextpagebuttonlocation = new Point2D(692, 394);
            Point2D pagetextlocation = new Point2D(594, 394);   

            int buttonx = 98;
            int buttony = 290;
            int labelx = 129;
            int labely = 288;

            int verticalspace = 22;
            AddPage(1);


            //Draw map selection buttons/text
            for (int x = 0; x < Facets.Length; x++)
            {
                //Current Map Selected
                if (x == _Map)
                {
                    AddButton(buttonx, buttony, SelectedGumpID, UnSelectedGumpID, buttonid + x, GumpButtonType.Reply, 0);
                }
                else//Current map not selected
                {
                    AddButton(buttonx, buttony, UnSelectedGumpID, SelectedGumpID, buttonid + x, GumpButtonType.Reply, 0);
                }

                //Add Label
                if (x == 0)
                    AddLabel(labelx, labely, RedTextHue, Facets[x]);
                else
                    AddLabel(labelx, labely, WhiteTextHue, Facets[x]);

                buttony += verticalspace;
                labely += verticalspace;
            }

            switch (_Map)
            {
                case 0://Fel
                    {
                        AddImage(335, 55, 5593);
                        AddImage(330, 50, 5599);

                        //Draw city selection buttons/text
                        for (int x = 0; x < FelNames.Length; x++)
                        {
                            //Current city Selected
                            if (x == city)
                            {
                                AddButton(FelButtonLocations[x].X, FelButtonLocations[x].Y, SelectedGumpID, UnSelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                                _CityName = FelNames[x];
                            }
                            else//Current city not selected
                            {
                                AddButton(FelButtonLocations[x].X, FelButtonLocations[x].Y, UnSelectedGumpID, SelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                            }
                            AddLabel(FelLabelLocations[x].X, FelLabelLocations[x].Y, WhiteTextHue, FelNames[x]);
                        }
                        break;
                    }
                case 1://Tram
                    {
                        AddImage(335, 55, 5594);
                        AddImage(330, 50, 5599);

                        //Draw city selection buttons/text
                        for (int x = 0; x < TramNames.Length; x++)
                        {
                            //Current city Selected
                            if (x == city)
                            {
                                AddButton(TramButtonLocations[x].X, TramButtonLocations[x].Y, SelectedGumpID, UnSelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                                _CityName = TramNames[x];
                            }
                            else//Current city not selected
                            {
                                AddButton(TramButtonLocations[x].X, TramButtonLocations[x].Y, UnSelectedGumpID, SelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                            }
                            AddLabel(TramLabelLocations[x].X, TramLabelLocations[x].Y, WhiteTextHue, TramNames[x]);
                        }
                        break;
                    }
                case 2://Ilsh
                    {
                        AddBackground(338, 58, 375, 375, 3000);
                        AddImage(335, 55, 5595);
                        AddImage(330, 50, 5599);

                        //Draw city selection buttons/text
                        for (int x = 0; x < IlshNames.Length; x++)
                        {
                            //Current city Selected
                            if (x == city)
                            {
                                AddButton(IlshButtonLocations[x].X, IlshButtonLocations[x].Y, SelectedGumpID, UnSelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                                _CityName = IlshNames[x];
                            }
                            else//Current city not selected
                            {
                                AddButton(IlshButtonLocations[x].X, IlshButtonLocations[x].Y, UnSelectedGumpID, SelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                            }
                            AddLabel(IlshLabelLocations[x].X, IlshLabelLocations[x].Y, WhiteTextHue, IlshNames[x]);
                        }
                        break;
                    }
                case 3://Malas
                    {
                        AddImage(335, 55, 5596);
                        AddImage(330, 50, 5599);

                        //Draw city selection buttons/text
                        for (int x = 0; x < MalasNames.Length; x++)
                        {
                            //Current city Selected
                            if (x == city)
                            {
                                AddButton(MalasButtonLocations[x].X, MalasButtonLocations[x].Y, SelectedGumpID, UnSelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                                _CityName = MalasNames[x];
                            }
                            else//Current city not selected
                            {
                                AddButton(MalasButtonLocations[x].X, MalasButtonLocations[x].Y, UnSelectedGumpID, SelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                            }
                            AddLabel(MalasLabelLocations[x].X, MalasLabelLocations[x].Y, WhiteTextHue, MalasNames[x]);
                        }
                        break;
                    }
                case 4://Tokuno
                    {
                        AddImage(335, 55, 5597);
                        AddImage(330, 50, 5599);

                        //Draw city selection buttons/text
                        for (int x = 0; x < TokunoNames.Length; x++)
                        {
                            //Current city Selected
                            if (x == city)
                            {
                                AddButton(TokunoButtonLocations[x].X, TokunoButtonLocations[x].Y, SelectedGumpID, UnSelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                                _CityName = TokunoNames[x];
                            }
                            else//Current city not selected
                            {
                                AddButton(TokunoButtonLocations[x].X, TokunoButtonLocations[x].Y, UnSelectedGumpID, SelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                            }
                            AddLabel(TokunoLabelLocations[x].X, TokunoLabelLocations[x].Y, WhiteTextHue, TokunoNames[x]);
                        }
                        break;
                    }
                case 5://Termur
                    {
                        AddImage(335, 55, 5598);
                        AddImage(330, 50, 5599);

                        //Draw city selection buttons/text
                        for (int x = 0; x < TerMurNames.Length; x++)
                        {
                            //Current city Selected
                            if (x == city)
                            {
                                AddButton(TerMurButtonLocations[x].X, TerMurButtonLocations[x].Y, SelectedGumpID, UnSelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                                _CityName = TerMurNames[x];
                            }
                            else//Current city not selected
                            {
                                AddButton(TerMurButtonLocations[x].X, TerMurButtonLocations[x].Y, UnSelectedGumpID, SelectedGumpID, switchid + x, GumpButtonType.Reply, 0);
                            }
                            AddLabel(TerMurLabelLocations[x].X, TerMurLabelLocations[x].Y, WhiteTextHue, TerMurNames[x]);
                        }

                        break;
                    }
            }



            AddLabel(100, 460, WhiteTextHue, String.Format("Map : {0} | City : {1}", Facets[_Map], _CityName));


        }

        public enum Buttons
        {
            OkayButton,
            NextPage,
            PrevPage
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            if (info.ButtonID >= 20000)//CITY switchid
            {
                from.SendGump(new CharacterCreatorMapGump(_Map, info.ButtonID - 20000, _StatNum, _IsAdvancedChar, false, EquipmentSelectedList, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
            }
            else if (info.ButtonID >= 10000)//MAP buttonid
            {
                from.SendGump(new CharacterCreatorMapGump(info.ButtonID - 10000, 0, _StatNum, _IsAdvancedChar, false, EquipmentSelectedList, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
            }
            else
            {
                switch (info.ButtonID)
                {
                    case (int)Buttons.OkayButton:
                        {

                            if (_IsAdvancedChar)
                            {
                                SkillValue = 50;
                            }
                            else
                            {
                                SkillValue = 60;
                            }

                            from.Name = _PlayerName;
                            foreach (Skill skill in from.Skills)
                            {
                                skill.Base = 0;
                            }

                            from.Map = Map.Maps[_Map];
                            from.RawStr = _Strength;
                            from.RawDex = _Dexterity;
                            from.RawInt = _Intellegence;
                            from.Female = _IsFemale;


                            from.Hue = _SkinHue;
                            from.Race = Race.Races[_RaceID];


                            if(from.Race == Race.Orc)
                            {
                                if (from.FindItemOnLayer(Layer.Face) == null)
                                {
                                    OrcFace orcface = new OrcFace();
                                    from.EquipItem(orcface);
                                    orcface.Movable = false;
                                    orcface.Hue = from.Hue;
                                }
                            }
                            

                            if (SkillsSelectedList != null)
                            {
                                if (SkillsSelectedList.Count > 0)
                                {
                                    foreach (Skill skill in SkillsSelectedList)
                                    {

                                        from.Skills[skill.SkillName].Base = SkillValue;
                                    }
                                }
                            }

                            int face = 0;
                            int hair = 0;
                            switch (from.Race.RaceID)
                            {
                                case 0:
                                    {
                                        if (from.Female)
                                            hair = HumanFemaleHairItemIDs[HairGumpHairChoosen];
                                        else
                                        {
                                            hair = HumanMaleHairItemIDs[HairGumpHairChoosen];
                                            face = HumanMaleFacilHairItemIDs[_FacialHairStyleChoosen];
                                        }
                                        break;
                                    }
                                case 1:
                                    {
                                        if (from.Female)
                                            hair = ElfFemaleHairItemIDs[HairGumpHairChoosen];
                                        else
                                        {
                                            hair = ElfMaleHairItemIDs[HairGumpHairChoosen];
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        if (from.Female)
                                            hair = GargoyleFemaleHairItemIDs[HairGumpHairChoosen];
                                        else
                                        {
                                            hair = GargoyleMaleHairItemIDs[HairGumpHairChoosen];
                                            face = GargoyleMaleFacilHairItemIDs[_FacialHairStyleChoosen];
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        if (from.Female)
                                            hair = OrcFemaleHairItemIDs[HairGumpHairChoosen];
                                        else
                                        {
                                            hair = OrcMaleHairItemIDs[HairGumpHairChoosen];
                                            face = OrcMaleFacilHairItemIDs[_FacialHairStyleChoosen];
                                        }
                                        break;
                                    }
                            }

                            from.HairItemID = hair;
                            from.HairHue = _HairHue;

                            from.FacialHairItemID = face;
                            from.FacialHairHue = _FacialHairHue;


                            if (EquipmentSelectedList != null)
                            {
                                if (EquipmentSelectedList.Count > 0)
                                {
                                    foreach (int num in EquipmentSelectedList)
                                    {
                                        Item item = (Item)Activator.CreateInstance(CharacterCreatorSystem.ItemTypes[num]);

                                        if (item is Bandage || item is Board || item is IronIngot || item is BlankScroll || item is Lockpick)
                                            item.Amount = 50;

                    
                                        if(item is NecromancerSpellbook)
                                        {
                                            NecromancerSpellbook sb = item as NecromancerSpellbook;
                                            sb.Content = 131071;
                                        }
                                        else if(item is MysticBook)
                                        {
                                            MysticBook book = item as MysticBook;
                                            book.Content = 65535;
                                        }
                                        else if (item is Spellbook && !(item is BookOfChivalry) && !(item is BookOfNinjitsu) && !(item is BookOfBushido))
                                        {
                                            Spellbook sb = item as Spellbook;
                                            sb.Content = 18446744073709551615;
                                        }


                                        if (item != null)
                                            from.Backpack.DropItem(item);
                                    }
                                }
                            }


                            switch (_Map)
                            {
                                case 0://Felucca
                                    {
                                        from.Location = FelCityLocations[_StartLocation];
                                        break;
                                    }
                                case 1://Trammel
                                    {
                                        from.Location = TramCityLocations[_StartLocation];
                                        break;
                                    }
                                case 2://Ilshenar
                                    {
                                        from.Location = IlshCityLocations[_StartLocation];
                                        break;
                                    }
                                case 3://Malas
                                    {
                                        from.Location = MalasCityLocations[_StartLocation];
                                        break;
                                    }
                                case 4://Tokuno
                                    {
                                        from.Location = TokunoCityLocations[_StartLocation];
                                        break;
                                    }
                                case 5://Termur
                                    {
                                        from.Location = TerMurCityLocations[_StartLocation];
                                        break;
                                    }
                            }


                            Item outer = from.FindItemOnLayer(Layer.OuterTorso);

                            if (outer != null)
                            {
                                outer.Delete();
                            }

                            Item fancy = from.FindItemOnLayer(Layer.Shirt);

                            if (fancy != null)
                            {
                                fancy.Delete();
                            }


                            if(from.Race != Race.Gargoyle)
                            {
                            from.EquipItem(new FancyShirt(_ShirtHue));
                            from.EquipItem(new LongPants(_PantsHue));
                            from.EquipItem(new Shoes(_ShoeHue));
                            }
                            else
                            {
                                from.EquipItem(new GargishRobe(_ShirtHue));
                            }

                            from.CantWalk = false;
                            from.Frozen = false;
                            from.Squelched = false;


                            from.Hits = from.HitsMax;
                            from.Mana = from.ManaMax;
                            from.Stam = from.StamMax;

                            from.Backpack.DropItem(new RedBook("a book", from.Name, 20, true));
                            from.Backpack.DropItem(gold);

                            from.Blessed = false;

                            Item itemdagger;

                            if (from.Race != Race.Gargoyle)
                                itemdagger = new Dagger();
                            else
                                itemdagger = new GargishDagger();

                            itemdagger.LootType = LootType.Newbied;
                            from.Backpack.DropItem(itemdagger);

                            break;
                        }
                }
            }
        }
    }
}

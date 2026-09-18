using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Items;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Server.CharacterCreator;

namespace Server.Gumps
{
    public class CharacterCreatorEquipmentGump : Gump
    {
        int switchid = 20000;
        int buttonid = 10000;

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
        int _SelectedColor = 0;
        int _FacialHairStyleChoosen = 0;
        int _FacialHairHue;
        int _FacialHarID;
        bool _Humble = false;
        int _Strength = 0;
        int _Dexterity = 0;
        int _Intellegence = 0;
        List<int> EquipmentSelectedList;
        List<Skill> SkillsSelectedList;
        bool _IsAdvancedChar;
        private readonly int _DisplayPage;


        public CharacterCreatorEquipmentGump(bool isAdvanced, bool humble, List<int> equipselected, List<Skill> skillsselectedlist, int strength, int intellegence, int dexterity, int xposs, int yposs, Mobile from, int selectedcharactercreatorcolor, string playername, bool isfemale, int skinHue, int raceID, int shirthue, int pantshue, int shoehue, int hairgumpselectedcolor, int hairgumphairstylechoosen, int hairhue, int hairid, int selectedcolor, int facialhairstyleselected, int facialhairhue, int facialhairid, int displayPage = 1)
            : base(50, 50)
        {
            _DisplayPage = displayPage;
            _IsAdvancedChar = isAdvanced;
            SkillsSelectedList = skillsselectedlist;
            _Strength = strength;
            _Dexterity = dexterity;
            _Intellegence = intellegence;

            _Humble = humble;
            if (equipselected == null)
            {
                EquipmentSelectedList = new List<int>();
            }
            else
            {
                EquipmentSelectedList = equipselected;
            }
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

            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;


            AddPage(0);
            AddBackground(48, 5, 700, 500, 9270);
            AddImage(716, 16, 10441);
            AddBackground(81, 62, 639, 389, 9200);
            AddImage(135, 80, 11413);
            AddLabel(330, 26, 1153, @"Character Creator");
            AddImage(2, 16, 10440);
            AddButton(308, 460, 12015, 12017, (int)Buttons.PreviousButton, GumpButtonType.Reply, 0);

            if (EquipmentSelectedList.Count == 3 || _Humble == true)
                AddButton(405, 460, 12009, 12011, (int)Buttons.ContinueButton, GumpButtonType.Reply, 0);

            AddBackground(148, 220, 158, 22, 3000);
            AddLabel(171, 220, 1153, @"Equipment Selection");
            AddBackground(124, 268, 205, 158, 3500);
            AddHtml(150, 284, 152, 131, @"As a newcomer to Britannia, you will receive 3 pieces of equipment to begin your journey. In addition, you'll also receive a dagger, journal and 1000 gold.", (bool)false, (bool)false);
            AddLabel(467, 400, 0, @"Nay, I humbly decline any");
            AddLabel(467, 420, 0, @"equipment.");
            AddLabel(110, 474, 52, @"(Be sure you're satisfied with your character, after this screen there is no turning back!)");

            if (_Humble)
                AddButton(438, 400, 211, 211, (int)Buttons.HumbleButton, GumpButtonType.Reply, 0);
            else
                AddButton(438, 400, 210, 211, (int)Buttons.HumbleButton, GumpButtonType.Reply, 0);

            int leftx = 374;
            int rightx = 545;
            int origionaly = 74;
            int ypos = 74;
            int xpos = 374;
            int verticalspace = 20;
            int horizontalspace = 29;
            int buttonxleft = 79;
            int buttonxright = 253;
            int buttonx = buttonxleft;


            int pagenum = 2;
            int count = 0;

            Point2D backbuttonlocation = new Point2D(470, 371);
            Point2D nextpagebuttonlocation = new Point2D(606, 371);
            Point2D pagetextlocation = new Point2D(519, 369);

            AddPage(CharacterCreatorSystem.MapPage(1, _DisplayPage));
            for (int x = 0; x < CharacterCreatorSystem.ItemTypes.Length; x++)
            {
                bool Isintable = false;
                if (EquipmentSelectedList != null)
                {
                    if (EquipmentSelectedList.Count > 0)
                    {
                        foreach (int num in EquipmentSelectedList)
                        {
                            if (x == num)
                                Isintable = true;
                        }
                    }
                }

                if (Isintable)
                {
                    AddButton(xpos, ypos, 211, 211, switchid + x, GumpButtonType.Reply, 0);
                }
                else
                {
                    AddButton(xpos, ypos, 210, 211, switchid + x, GumpButtonType.Reply, 0);
                }

                string text = CharacterCreatorSystem.ItemTypes[x].ToString().Replace("Server.Items.", "");

                if (text == "Axes")
                    text = "Axe";
                else if (text == "Maces")
                    text = "Mace";
                else if (text == "Shields")
                    text = "Wooden Shield";
                else if (text == "Swords")
                    text = "Sword";
                else if (text == "LightArmor")
                    text = "Light Armor";
                else if (text == "MediumArmor")
                    text = "Medium Armor";
                else if (text == "HeavyArmor")
                    text = "Heavy Armor";

                AddLabel(xpos + horizontalspace, ypos, 1153, Regex.Replace(text, "(\\B[A-Z])", " $1"));

                count++;
                ypos += verticalspace;

                if (count == 28)
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
                else if (count == 14)
                {
                    xpos = rightx;
                    ypos = origionaly;
                }

            }
        }

        public enum Buttons
        {
            PreviousButton,
            ContinueButton,
            HumbleButton
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            if (info.ButtonID >= 20000)//Button
            {

                if (EquipmentSelectedList != null)
                {

                    if (EquipmentSelectedList.Contains(info.ButtonID - 20000))
                    {
                        EquipmentSelectedList.Remove(info.ButtonID - 20000);
                    }
                    else
                    {
                        if (EquipmentSelectedList.Count + 1 <= 3)
                            EquipmentSelectedList.Add(info.ButtonID - 20000);
                    }
                }
                from.SendGump(new CharacterCreatorEquipmentGump(_IsAdvancedChar, false, EquipmentSelectedList, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID, CharacterCreatorSystem.GetSelectionPage(info.ButtonID - 20000, 28)));

            }
            else
            {
                switch (info.ButtonID)
                {
                    case (int)Buttons.PreviousButton:
                        {
                            if (_IsAdvancedChar)
                            {
                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            }
                            else
                            {
                                from.SendGump(new CharacterCreatorSkillsGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            }
                            break;
                        }
                    case (int)Buttons.ContinueButton:
                        {

                            if (_Humble)
                                from.SendGump(new CharacterCreatorHumbleGump(0, _IsAdvancedChar, false, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            else
                                from.SendGump(new CharacterCreatorMapGump(1, 0, 0, _IsAdvancedChar, false, EquipmentSelectedList, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            break;
                        }
                    case (int)Buttons.HumbleButton:
                        {
                            if (_Humble)
                                from.SendGump(new CharacterCreatorEquipmentGump(_IsAdvancedChar, false, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            else
                            {
                                from.SendGump(new CharacterCreatorEquipmentGump(_IsAdvancedChar, true, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            }
                            break;
                        }
                }
            }
        }
    }
}
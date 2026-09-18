using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.CharacterCreator;
using System.Collections.Generic;
using System.Linq;

namespace Server.Gumps
{
    public class CharacterCreatorAdvancedSkillsGump : Gump
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
        List<Skill> SkillsSelectedList;

        int[] FacialHairStyles = new int[] { };
        int _SelectedColor = 0;
        int _FacialHairStyleChoosen = 0;
        int _FacialHairHue;
        int _FacialHarID;

        int _Strength = 0;
        int _Dexterity = 0;
        int _Intellegence = 0;
        int _StatTotal = 0;
        private readonly int _DisplayPage;
        public CharacterCreatorAdvancedSkillsGump(List<Skill> skillsselectedlist, int strength, int intellegence, int dexterity, int xposs, int yposs, Mobile from, int selectedcharactercreatorcolor, string playername, bool isfemale, int skinHue, int raceID, int shirthue, int pantshue, int shoehue, int hairgumpselectedcolor, int hairgumphairstylechoosen, int hairhue, int hairid, int selectedcolor, int facialhairstyleselected, int facialhairhue, int facialhairid, int displayPage = 1)
            : base(50, 50)
        {
            _DisplayPage = displayPage;
            if (skillsselectedlist == null)
            {
                SkillsSelectedList = new List<Skill>();
            }
            else { SkillsSelectedList = skillsselectedlist; }
            _Strength = strength;
            _Dexterity = dexterity;
            _Intellegence = intellegence;
            _StatTotal = _Strength + _Dexterity + _Intellegence;
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
            AddBackground(124, 132, 205, 284, 3500);
            AddLabel(330, 26, 1153, @"Character Creator");
            AddImage(2, 16, 10440);
            if (SkillsSelectedList.Count == 7 && 100 - _StatTotal == 0)
                AddButton(405, 460, 12009, 12011, (int)Buttons.ContinueButton, GumpButtonType.Reply, 0);
            AddBackground(148, 73, 158, 22, 3000);
            AddLabel(191, 74, 1153, @"Advanced");
            // AddBackground(0, 0, 204, 22, 3000);
            AddButton(308, 460, 12015, 12017, (int)Buttons.PreviousButton, GumpButtonType.Reply, 0);

            AddHtml(145, 150, 163, 150, @"As an Advanced Character, you have the freedom to choose your skills and starting stats as you see fit.", (bool)false, (bool)false);

            int skillshue = 37;
            if (SkillsSelectedList.Count == 7)
                skillshue = 73;
            AddLabel(155, 380, skillshue, "Skills Selected : ");
            AddLabel(255, 380, skillshue, SkillsSelectedList.Count.ToString());
            AddLabel(262, 380, skillshue, " / 7");
            if (100 - _StatTotal == 0)
                AddLabel(155, 360, 73, "Stat Points Left : " + (100 - _StatTotal).ToString());
            else
                AddLabel(155, 360, 37, "Stat Points Left : " + (100 - _StatTotal).ToString());

            AddBackground(253, 329, 23, 23, 3000);
            AddBackground(253, 303, 23, 23, 3000);
            AddBackground(253, 275, 23, 23, 3000);
            AddLabel(255, 304, 0, _Dexterity.ToString());//dex
            AddLabel(255, 330, 0, _Intellegence.ToString());//int
            AddLabel(255, 276, 0, _Strength.ToString());//strength
            AddLabel(172, 274, 0, @"Strength");
            AddLabel(168, 302, 0, @"Dexterity");
            AddLabel(160, 328, 0, @"Intelligence");
            if (_Strength > 10)
                AddButton(234, 276, 56, 56, (int)Buttons.StrengthLowerButton, GumpButtonType.Reply, 0);//strength subtract
            if (_Strength < 60)
                AddButton(280, 276, 55, 55, (int)Buttons.StrengthRaiseButton, GumpButtonType.Reply, 0);//strength raise

            if (_Dexterity > 10)
                AddButton(234, 302, 56, 56, (int)Buttons.DexterityLowerButton, GumpButtonType.Reply, 0);//dexterity subtract
            if (_Dexterity < 60)
                AddButton(280, 302, 55, 55, (int)Buttons.DexterityRaiseButton, GumpButtonType.Reply, 0);//dexterity raise

            if (_Intellegence > 10)
                AddButton(234, 330, 56, 56, (int)Buttons.IntelligenceLowerButton, GumpButtonType.Reply, 0);//intelligence subtract
            if (_Intellegence < 60)
                AddButton(280, 330, 55, 55, (int)Buttons.IntelligenceRaiseButton, GumpButtonType.Reply, 0);//intelligence raise

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

            Point2D backbuttonlocation = new Point2D(470, 411);
            Point2D nextpagebuttonlocation = new Point2D(606, 411);
            Point2D pagetextlocation = new Point2D(519, 409);

            AddPage(CharacterCreatorSystem.MapPage(1, _DisplayPage));

            var skills = from.Skills.Select(skill => new KeyValuePair<SkillName, int>(skill.SkillName, (int)skill.SkillName)).ToList();

            skills.Sort((pair1, pair2) => pair1.Key.CompareTo(pair2.Key));

            for (int x = 0; x < skills.Count; x++)
            {
                bool Isintable = false;
                if (SkillsSelectedList != null)
                {
                    if (SkillsSelectedList.Count > 0)
                    {
                        foreach (Skill num in SkillsSelectedList)
                        {
                            if (skills[x].Value == num.SkillID)
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

                AddLabel(xpos + horizontalspace, ypos, 1153, skills[x].Key.ToString());

                count++;
                ypos += verticalspace;

                if (count == 32)
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
                else if (count == 16)
                {
                    xpos = rightx;
                    ypos = origionaly;
                }

            }
        }

        public enum Buttons
        {
            ContinueButton,
            PreviousButton,
            StrengthLowerButton,
            StrengthRaiseButton,
            DexterityLowerButton,
            DexterityRaiseButton,
            IntelligenceLowerButton,
            IntelligenceRaiseButton
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            if (info.ButtonID >= 20000)//Button
            {
                int index = info.ButtonID - 20000;

                if (SkillsSelectedList != null)
                {
                    var skills = from.Skills.Select(skill => new KeyValuePair<SkillName, int>(skill.SkillName, (int)skill.SkillName)).ToList();

                    skills.Sort((pair1, pair2) => pair1.Key.CompareTo(pair2.Key));

                    Skill selectedSkill = from.Skills[skills[index].Value];

                    if (SkillsSelectedList.Contains(selectedSkill))
                    {
                        SkillsSelectedList.Remove(selectedSkill);
                    }
                    else
                    {
                        if (SkillsSelectedList.Count + 1 <= 7)
                            SkillsSelectedList.Add(selectedSkill);
                    }
                }
                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID, CharacterCreatorSystem.GetSelectionPage(index, 32)));

            }
            else
            {

                switch (info.ButtonID)
                {
                    case (int)Buttons.ContinueButton:
                        {
                            from.SendGump(new CharacterCreatorEquipmentGump(true, false, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            break;
                        }
                    case (int)Buttons.PreviousButton:
                        {
                            from.SendGump(new CharacterCreatorSkillsGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            break;
                        }


                    case (int)Buttons.StrengthRaiseButton:
                        {
                            if (_StatTotal + 1 <= 100 && _Strength + 1 <= 60)

                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength + 1, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            else
                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));

                            break;
                        }
                    case (int)Buttons.StrengthLowerButton:
                        {
                            if (_Strength - 1 >= 10)

                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength - 1, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            else
                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));

                            break;
                        }
                    case (int)Buttons.DexterityLowerButton:
                        {
                            if (_Dexterity - 1 >= 10)

                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence, _Dexterity - 1, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            else
                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));

                            break;
                        }
                    case (int)Buttons.DexterityRaiseButton:
                        {
                            if (_StatTotal + 1 <= 100 && _Dexterity + 1 <= 60)

                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence, _Dexterity + 1, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            else
                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));

                            break;
                        }
                    case (int)Buttons.IntelligenceRaiseButton:
                        {
                            if (_StatTotal + 1 <= 100 && _Intellegence + 1 <= 60)

                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence + 1, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            else
                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));

                            break;
                        }
                    case (int)Buttons.IntelligenceLowerButton:
                        {
                            if (_Intellegence - 1 >= 10)

                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence - 1, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                            else
                                from.SendGump(new CharacterCreatorAdvancedSkillsGump(SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));

                            break;
                        }
                }
            }
        }
    }
}

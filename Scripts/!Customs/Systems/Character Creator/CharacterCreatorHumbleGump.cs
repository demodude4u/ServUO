using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using System.Collections.Generic;

namespace Server.Gumps
{
    public class CharacterCreatorHumbleGump : Gump
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
        int _StatNum = 0;
        public CharacterCreatorHumbleGump(int statnum, bool isAdvanced, bool humble, List<int> equipselected, List<Skill> skillsselectedlist, int strength, int intellegence, int dexterity, int xposs, int yposs, Mobile from, int selectedcharactercreatorcolor, string playername, bool isfemale, int skinHue, int raceID, int shirthue, int pantshue, int shoehue, int hairgumpselectedcolor, int hairgumphairstylechoosen, int hairhue, int hairid, int selectedcolor, int facialhairstyleselected, int facialhairhue, int facialhairid)
            : base(50, 50)
        {
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



            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;


            AddPage(0);
            AddBackground(48, 5, 700, 500, 9270);
            AddImage(716, 16, 10441);
            AddBackground(81, 62, 639, 389, 9200);
            AddLabel(330, 26, 1153, @"Character Creator");
            AddImage(2, 16, 10440);

            AddBackground(124, 192, 205, 22, 3000);
            AddLabel(141, 193, 1153, @"Thou art humble...");
            AddBackground(124, 224, 205, 219, 3500);
            AddHtml(151, 239, 152, 188, @"As a reward for thou act of Humility, you may receive an additional 5 points in any stat you wish.", (bool)false, (bool)false);
            AddImage(156, 84, 103);
            AddImage(195, 100, 108);
            AddLabel(545, 159, 0, @"+5 Strength");
            AddImage(409, 128, 1417);
            AddItem(432, 156, 3851);
            AddImage(409, 217, 1417);
            AddItem(431, 245, 3852);
            AddImage(409, 306, 1417);
            AddItem(432, 334, 3848);
            AddLabel(545, 248, 0, @"+5 Dexterity");
            AddLabel(545, 337, 0, @"+5 Intelligence");


            if (statnum == 1)
                AddButton(503, 154, 2154, 2154, (int)Buttons.StrengthCheckbox, GumpButtonType.Reply, 0);
            else
                AddButton(503, 154, 2151, 2154, (int)Buttons.StrengthCheckbox, GumpButtonType.Reply, 0);

            if (statnum == 2)
                AddButton(503, 242, 2154, 2154, (int)Buttons.DexterityCheckbox, GumpButtonType.Reply, 0);
            else
                AddButton(503, 242, 2151, 2154, (int)Buttons.DexterityCheckbox, GumpButtonType.Reply, 0);

            if (statnum == 3)
                AddButton(503, 332, 2154, 2154, (int)Buttons.IntelligenceCheckbox, GumpButtonType.Reply, 0);
            else
                AddButton(503, 332, 2151, 2154, (int)Buttons.IntelligenceCheckbox, GumpButtonType.Reply, 0);


            if (statnum > 0)
                AddButton(405, 460, 12009, 12011, (int)Buttons.ContinueButton, GumpButtonType.Reply, 0);

        }

        public enum Buttons
        {
            ContinueButton,
            StrengthCheckbox,
            DexterityCheckbox,
            IntelligenceCheckbox,
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case (int)Buttons.ContinueButton:
                    {
                        if (_StatNum == 1)
                            _Strength += 5;
                        else if (_StatNum == 2)
                            _Dexterity += 5;
                        else if (_StatNum == 3)
                            _Intellegence += 5;

                        from.SendGump(new CharacterCreatorMapGump(1, 0, 0, _IsAdvancedChar, false, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                        break;
                    }
                case (int)Buttons.StrengthCheckbox:
                    {
                        if (_StatNum == 1)
                            from.SendGump(new CharacterCreatorHumbleGump(0, _IsAdvancedChar, false, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                        else
                            from.SendGump(new CharacterCreatorHumbleGump(1, _IsAdvancedChar, false, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                        break;
                    }
                case (int)Buttons.DexterityCheckbox:
                    {
                        if (_StatNum == 2)
                            from.SendGump(new CharacterCreatorHumbleGump(0, _IsAdvancedChar, false, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                        else
                            from.SendGump(new CharacterCreatorHumbleGump(2, _IsAdvancedChar, false, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                        break;
                    }
                case (int)Buttons.IntelligenceCheckbox:
                    {
                        if (_StatNum == 3)
                            from.SendGump(new CharacterCreatorHumbleGump(0, _IsAdvancedChar, false, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                        else
                            from.SendGump(new CharacterCreatorHumbleGump(3, _IsAdvancedChar, false, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                        break;
                    }

            }
        }
    }
}
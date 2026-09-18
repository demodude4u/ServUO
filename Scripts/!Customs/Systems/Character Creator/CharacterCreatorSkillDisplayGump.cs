using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using System.Collections.Generic;

namespace Server.Gumps
{
    public class CharacterCreatorSkillDisplayGump : Gump
    {
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
        int _FacialHairHue;
        int _FacialHarID;

        int _Strength = 0;
        int _Dexterity = 0;
        int _Intellegence = 0;




        List<Skill> SkillsSelectedList = new List<Skill>();

        public CharacterCreatorSkillDisplayGump(int xposs, int yposs, Mobile from, int selectedcharactercreatorcolor, string playername, bool isfemale, int skinHue, int raceID, int shirthue, int pantshue, int shoehue, int hairgumpselectedcolor, int hairgumphairstylechoosen, int hairhue, int hairid, int selectedcolor, int facialhairstyleselected, int facialhairhue, int facialhairid, string profession, Skill skillone, Skill skilltwo, Skill skillthree, Skill skillfour, Skill skillfive, string skilldescription, int str, int dex, int intel, int skillicon)
            : base(50, 50)
        {

            SkillsSelectedList.Add(skillone);
            SkillsSelectedList.Add(skilltwo);
            SkillsSelectedList.Add(skillthree);
            SkillsSelectedList.Add(skillfour);
            SkillsSelectedList.Add(skillfive);

            _Intellegence = intel;
            _Dexterity = dex;
            _Strength = str;
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
            AddImage(221, 92, 1417);
            AddLabel(330, 26, 1153, @"Character Creator");
            AddImage(2, 16, 10440);
            AddButton(308, 460, 12015, 12017, (int)Buttons.PreviousButton, GumpButtonType.Reply, 0);
            AddButton(405, 460, 12009, 12011, (int)Buttons.ContinueButton, GumpButtonType.Reply, 0);
            AddBackground(438, 101, 179, 100, 3500);
            AddBackground(425, 227, 205, 184, 3500);

            string skilllist = String.Format(@"{0}
{1}
{2}
{3}
{4}", skillone.Name, skilltwo.Name, skillthree.Name, skillfour.Name, skillfive.Name);

            AddHtml(450, 243, 152, 151, skilllist, (bool)false, (bool)false);//SKILLS
            AddBackground(182, 179, 158, 22, 3000);
            AddLabel(222, 180, 1153, profession);//Profession
            AddBackground(157, 227, 205, 184, 3500);
            AddHtml(184, 243, 152, 151, "<basefont COLOR=#111111>" + skilldescription + "</font>", (bool)false, (bool)true);//Skill Description
            AddLabel(483, 116, 1153, @"Strength");
            AddBackground(564, 167, 23, 23, 3000);
            AddBackground(563, 141, 23, 23, 3000);
            AddBackground(563, 113, 23, 23, 3000);
            AddLabel(565, 142, 1153, dex.ToString());//Dex
            AddLabel(565, 168, 1153, intel.ToString());//Int
            AddLabel(565, 114, 1153, str.ToString());//str
            //AddTextEntry(565, 142, 20, 20, 1153, 1, dex.ToString());//Dex
            //AddTextEntry(565, 168, 20, 20, 1153, 2, intel.ToString());//Int
            //AddTextEntry(565, 114, 20, 20, 1153, 0, str.ToString());//str
            AddLabel(479, 144, 1153, @"Dexterity");
            AddLabel(470, 170, 1153, @"Intelligence");
            AddImage(230, 101, skillicon);





        }

        public enum Buttons
        {
            PreviousButton,
            ContinueButton,
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case (int)Buttons.PreviousButton:
                    {
                        from.SendGump(new CharacterCreatorSkillsGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                        break;
                    }
                case (int)Buttons.ContinueButton:
                    {
                        from.SendGump(new CharacterCreatorEquipmentGump(false, false, null, SkillsSelectedList, _Strength, _Intellegence, _Dexterity, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                        break;
                    }

            }
        }
    }
}
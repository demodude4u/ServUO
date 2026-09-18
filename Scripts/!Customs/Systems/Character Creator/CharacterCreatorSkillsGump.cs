using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Items;

namespace Server.Gumps
{
    public class CharacterCreatorSkillsGump : Gump
    {
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
        int _FacialHairHue;
        int _FacialHarID;
        public CharacterCreatorSkillsGump(int xposs, int yposs, Mobile from, int selectedcharactercreatorcolor, string playername, bool isfemale, int skinHue, int raceID, int shirthue, int pantshue, int shoehue, int hairgumpselectedcolor, int hairgumphairstylechoosen, int hairhue, int hairid, int selectedcolor, int facialhairstyleselected, int facialhairhue, int facialhairid)
            : base(50, 50)
        {
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
            AddLabel(330, 26, 1153, @"Character Creator");
            AddBackground(81, 62, 639, 389, 9200);
            AddLabel(282, 63, 1153, @"Choose a trade for your character");
            AddImage(2, 16, 10440);
            AddLabel(192, 82, 1153, @"(You can readjust your skills later at any time you'd like)");
            AddButton(308, 460, 12015, 12017, (int)Buttons.CancelButton, GumpButtonType.Reply, 0);

            AddButton(179, 317, 5567, 5568, (int)Buttons.MacerButton, GumpButtonType.Reply, 0);
            AddLabel(100, 339, 1153, @"Macer");

            AddButton(179, 249, 5561, 5562, (int)Buttons.FencerButton, GumpButtonType.Reply, 0);
            AddLabel(100, 271, 1153, @"Fencer");

            AddButton(654, 318, 5575, 5576, (int)Buttons.ThiefButton, GumpButtonType.Reply, 0);
            AddLabel(570, 338, 1153, @"Thief");

            AddButton(179, 386, 5591, 5592, (int)Buttons.SamuraiButton, GumpButtonType.Reply, 0);
            AddLabel(100, 408, 1153, @"Samurai");

            AddButton(345, 386, 5549, 5550, (int)Buttons.TamerButton, GumpButtonType.Reply, 0);
            AddLabel(250, 408, 1153, @"Tamer");

            AddButton(345, 317, 5587, 5588, (int)Buttons.PaladinButton, GumpButtonType.Reply, 0);
            AddLabel(250, 339, 1153, @"Paladin");

            AddButton(497, 384, 5579, 5580, (int)Buttons.TailorButton, GumpButtonType.Reply, 0);
            AddLabel(420, 405, 1153, @"Tailor");

            AddButton(654, 384, 5545, 5546, (int)Buttons.AdvancedButton, GumpButtonType.Reply, 0);
            AddLabel(570, 406, 1153, @"Advanced");

            AddButton(654, 249, 5563, 5564, (int)Buttons.ScribeButton, GumpButtonType.Reply, 0);
            AddLabel(570, 269, 1153, @"Scribe");

            AddButton(654, 181, 5553, 5554, (int)Buttons.BardButton, GumpButtonType.Reply, 0);
            AddLabel(570, 203, 1153, @"Bard");

            AddButton(654, 113, 5555, 5556, (int)Buttons.BlacksmithButton, GumpButtonType.Reply, 0);
            AddLabel(570, 134, 1153, @"Blacksmith");

            AddButton(497, 318, 5559, 5560, (int)Buttons.CarpenterButton, GumpButtonType.Reply, 0);
            AddLabel(420, 339, 1153, @"Carpenter");

            AddButton(497, 249, 5565, 5566, (int)Buttons.FishermanButton, GumpButtonType.Reply, 0);
            AddLabel(420, 271, 1153, @"Fisherman");

            AddButton(497, 181, 5573, 5574, (int)Buttons.MuleButton, GumpButtonType.Reply, 0);
            AddLabel(420, 203, 1153, @"Mule");

            AddButton(497, 113, 5583, 5584, (int)Buttons.AlchemistButton, GumpButtonType.Reply, 0);
            AddLabel(420, 134, 1153, @"Alchemist");

            AddButton(345, 249, 5557, 5558, (int)Buttons.NecromancerButton, GumpButtonType.Reply, 0);
            AddLabel(250, 272, 1153, @"Necromancer");

            AddButton(345, 181, 5569, 5570, (int)Buttons.MageButton, GumpButtonType.Reply, 0);
            AddLabel(250, 204, 1153, @"Mage");

            AddButton(345, 113, 5589, 5590, (int)Buttons.NinjaButton, GumpButtonType.Reply, 0);
            AddLabel(250, 136, 1153, @"Ninja");

            AddButton(179, 181, 5551, 5552, (int)Buttons.ArcherButton, GumpButtonType.Reply, 0);
            AddLabel(100, 203, 1153, @"Archer");

            AddButton(179, 113, 5577, 5578, (int)Buttons.SwordsmanButton, GumpButtonType.Reply, 0);
            AddLabel(100, 135, 1153, @"Swordsman");


        }

        public enum Buttons
        {
            CancelButton,
            MacerButton,
            FencerButton,
            ThiefButton,
            SamuraiButton,
            TamerButton,
            PaladinButton,
            TailorButton,
            AdvancedButton,
            ScribeButton,
            BardButton,
            BlacksmithButton,
            CarpenterButton,
            FishermanButton,
            MuleButton,
            AlchemistButton,
            NecromancerButton,
            MageButton,
            NinjaButton,
            ArcherButton,
            SwordsmanButton,
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case (int)Buttons.CancelButton:
                    {
                        if (_IsFemale || _RaceID == 1)
                        {
                            from.SendGump(new CharacterCreatorHairGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen));
                        }
                        else
                        {
                            from.SendGump(new CharacterCreatorFacialHairGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen));
                        }
                        break;
                    }
                case (int)Buttons.MacerButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Macer",
                            from.Skills[SkillName.Anatomy],
                            from.Skills[SkillName.Healing],
                            from.Skills[SkillName.Parry],
                            from.Skills[SkillName.Macing],
                            from.Skills[SkillName.Tactics],

                            "Striking their opponent with all of their might, shattering their shield in one powerful blow. The mace is a brutal weapon when paired with a strong arm and mind.", 50, 40, 10, 11975));
                        break;
                    }
                case (int)Buttons.FencerButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Fencer",
                                      from.Skills[SkillName.Anatomy],
                            from.Skills[SkillName.Healing],
                            from.Skills[SkillName.Parry],
                            from.Skills[SkillName.Fencing],
                            from.Skills[SkillName.Tactics],

                            "Parry, riposte, and thrust! Their feet moves as quickly as their blade as they seek an opening in their opponent's defense. A truly skilled fence is agile both in body and mind.", 50, 40, 10, 11967));
                        break;
                    }
                case (int)Buttons.ThiefButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Thief",

                                     from.Skills[SkillName.Hiding],
                            from.Skills[SkillName.Lockpicking],
                            from.Skills[SkillName.RemoveTrap],
                            from.Skills[SkillName.Stealing],
                            from.Skills[SkillName.Snooping],
                            "Light-fingered and adept at stealth, the Thief will steal to achieve his/her ends.", 50, 15, 35, 11981));
                        break;
                    }
                case (int)Buttons.SamuraiButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Samurai",
                                     from.Skills[SkillName.Anatomy],
                            from.Skills[SkillName.Healing],
                            from.Skills[SkillName.Bushido],
                            from.Skills[SkillName.Swords],
                            from.Skills[SkillName.Tactics],


                            "Powerful, loyal, and skilled in weaponry, a Samurai is a master of combat.", 50, 30, 20, 11983));
                        break;
                    }
                case (int)Buttons.TamerButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Tamer",

                             from.Skills[SkillName.AnimalLore],
                             from.Skills[SkillName.AnimalTaming],
                             from.Skills[SkillName.Magery],
                             from.Skills[SkillName.Meditation],
                             from.Skills[SkillName.Veterinary],

                            "The man or woman skilled at Taming can make use of the power and strength of the creatures of the wild.", 40, 10, 50, 11985));
                        break;
                    }
                case (int)Buttons.PaladinButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Paladin",

                            from.Skills[SkillName.Chivalry],
                            from.Skills[SkillName.Focus],
                            from.Skills[SkillName.Parry],
                            from.Skills[SkillName.Swords],
                            from.Skills[SkillName.Tactics],

                            "The Paladin hones his skills through virtue, righteousness, and focus on an essential belief in the greater good in the world. ", 50, 40, 10, 11977));
                        break;
                    }
                case (int)Buttons.TailorButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Tailor",
                                     from.Skills[SkillName.Herding],
                            from.Skills[SkillName.Magery],
                            from.Skills[SkillName.Mining],
                            from.Skills[SkillName.Tailoring],
                            from.Skills[SkillName.Tinkering],


                            "Skilled in needle and thread, the tailor is ready to provide (and repair) well-fitted and flattering garments for all occasions that the adventurer may encounter.", 50, 25, 25, 11987));
                        break;
                    }
                case (int)Buttons.AdvancedButton:
                    {
                        from.SendGump(new CharacterCreatorAdvancedSkillsGump(null, 25, 25, 25, this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID));
                        break;
                    }
                case (int)Buttons.ScribeButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Scribe",
                                     from.Skills[SkillName.Cooking],
                            from.Skills[SkillName.Inscribe],
                            from.Skills[SkillName.Lumberjacking],
                            from.Skills[SkillName.Magery],
                            from.Skills[SkillName.Meditation],


                            "Long hours transcribing his wisdom upon parchment with both quill and ink - Scribes across the land can be found practicing the art of spell writhin, for no spellbook is complete without the very spells found within.", 50, 10, 40, 11973));
                        break;
                    }
                case (int)Buttons.BardButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Bard",
                                     from.Skills[SkillName.Discordance],
                            from.Skills[SkillName.Magery],
                            from.Skills[SkillName.Musicianship],
                            from.Skills[SkillName.Peacemaking],
                            from.Skills[SkillName.Provocation],


                            "Weaving story into song, talented in voice and instrument, the Bard uses these songs to bolster the morale and resolve of his comrades.", 50, 10, 40, 11965));
                        break;
                    }
                case (int)Buttons.BlacksmithButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Blacksmith",
                                     from.Skills[SkillName.ArmsLore],
                            from.Skills[SkillName.Blacksmith],
                            from.Skills[SkillName.Imbuing],
                            from.Skills[SkillName.Mining],
                            from.Skills[SkillName.Tinkering],


                            "In war or in peace, the strength and talent of the Blacksmith is integral to all of society.", 50, 30, 20, 11957));
                        break;
                    }
                case (int)Buttons.CarpenterButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Carpenter",
                                     from.Skills[SkillName.Carpentry],
                            from.Skills[SkillName.Fletching],
                            from.Skills[SkillName.Lumberjacking],
                            from.Skills[SkillName.Mining],
                            from.Skills[SkillName.Tinkering],


                            "Benches, tables, walls, houses, ships, there is little that a Carpenter's skill in shaping wood cannot provide.", 50, 35, 15, 11979));
                        break;
                    }
                case (int)Buttons.FishermanButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Fisherman",
                                     from.Skills[SkillName.Cartography],
                            from.Skills[SkillName.Fishing],
                            from.Skills[SkillName.Lockpicking],
                            from.Skills[SkillName.Magery],
                            from.Skills[SkillName.RemoveTrap],


                            "With the right bait, a proper casting technique, patience and a little luck, the humble fisherman will receive all the spoils the shores have to offer.", 50, 10, 40, 11971));
                        break;
                    }
                case (int)Buttons.MuleButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Mule",
                                     from.Skills[SkillName.Fishing],
                            from.Skills[SkillName.Lumberjacking],
                            from.Skills[SkillName.Magery],
                            from.Skills[SkillName.Mining],
                            from.Skills[SkillName.Tinkering],


                            "The most resourceful of all other trades. The mule always has the proper skills and tools to procure a resource when need be.", 50, 25, 25, 11963));
                        break;
                    }
                case (int)Buttons.AlchemistButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Alchemist",
                                     from.Skills[SkillName.Alchemy],
                            from.Skills[SkillName.Magery],
                            from.Skills[SkillName.Meditation],
                            from.Skills[SkillName.Poisoning],
                            from.Skills[SkillName.TasteID],


                            "Your fingers are stained by strange chemicals, and your hair is slightly burnt, but the reward of your long efforts sits bubbling in a flask before you.", 50, 10, 40, 11955));
                        break;
                    }
                case (int)Buttons.NecromancerButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Necromancer",
                            from.Skills[SkillName.EvalInt],
                            from.Skills[SkillName.Meditation],
                            from.Skills[SkillName.Necromancy],
                            from.Skills[SkillName.SpiritSpeak],
                            from.Skills[SkillName.MagicResist],


                            "Watch what was once dead walk again. The powers of the Necromancer are thick with evil, raising the slumbering spirits of the dead to roam Britannia once more.", 50, 10, 40, 11969));
                        break;
                    }
                case (int)Buttons.MageButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Mage",
                                     from.Skills[SkillName.EvalInt],
                            from.Skills[SkillName.Magery],
                            from.Skills[SkillName.Meditation],
                            from.Skills[SkillName.Inscribe],
                            from.Skills[SkillName.MagicResist],


                            "Long study of ancient lore has given the Mage wisdom and ability to utilize herbs and chemicals to create magic.", 40, 10, 50, 11961));
                        break;
                    }
                case (int)Buttons.NinjaButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Ninja",
                                     from.Skills[SkillName.Fencing],
                            from.Skills[SkillName.Hiding],
                            from.Skills[SkillName.Ninjitsu],
                            from.Skills[SkillName.Stealth],
                            from.Skills[SkillName.Tactics],

                            "Sleek and silent, ninjas can evade detection and will not stop until they have achieved their goal.", 40, 40, 20, 11953));
                        break;
                    }
                case (int)Buttons.ArcherButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Archer",
                                     from.Skills[SkillName.Anatomy],
                            from.Skills[SkillName.Healing],
                            from.Skills[SkillName.Fletching],
                            from.Skills[SkillName.Archery],
                            from.Skills[SkillName.Tactics],


                            "The deer stands perfectly still as the archer takes aim with their bow, patient and calm. Letting their arrow fly, striking both straight and true.Archery is the art of the Hunter and the Ranger.", 40, 50, 10, 11959));
                        break;
                    }
                case (int)Buttons.SwordsmanButton:
                    {
                        from.SendGump(new CharacterCreatorSkillDisplayGump(this.X, this.Y, from, SelectedCharacterCreatorColor, _PlayerName, _IsFemale, _SkinHue, _RaceID, _ShirtHue, _PantsHue, _ShoeHue, HairGumpHairColorChoosen, HairGumpHairChoosen, _HairHue, _HairStyle, _SelectedColor, _FacialHairStyleChoosen, _FacialHairHue, _FacialHarID,
                            "Swordsman",
                                     from.Skills[SkillName.Anatomy],
                            from.Skills[SkillName.Healing],
                            from.Skills[SkillName.Parry],
                            from.Skills[SkillName.Swords],
                            from.Skills[SkillName.Tactics],

                            "All weapons require skill, but to see a sword wielded properly is to see a work of art.", 50, 40, 10, 11951));
                        break;
                    }

            }
        }
    }
}

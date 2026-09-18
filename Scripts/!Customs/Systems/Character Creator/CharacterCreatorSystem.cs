using Server.Accounting;
using Server.Commands;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.CharacterCreator
{
    public class CharacterCreatorSystem
    {
        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(EventSink_Login);
            EventSink.ServerList += EventSink_ServerList;
            CommandSystem.Register("CCCheat", AccessLevel.GameMaster, CharacterCreatorCheat_OnCommand);
            
        }

        [Usage("CCCheat NAME")]
        [Description("Sets your name to (NAME) and removes Character Creator GUMP")]
        public static void CharacterCreatorCheat_OnCommand(CommandEventArgs e)
        {
            string NAME = e.ArgString.Trim();
            Mobile m = e.Mobile;

            if (NAME.Length > 0)
            {
                m.Name = NAME;
                m.CloseAllGumps();
                m.Race = Race.Human;
                m.Hue = Race.Human.RandomSkinHue();
            }
        }
        public static void EventSink_Login(LoginEventArgs e)
        {
            if (e.Mobile != null)
            {
                if (e.Mobile.Name == "New Character" || !CharacterCreatorGump.CheckDupe(e.Mobile, e.Mobile.Name))
                {
                    e.Mobile.CantWalk = true;
                    e.Mobile.SendGump(new CharacterCreatorGump(e.Mobile));
                    e.Mobile.Blessed = true;
                }
                else if(e.Mobile.Race == Race.Orc)
                {
                    if(e.Mobile.FindItemOnLayer(Layer.Face) == null)
                    {
                        OrcFace face = new OrcFace();
                        e.Mobile.EquipItem(face);
                        face.Movable = false;
                        face.Hue = e.Mobile.Hue;
                    }
                }
            }
        }

        private static void EventSink_ServerList(ServerListEventArgs e)
        {
            if (e.Account is Account account)
            {
                AddGenericCharacters(account);
            }
        }

        private static void AddGenericCharacters(Account account)
        {
            for (int i = 0; i < account.Length; ++i)
            {
                Mobile character = account[i];

                if (character != null && character.Name == "New Character")
                {
                    character.Map = Map.Internal;
                    character.Location = Point3D.Zero;
                }
            }

            while (account.Count < account.Limit)
            {
                AddGenericCharacter(account);
            }
        }

        private static void AddGenericCharacter(Account account)
        {
            Mobile newChar = CreateMobile(account);

            if (newChar == null)
            {
                return;
            }

            newChar.Player = true;
            newChar.AccessLevel = account.AccessLevel;
            newChar.Female = false;
            newChar.Race = Race.DefaultRace;
            newChar.Hue = 1;
            newChar.Hunger = 20;
            newChar.Name = "New Character";
            newChar.CantWalk = true;
            newChar.Frozen = true;
            newChar.Squelched = true;
            newChar.Blessed = true;
            newChar.BodyValue = 1;

            var backpack = new Backpack
            {
                Movable = false
            };

            newChar.EquipItem(backpack);
        }

        private static Mobile CreateMobile(Account account)
        {
            if (account.Count >= account.Limit)
            {
                return null;
            }

            for (int i = 0; i < account.Length; ++i)
            {
                if (account[i] == null)
                {
                    return account[i] = new PlayerMobile();
                }
            }

            return null;
        }

        public static int GetSelectionPage(int index)
        {
            return index < 21 ? 1 : ((index - 1) / 20) + 1;
        }

        public static int GetSelectionPage(int index, int firstPageSize)
        {
            return index < firstPageSize ? 1 : ((index - firstPageSize) / firstPageSize) + 2;
        }

        public static int MapPage(int page, int selectedPage)
        {
            if (selectedPage <= 1)
            {
                return page;
            }

            if (page == 1)
            {
                return selectedPage;
            }

            return page == selectedPage ? 1 : page;
        }

        public static Type[] ItemTypes = new Type[]
        {
            typeof(BagOfFood), typeof(BagOfPotions),typeof(Bandage),typeof(BlankScroll),typeof(Board),
            typeof(IronIngot),typeof(Lockpick),typeof(BagOfReagents),typeof(BagOfNecroReagents),typeof(BookOfBushido),
            typeof(BookOfChivalry),typeof(Maces),typeof(BookOfNinjitsu),typeof(Swords),typeof(Axes),
            typeof(Instrument),typeof(NecromancerSpellbook),typeof(Spellbook),typeof(Shields),typeof(LightArmor),
            typeof(MediumArmor),typeof(HeavyArmor),typeof(MapmakersPen),typeof(FletcherTools),typeof(MortarPestle),typeof(Pickaxe),
            typeof(RollingPin),typeof(Tongs),typeof(Saw),typeof(ScribesPen),typeof(SewingKit),typeof(TinkersTools),typeof(MysticBook)
        };
    }
}

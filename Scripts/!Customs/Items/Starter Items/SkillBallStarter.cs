using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Misc;
// COPYRIGHT BY ROMANTHEBRAIN
namespace Server
{
    public class SkilllimPickGump : Gump
    {

        private static string GetSkillDisplayName(SkillName skill)
        {
            switch (skill)
            {
                case SkillName.Alchemy: return "Alchemy";
                case SkillName.Anatomy: return "Anatomy";
                case SkillName.AnimalLore: return "Animal Lore";
                case SkillName.ItemID: return "Item Identification";
                case SkillName.ArmsLore: return "Arms Lore";
                case SkillName.Parry: return "Parrying";
                case SkillName.Begging: return "Begging";
                case SkillName.Blacksmith: return "Blacksmithy";
                case SkillName.Fletching: return "Fletching";
                case SkillName.Peacemaking: return "Peacemaking";
                case SkillName.Camping: return "Camping";
                case SkillName.Carpentry: return "Carpentry";
                case SkillName.Cartography: return "Cartography";
                case SkillName.Cooking: return "Cooking";
                case SkillName.DetectHidden: return "Detect Hidden";
                case SkillName.Discordance: return "Discordance";
                case SkillName.EvalInt: return "Evaluating Intelligence";
                case SkillName.Healing: return "Healing";
                case SkillName.Fishing: return "Fishing";
                case SkillName.Forensics: return "Forensic Evaluation";
                case SkillName.Herding: return "Herding";
                case SkillName.Hiding: return "Hiding";
                case SkillName.Provocation: return "Provocation";
                case SkillName.Inscribe: return "Inscription";
                case SkillName.Lockpicking: return "Lockpicking";
                case SkillName.Magery: return "Magery";
                case SkillName.MagicResist: return "Resisting Spells";
                case SkillName.Tactics: return "Tactics";
                case SkillName.Snooping: return "Snooping";
                case SkillName.Musicianship: return "Musicianship";
                case SkillName.Poisoning: return "Poisoning";
                case SkillName.Archery: return "Archery";
                case SkillName.SpiritSpeak: return "Spirit Speak";
                case SkillName.Stealing: return "Stealing";
                case SkillName.Tailoring: return "Tailoring";
                case SkillName.AnimalTaming: return "Animal Taming";
                case SkillName.TasteID: return "Taste Identification";
                case SkillName.Tinkering: return "Tinkering";
                case SkillName.Tracking: return "Tracking";
                case SkillName.Veterinary: return "Veterinary";
                case SkillName.Swords: return "Swordsmanship";
                case SkillName.Macing: return "Mace Fighting";
                case SkillName.Fencing: return "Fencing";
                case SkillName.Wrestling: return "Wrestling";
                case SkillName.Lumberjacking: return "Lumberjacking";
                case SkillName.Mining: return "Mining";
                case SkillName.Meditation: return "Meditation";
                case SkillName.Stealth: return "Stealth";
                case SkillName.RemoveTrap: return "Remove Trap";
                case SkillName.Necromancy: return "Necromancy";
                case SkillName.Focus: return "Focus";
                case SkillName.Chivalry: return "Chivalry";
                case SkillName.Bushido: return "Bushido";
                case SkillName.Ninjitsu: return "Ninjitsu";
                case SkillName.Spellweaving: return "Spellweaving";
                case SkillName.Mysticism: return "Mysticism";
                case SkillName.Imbuing: return "Imbuing";
                case SkillName.Throwing: return "Throwing";
                default: return skill.ToString();
            }
        }


        private int switches = 7;
        private SkillBallStarter m_SkillBall;
        private double val = 50;
        private static Item MakeNewbie( Item item )
        {
            if ( !Core.AOS )
            	item.LootType = LootType.Newbied;
 
            return item;
        }

        public SkilllimPickGump(SkillBallStarter ball)
            : base(0, 0)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = true;
            m_SkillBall = ball;
            
            this.AddPage(0);
            this.AddBackground(39, 33, 750, 500, 5120);
            this.AddLabel(67, 41, 1153, @"Please choose your 7 skills to increase to 50 skill points. - WARNING: This will reset all currently trained skills!");
            this.AddButton(80, 500, 2071, 2072, (int)Buttons.Close, GumpButtonType.Reply, 0);
            this.AddBackground(52, 60, 720, 430, 9350);
            this.AddImage(610, 338, 9000);
            this.AddPage(1);
            this.AddButton(690, 500, 2311, 2312, (int)Buttons.FinishButton, GumpButtonType.Reply, 0);
//				Button X, Button Y,             
            this.AddCheck(55, 65, 210, 211, false, (int)SkillName.Alchemy);
            this.AddCheck(55, 90, 210, 211, false, (int)SkillName.Anatomy);
            this.AddCheck(55, 115, 210, 211, false, (int)SkillName.AnimalLore);
            this.AddCheck(55, 140, 210, 211, false, (int)SkillName.AnimalTaming);
            this.AddCheck(55, 165, 210, 211, false, (int)SkillName.Archery);
            this.AddCheck(55, 190, 210, 211, false, (int)SkillName.ArmsLore);
            this.AddCheck(55, 215, 210, 211, false, (int)SkillName.Begging);
            this.AddCheck(55, 240, 210, 211, false, (int)SkillName.Blacksmith);
            this.AddCheck(55, 265, 210, 211, false, (int)SkillName.Bushido);
            this.AddCheck(55, 290, 210, 211, false, (int)SkillName.Camping);
            this.AddCheck(55, 315, 210, 211, false, (int)SkillName.Carpentry);
            this.AddCheck(55, 340, 210, 211, false, (int)SkillName.Cartography);
            this.AddCheck(55, 365, 210, 211, false, (int)SkillName.Chivalry);
            this.AddCheck(55, 390, 210, 211, false, (int)SkillName.Cooking);
            this.AddCheck(55, 415, 210, 211, false, (int)SkillName.DetectHidden);
            this.AddCheck(55, 440, 210, 211, false, (int)SkillName.Discordance);
            this.AddCheck(55, 465, 210, 211, false, (int)SkillName.EvalInt);
            this.AddLabel(80, 65, 0, GetSkillDisplayName(SkillName.Alchemy));
            this.AddLabel(80, 90, 0, GetSkillDisplayName(SkillName.Anatomy));
            this.AddLabel(80, 115, 0, GetSkillDisplayName(SkillName.AnimalLore));
            this.AddLabel(80, 140, 0, GetSkillDisplayName(SkillName.AnimalTaming));
            this.AddLabel(80, 165, 0, GetSkillDisplayName(SkillName.Archery));
            this.AddLabel(80, 190, 0, GetSkillDisplayName(SkillName.ArmsLore));
            this.AddLabel(80, 215, 0, GetSkillDisplayName(SkillName.Begging));
            this.AddLabel(80, 240, 0, GetSkillDisplayName(SkillName.Blacksmith));
            this.AddLabel(80, 265, 0, GetSkillDisplayName(SkillName.Bushido));
            this.AddLabel(80, 290, 0, GetSkillDisplayName(SkillName.Camping));
            this.AddLabel(80, 315, 0, GetSkillDisplayName(SkillName.Carpentry));
            this.AddLabel(80, 340, 0, GetSkillDisplayName(SkillName.Cartography));
            this.AddLabel(80, 365, 0, GetSkillDisplayName(SkillName.Chivalry));
            this.AddLabel(80, 390, 0, GetSkillDisplayName(SkillName.Cooking));
            this.AddLabel(80, 415, 0, GetSkillDisplayName(SkillName.DetectHidden));
            this.AddLabel(80, 440, 0, GetSkillDisplayName(SkillName.Discordance));
            this.AddLabel(80, 465, 0, GetSkillDisplayName(SkillName.EvalInt));
            
            // ********************************************************
            
            this.AddCheck(240, 65, 210, 211, false, (int)SkillName.Fencing);
            this.AddCheck(240, 90, 210, 211, false, (int)SkillName.Fishing);
            this.AddCheck(240, 115, 210, 211, false, (int)SkillName.Fletching);
            this.AddCheck(240, 140, 210, 211, false, (int)SkillName.Focus);
            this.AddCheck(240, 165, 210, 211, false, (int)SkillName.Forensics);
            this.AddCheck(240, 190, 210, 211, false, (int)SkillName.Healing);
            this.AddCheck(240, 215, 210, 211, false, (int)SkillName.Herding);
            this.AddCheck(240, 240, 210, 211, false, (int)SkillName.Hiding);            
            this.AddCheck(240, 265, 210, 211, false, (int)SkillName.Imbuing);
            this.AddCheck(240, 290, 210, 211, false, (int)SkillName.Inscribe);
            this.AddCheck(240, 315, 210, 211, false, (int)SkillName.ItemID);
            this.AddCheck(240, 340, 210, 211, false, (int)SkillName.Lockpicking);
            this.AddCheck(240, 365, 210, 211, false, (int)SkillName.Lumberjacking);
            this.AddCheck(240, 390, 210, 211, false, (int)SkillName.Macing);
            this.AddCheck(240, 415, 210, 211, false, (int)SkillName.Magery);
            this.AddCheck(240, 440, 210, 211, false, (int)SkillName.MagicResist);
            this.AddCheck(240, 465, 210, 211, false, (int)SkillName.Meditation);
            this.AddLabel(265, 65, 0, GetSkillDisplayName(SkillName.Fencing));
            this.AddLabel(265, 90, 0, GetSkillDisplayName(SkillName.Fishing));
            this.AddLabel(265, 115, 0, GetSkillDisplayName(SkillName.Fletching));
            this.AddLabel(265, 140, 0, GetSkillDisplayName(SkillName.Focus));
            this.AddLabel(265, 165, 0, GetSkillDisplayName(SkillName.Forensics));
            this.AddLabel(265, 190, 0, GetSkillDisplayName(SkillName.Healing));
            this.AddLabel(265, 215, 0, GetSkillDisplayName(SkillName.Herding));
            this.AddLabel(265, 240, 0, GetSkillDisplayName(SkillName.Hiding));
            this.AddLabel(265, 265, 0, GetSkillDisplayName(SkillName.Imbuing));
            this.AddLabel(265, 290, 0, GetSkillDisplayName(SkillName.Inscribe));
            this.AddLabel(265, 315, 0, GetSkillDisplayName(SkillName.ItemID));
            this.AddLabel(265, 340, 0, GetSkillDisplayName(SkillName.Lockpicking));
            this.AddLabel(265, 365, 0, GetSkillDisplayName(SkillName.Lumberjacking));
            this.AddLabel(265, 390, 0, GetSkillDisplayName(SkillName.Macing));
            this.AddLabel(265, 415, 0, GetSkillDisplayName(SkillName.Magery));
            this.AddLabel(265, 440, 0, GetSkillDisplayName(SkillName.MagicResist));
            this.AddLabel(265, 465, 0, GetSkillDisplayName(SkillName.Meditation));
            
            // ********************************************************
            
            this.AddCheck(425, 65, 210, 211, false, (int)SkillName.Mining);
            this.AddCheck(425, 90, 210, 211, false, (int)SkillName.Musicianship);
            this.AddCheck(425, 115, 210, 211, false, (int)SkillName.Mysticism);
            this.AddCheck(425, 140, 210, 211, false, (int)SkillName.Necromancy);
            this.AddCheck(425, 165, 210, 211, false, (int)SkillName.Ninjitsu);
            this.AddCheck(425, 190, 210, 211, false, (int)SkillName.Parry);
            this.AddCheck(425, 215, 210, 211, false, (int)SkillName.Peacemaking);
            this.AddCheck(425, 240, 210, 211, false, (int)SkillName.Poisoning);
            this.AddCheck(425, 265, 210, 211, false, (int)SkillName.Provocation);
            this.AddCheck(425, 290, 210, 211, false, (int)SkillName.RemoveTrap);
            this.AddCheck(425, 315, 210, 211, false, (int)SkillName.Snooping);
            this.AddCheck(425, 340, 210, 211, false, (int)SkillName.Spellweaving);
            this.AddCheck(425, 365, 210, 211, false, (int)SkillName.SpiritSpeak);
            this.AddCheck(425, 390, 210, 211, false, (int)SkillName.Stealing);
            this.AddCheck(425, 415, 210, 211, false, (int)SkillName.Stealth);
            this.AddCheck(425, 440, 210, 211, false, (int)SkillName.Swords);
            this.AddCheck(425, 465, 210, 211, false, (int)SkillName.Tactics);
            this.AddLabel(450, 65, 0, GetSkillDisplayName(SkillName.Mining));
            this.AddLabel(450, 90, 0, GetSkillDisplayName(SkillName.Musicianship));
            this.AddLabel(450, 115, 0, GetSkillDisplayName(SkillName.Mysticism));
            this.AddLabel(450, 140, 0, GetSkillDisplayName(SkillName.Necromancy));
            this.AddLabel(450, 165, 0, GetSkillDisplayName(SkillName.Ninjitsu));
            this.AddLabel(450, 190, 0, GetSkillDisplayName(SkillName.Parry));
            this.AddLabel(450, 215, 0, GetSkillDisplayName(SkillName.Peacemaking));
            this.AddLabel(450, 240, 0, GetSkillDisplayName(SkillName.Poisoning));
            this.AddLabel(450, 265, 0, GetSkillDisplayName(SkillName.Provocation));
            this.AddLabel(450, 290, 0, GetSkillDisplayName(SkillName.RemoveTrap));
            this.AddLabel(450, 315, 0, GetSkillDisplayName(SkillName.Snooping));
            this.AddLabel(450, 340, 0, GetSkillDisplayName(SkillName.Spellweaving));
            this.AddLabel(450, 365, 0, GetSkillDisplayName(SkillName.SpiritSpeak));
            this.AddLabel(450, 390, 0, GetSkillDisplayName(SkillName.Stealing));
            this.AddLabel(450, 415, 0, GetSkillDisplayName(SkillName.Stealth));
            this.AddLabel(450, 440, 0, GetSkillDisplayName(SkillName.Swords));
            this.AddLabel(450, 465, 0, GetSkillDisplayName(SkillName.Tactics));

            //**********************************************************

            this.AddCheck(610, 65, 210, 211, false, (int)SkillName.Tailoring);            
            this.AddCheck(610, 90, 210, 211, false, (int)SkillName.TasteID);           
            this.AddCheck(610, 115, 210, 211, false, (int)SkillName.Throwing);
            this.AddCheck(610, 140, 210, 211, false, (int)SkillName.Tinkering);
            this.AddCheck(610, 165, 210, 211, false, (int)SkillName.Tracking);
            this.AddCheck(610, 190, 210, 211, false, (int)SkillName.Veterinary);
            this.AddCheck(610, 215, 210, 211, false, (int)SkillName.Wrestling); 
            this.AddLabel(635, 65, 0, GetSkillDisplayName(SkillName.Tailoring));
            this.AddLabel(635, 90, 0, GetSkillDisplayName(SkillName.TasteID));
            this.AddLabel(635, 115, 0, GetSkillDisplayName(SkillName.Throwing));
            this.AddLabel(635, 140, 0, GetSkillDisplayName(SkillName.Tinkering));
            this.AddLabel(635, 165, 0, GetSkillDisplayName(SkillName.Tracking));
            this.AddLabel(635, 190, 0, GetSkillDisplayName(SkillName.Veterinary));
            this.AddLabel(635, 215, 0, GetSkillDisplayName(SkillName.Wrestling));
            
            //**********************************************************
        }

        public enum Buttons
        {
            Close,
            FinishButton,

        }
        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile m = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: { break; }
                case 1:
                    {

                        if (info.Switches.Length < switches)
                        {
                            m.SendGump(new SkilllimPickGump(m_SkillBall));
                            m.SendMessage(0, "You must pick {0} more skills.", switches - info.Switches.Length);
                            break;
                        }
                        else if (info.Switches.Length > switches)
                        {
                            m.SendGump(new SkilllimPickGump(m_SkillBall));
                            m.SendMessage(0, "Please get rid of {0} skills, you have exceeded the 7 skills that are allowed.", info.Switches.Length - switches);
                            break;

                        }
                                                       
                        else
                        {
                            Server.Skills skills = m.Skills;

                            for (int i = 0; i < skills.Length; ++i)
                                skills[i].Base = 0;
                            for (int i = 0; i < 58; ++i)
                            {
                                if (info.IsSwitched(i))
                                    m.Skills[i].Base = val;
                            }
                            
							m_SkillBall.Delete();

                        }
					}
							
				break;
			}
		}
	}
 
    public class SkillBallStarter : Item
    {
        [Constructable]
        public SkillBallStarter() :  base( 0xE73 )
        {
            Weight = 1.0;
            Hue = 1161;
            Name = "Quick Start 7x Skill Booster";
            Movable =  true;
        }
        public override void OnDoubleClick( Mobile m )
        {
 
            if (m.Backpack != null && m.Backpack.GetAmount(typeof(SkillBallStarter)) > 0)
            {
                m.SendMessage("Please choose your 7 skills to increase to 50 skill points.");
                m.CloseGump(typeof(SkilllimPickGump));
                m.SendGump(new SkilllimPickGump(this));
            }
            else
                m.SendMessage(" This must be in your backpack to function.");
           
        }
 
        public SkillBallStarter( Serial serial ) : base( serial )
        {
        }
 
		public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int) 1 ); // version
        }
 
	    public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
		}
     
    }     
}

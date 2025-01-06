using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class MutteringStargazer : MondainQuester
    {
		private DateTime m_NextMutter;
        public override Type[] Quests
        { 
            get
            {
                return new Type[] 
                {
                    typeof(CelestialPuzzleBoxQuest)
                };
            }
        }
		
        [Constructable]
        public MutteringStargazer()
            : base("Nicolaus Copernicus", "The Stargazer")
        { 
            this.SetSkill(SkillName.Magery, 120.0, 120.0);
            this.SetSkill(SkillName.EvalInt, 120.0, 120.0);
            this.SetSkill(SkillName.Meditation, 120.0, 120.0);
            this.SetSkill(SkillName.Inscribe, 120.0, 120.0);
            this.SetSkill(SkillName.Alchemy, 120.0, 120.0);
            this.SetSkill(SkillName.Focus, 120.0, 120.0);
			this.m_NextMutter = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.Random(10, 30));
        }
		
        public MutteringStargazer(Serial serial)
            : base(serial)
        {
        }
		
        public override void Advertise()
        {
            this.Say("*mutters quietly* I know there's a pattern here... You there! Looking for work?"); // Learning of the body will allow you to excel in combat.
        }
		
        public override void OnOfferFailed()
        { 
            this.Say(1077772); // I cannot teach you, for you know all I can teach!
        }
		
        public override void InitBody()
        { 
            this.Female = false;
            this.CantWalk = true;
            this.Race = Race.Elf;		
		
            base.InitBody();
        }
		
        public override void InitOutfit()
        {
            this.AddItem(new Backpack());			
            this.AddItem(new Spellbook());	
            this.AddItem(new ElvenBoots(1109));
            this.AddItem(new MaleElvenRobe(1109));	
        }
		
		public override void OnThink()
		{
			if (DateTime.UtcNow > this.m_NextMutter)
			{
				this.m_NextMutter = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.Random(10, 30));
				switch(Utility.Random(9))
				{
					case 0:
					{
						this.Say("*grumbles something about celestial coordinates*");
						break;
					}
					case 1:
					{
						this.Say("Now if I cross-reference that with... yes...");
						break;
					}
					case 2:
					{
						this.Say("*mutters incomprehensible calculations*");
						break;
					}
					case 3:
					{
						this.Say("No, I did the math wrong there...");
						break;
					}
					case 4:
					{
						this.Say("The pattern must lie between...");
						break;
					}
					case 5:
					{
						this.Say("*sighs and shuffles through star charts*");
						break;
					}
					case 6:
					{
						this.Say("And they said I'd never have a practical application for relativity. Ha!");
						break;
					}
					case 7:
					{
						this.Say("*carefully measures distance on a star chart*");
						break;
					}
					case 8:
					{
						this.Say("*mumbles about astronomical units*");
						break;
					}
					case 9:
					{
						this.Say("...and gravitational lensing will help to look beyond...");
						break;
					}
				}
			}
			base.OnThink();
		}
		
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
			
            writer.Write((int)0); // version
        }
		
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
			
            int version = reader.ReadInt();
        }
    }
}
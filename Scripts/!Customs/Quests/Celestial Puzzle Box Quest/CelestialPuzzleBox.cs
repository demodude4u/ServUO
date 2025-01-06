using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
    public class CelestialPuzzleBox : LockableContainer
    {
        private bool m_Locked;

        [Constructable]
        public CelestialPuzzleBox(): base(0x9AA)
        {
			Name = "Celestial Puzzle Box";
			Hue = 1163;
            Locked = true;
            LockLevel = 10;
            RequiredSkill = 10;
            Weight = 4.0;

        }
		
		public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            list.Add("Lockpick and Remove Trap Trainer, Double click to set for your skill level.");
        }

        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);
            this.LabelTo(from, "Lockpick and Remove Trap Trainer, Double click to set for your skill level.");
        }
		
		public override void Open(Mobile from)
		{
			double lockpicking = from.Skills[SkillName.Lockpicking].Value;
			int level = (int)(lockpicking * 0.8);
			double removetrap = from.Skills[SkillName.RemoveTrap].Value;
			int leveltrap = (int)(removetrap * 0.8);
			this.RequiredSkill = level - 4;
            this.LockLevel = level - 14;
            this.MaxLockLevel = level + 35;
			this.Locked = true;

            if (this.LockLevel == 0)
                this.LockLevel = -1;
            else if (this.LockLevel > 120)
                this.LockLevel = 120;

            if (this.RequiredSkill > 120)
                this.RequiredSkill = 120;

            if (this.MaxLockLevel > 120)
                this.MaxLockLevel = 120;
			this.TrapType = TrapType.MagicTrap;
			this.TrapLevel = (int)(removetrap / 9);
			if ( this.TrapLevel > 12 )
				this.TrapLevel = 12;
			this.TrapPower = this.TrapLevel * 9;			
					
			from.SendMessage("This chest has been set for "+lockpicking+" lockpicking skill and "+removetrap+" remove trap skill!");
			switch (Utility.Random( 19 ))
			{
				case 0:
				{
					from.SendMessage("You hear incomprehensible whispering coming from the puzzle box...");
					break;
				}
				case 1:
				{
					from.SendMessage("Which is the true nightmare, the horrific dream that you have in your sleep or the dissatisfied reality that awaits you when you awake?");
					break;
				}
				case 2:
				{
					from.SendMessage("We live on a placid island of ignorance in the midst of black seas of infinity, and it was not meant that we should voyage far.");
					break;
				}
				case 3:
				{
					from.SendMessage("Who knows the end? What has risen may sink, and what has sunk may rise. Loathsomeness waits and dreams in the deep, and decay spreads over the tottering cities of men.");
					break;
				}
				case 4:
				{
					from.SendMessage("Believe nothing you hear, and only one half that you see.");
					break;
				}
				case 5:
				{
					from.SendMessage("All that we see or seem is but a dream within a dream.");
					break;
				}
				case 6:
				{
					from.SendMessage("It is by no means an irrational fancy that, in a future existence, we shall look upon what we think our present existence, as a dream.");
					break;
				}
				case 7:
				{
					from.SendMessage("Your mind is flooded with images of distant stars.");
					break;
				}
				case 8:
				{
					from.SendMessage("The boundaries which divide Life from Death are at best shadowy and vague. Who shall say where the one ends, and where the other begins?");
					break;
				}
				case 9:
				{
					from.SendMessage("The image will haunt your dreams throughout each night; of gazing upon stars, and the stars gazing back.");
					break;
				}
				case 10:
				{
					from.SendMessage("Strange images of constellations flash before your eyes for a brief moment.");
					break;
				}
				case 11:
				{
					from.SendMessage("There are horrors beyond life's edge that we do not suspect, and once in a while man's evil prying calls them just within our range.");
					break;
				}
				case 12:
				{
					from.SendMessage("Imagination, of course, can open any door - turn the key and let terror walk right in..");
					break;
				}
				case 13:
				{
					from.SendMessage("It’s a Dance. And sometimes they turn the lights off in this ballroom. But we’ll dance anyway, you and I. Even in the Dark. Especially in the Dark. May I have the pleasure?");
					break;
				}
				case 14:
				{
					from.SendMessage("Deep into that darkness peering, long I stood there, wondering, fearing, doubting, dreaming dreams no mortal ever dared to dream before.");
					break;
				}
				case 15:
				{
					from.SendMessage("There are some secrets which do not permit themselves to be told.");
					break;
				}
				case 16:
				{
					from.SendMessage("As you reconfigure the gears of the puzzle box, you sense that something inside can see you.");
					break;
				}
				case 17:
				{
					from.SendMessage("To recognize one's own insanity is, of course, the arising of sanity, the beginning of healing and transcendence.");
					break;
				}
				case 18:
				{
					from.SendMessage("Insanity is relative. It depends on who has who locked in what cage.");
					break;
				}
				case 19:
				{
					from.SendMessage("You get the distinct feeling that something inside this puzzle box wants out.");
					break;
				}
			}
		}
        public CelestialPuzzleBox(Serial serial)
            : base(serial)
        {
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
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class CelestialPuzzleBoxQuest : BaseQuest
    {
        public CelestialPuzzleBoxQuest()
        {
            AddObjective(new ObtainObjective(typeof (Sextant), "Sextant", 1, 0x1058));

            AddObjective(new ObtainObjective(typeof (Spyglass), "Spyglass", 1, 0x14f5));
			
			AddObjective(new ObtainObjective(typeof (WoodenBox), "Wooden Box", 1, 0x9aa));
			
			AddObjective(new ObtainObjective(typeof (Gears), "Gears", 25, 0x1053));
			
			AddObjective(new ObtainObjective(typeof (ArcaneGem), "Arcane Gem", 1, 0x1ea7));

            AddReward(new BaseReward(typeof (CelestialPuzzleBox), "Celestial Puzzle Box"));
        }

        /*Celestial Puzzle Box Quest*/

        public override object Title
        {
            get { return "The Void Gazes Back"; }
        }

        public override object Description
        {
            get { return "You there... I know that this is going to sound deranged, but I have a favor to ask. In my study of the stars, I have found what appears to be schematics for a devilishly complex device of some kind. I am fair hand at tinkering, but I cannot interrupt my studies to acquire the components I need. If you can bring them me, you can keep the device... I just want to know what it does!"; }
        }

        public override object Refuse
        {
            get { return "If you change your mind, you know where to find me."; }
        }

        public override object Uncomplete
        {
            get { return "Return to me when you have all of the items I requested! Remember, to register an item as a quest objective item, single-click your character and select 'Toggle Quest Item' from the context menu."; }
        }

        public override object Complete
        {
            get { return "This puzzle box... something about it disturbs me. All it seems to do is re-lock and re-trap itself when I try to open it. I swear though, sometimes it sounds like it's... whispering. Please, take this thing from me."; }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}
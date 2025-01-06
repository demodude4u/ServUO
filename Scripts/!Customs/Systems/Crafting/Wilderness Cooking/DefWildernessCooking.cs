using System;
using Server.Items;

namespace Server.Engines.Craft
{
    public class DefWildernessCooking : CraftSystem
    {
        public override SkillName MainSkill
        {
            get
            {
                return SkillName.Cooking;
            }
        }

        public override string GumpTitleString
        {
            get { return "WILDERNESS COOKING MENU"; }
        }
        /*
        public override int GumpTitleNumber
        {
            get
            {
                return 1044003;
            }// <CENTER>COOKING MENU</CENTER>
        }
        */
        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefWildernessCooking();

                return m_CraftSystem;
            }
        }

        public override CraftECA ECA
        {
            get
            {
                return CraftECA.ChanceMinusSixtyToFourtyFive;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            if (item.ItemType == typeof(GrapesOfWrath) ||
                item.ItemType == typeof(EnchantedApple))
            {
                return .5;
            }

            return 0.0; // 0%
        }

        private DefWildernessCooking()
            : base(1, 1, 1.25)// base( 1, 1, 1.5 )
        {
        }

        public override int CanCraft(Mobile from, ITool tool, Type itemType)
        {
            int num = 0;

            if (tool == null || tool.Deleted || tool.UsesRemaining <= 0)
                return 1044038; // You have worn out your tool!
            else if (!tool.CheckAccessible(from, ref num))
                return num; // The tool must be on your person to use.

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (lostMaterial)
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                else
                    return 1044157; // You failed to create the item, but no materials were lost.
            }
            else
            {
                if (quality == 0)
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                else if (makersMark && quality == 2)
                    return 1044156; // You create an exceptional quality item and affix your maker's mark.
                else if (quality == 2)
                    return 1044155; // You create an exceptional quality item.
                else 
                    return 1044154; // You create the item.
            }
        }

        public override void InitCraftList()
        {
            int index = -1;

            #region Kindling
            index = AddCraft(typeof(Kindling), "kindling", "kindling", 0.0, 0.0, typeof(Stick), "sticks", 1, 1044253);
            //SetUseAllRes(index, true);
            ForceNonExceptional(index);
            #endregion

            #region Barbecue
            index = AddCraft(typeof(CharredCookedBird), "charred barbeque", "charred bird", 0.0, 30.0, typeof(RawBird), 1044470, 1, 1044253);
            SetNeedHeat(index, true);
            //SetUseAllRes(index, true);
            ForceNonExceptional(index);

            index = AddCraft(typeof(CharredChickenLeg), "charred barbeque", "charred chicken leg", 0.0, 30.0, typeof(RawChickenLeg), 1044473, 1, 1044253);
            SetNeedHeat(index, true);
            //SetUseAllRes(index, true);
            ForceNonExceptional(index);

            index = AddCraft(typeof(CharredFishSteak), "charred barbeque", "charred fish steak", 0.0, 30.0, typeof(RawFishSteak), 1044476, 1, 1044253);
            SetNeedHeat(index, true);
            //SetUseAllRes(index, true);
            ForceNonExceptional(index);

            index = AddCraft(typeof(CharredLambLeg), "charred barbeque", "charred leg of lamb", 0.0, 30.0, typeof(RawLambLeg), 1044478, 1, 1044253);
            SetNeedHeat(index, true);
            //SetUseAllRes(index, true);
            ForceNonExceptional(index);

            index = AddCraft(typeof(CharredRibs), "charred barbeque", "charred ribs", 0.0, 30.0, typeof(RawRibs), 1044485, 1, 1044253);
            SetNeedHeat(index, true);
            //SetUseAllRes(index, true);
            ForceNonExceptional(index);
            #endregion
        }
    }
}

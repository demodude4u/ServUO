using System;
using Server.Items;
using Server.Multis;

namespace Server.Engines.Craft
{
    public class DefShipCraft : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Carpentry; }
        }
        public override string GumpTitleString
        {
            get { return "Ship Craft"; }
        }
        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefShipCraft();

                return m_CraftSystem;
            }
        }

        public override CraftECA ECA { get { return CraftECA.ChanceMinusSixtyToFourtyFive; } }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0; // 0%
        }

        private DefShipCraft() : base(1, 1, 1.25)// base( 1, 1, 1.5 )
        {
        }

        public override int CanCraft(Mobile from, ITool tool, Type itemType)
        {
            if (tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
          
            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
            from.PlaySound(0x242);
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



            #region Galleon
            index = AddCraft(typeof(BritannianShipDeed), "Galleon", "BritannianShipDeed", 100, 100, typeof(GalleonTemplate), "GalleonTemplate", 1, "You dont have a galleon template");
            AddRes(index, typeof(GalleonAnchor), "Galleon Anchor", 1, "Missing galleon anchor");
            AddRes(index, typeof(GalleonPlank), "Galleon Plank", 1, "Missing galleon plank");
            index = AddCraft(typeof(GargishGalleonDeed), "Galleon", "GargishGalleonDeed", 100, 100, typeof(GalleonTemplate), "GalleonTemplate", 1, "You dont have a galleon template");
            AddRes(index, typeof(GalleonAnchor), "Galleon Anchor", 1, "Missing galleon anchor");
            AddRes(index, typeof(GalleonPlank), "Galleon Plank", 1, "Missing galleon plank");
            index = AddCraft(typeof(OrcishGalleonDeed), "Galleon", "OrcishGalleonDeed", 100, 100, typeof(GalleonTemplate), "GalleonTemplate", 1, "You dont have a galleon template");
            AddRes(index, typeof(GalleonAnchor), "Galleon Anchor", 1, "Missing galleon anchor");
            AddRes(index, typeof(GalleonPlank), "Galleon Plank", 1, "Missing galleon plank");
            index = AddCraft(typeof(TokunoGalleonDeed), "Galleon", "TokunoGalleonDeed", 100, 100, typeof(GalleonTemplate), "GalleonTemplate", 1, "You dont have a galleon template");
            AddRes(index, typeof(GalleonAnchor), "Galleon Anchor", 1, "Missing galleon anchor");
            AddRes(index, typeof(GalleonPlank), "Galleon Plank", 1, "Missing galleon plank");
            #endregion

            #region Boat
            index = AddCraft(typeof(SmallBoatDeed), "Boat", "SmallBoatDeed", 100, 100, typeof(BoatTemplate), "BoatTemplate", 1, "You dont have a boat template");
            AddRes(index, typeof(BoatAnchor), "Boat Anchor", 1, "Missing boat anchor");
            AddRes(index, typeof(BoatPlank), "Boat Plank", 1, "Missing boat plank");
            index = AddCraft(typeof(SmallDragonBoatDeed), "Boat", "SmallDragonBoatDeed", 100, 100, typeof(BoatTemplate), "BoatTemplate", 1, "You dont have a boat template");
            AddRes(index, typeof(BoatAnchor), "Boat Anchor", 1, "Missing boat anchor");
            AddRes(index, typeof(BoatPlank), "Boat Plank", 1, "Missing boat plank");
            index = AddCraft(typeof(MediumBoatDeed), "Boat", "MediumBoatDeed", 100, 100, typeof(BoatTemplate), "BoatTemplate", 2, "You dont have a boat template");
            AddRes(index, typeof(BoatAnchor), "Boat Anchor", 1, "Missing boat anchor");
            AddRes(index, typeof(BoatPlank), "Boat Plank", 1, "Missing boat plank");
            index = AddCraft(typeof(MediumDragonBoatDeed), "Boat", "MediumDragonBoatDeed", 100, 100, typeof(BoatTemplate), "BoatTemplate", 2, "You dont have a boat template");
            AddRes(index, typeof(BoatAnchor), "Boat Anchor", 1, "Missing boat anchor");
            AddRes(index, typeof(BoatPlank), "Boat Plank", 1, "Missing boat plank");
            index = AddCraft(typeof(LargeBoatDeed), "Boat", "LargeBoatDeed", 100, 100, typeof(BoatTemplate), "BoatTemplate", 3, "You dont have a boat template");
            AddRes(index, typeof(BoatAnchor), "Boat Anchor", 1, "Missing boat anchor");
            AddRes(index, typeof(BoatPlank), "Boat Plank", 1, "Missing boat plank");
            index = AddCraft(typeof(LargeDragonBoatDeed), "Boat", "Large Dragon Boat Deed", 100, 100, typeof(BoatTemplate), "BoatTemplate", 3, "You dont have a boat template");
            AddRes(index, typeof(BoatAnchor), "Boat Anchor", 1, "Missing boat anchor");
            AddRes(index, typeof(BoatPlank), "Boat Plank", 1, "Missing boat plank");
            #endregion

            #region Assembly
            index = AddCraft(typeof(BoatTemplate), "Assembly", "Boat Template", 100, 100, typeof(BoatHold), "Boat Hold", 1, "You dont have a boat hold");
            AddRes(index, typeof(BoatMast), "Boat Mast", 1, "Missing boat mast");
            AddRes(index, typeof(BoatSail), "Boat Sail", 1, "Missing boat sail");

            index = AddCraft(typeof(GalleonTemplate), "Assembly", "Galleon Template", 100, 100, typeof(GalleonShipHold), "Galleon Hold", 1, "You dont have a galleon hold");
            AddRes(index, typeof(GalleonMast), "Galleon Mast", 1, "Missing galleon mast");
            AddRes(index, typeof(GalleonSail), "Galleon Sail", 1, "Missing galleon sail");
            #endregion


            #region Boat Comp.
            index = AddCraft(typeof(BoatHold), "Boat Parts", "Boat Hold", 100, 100, typeof(Board), "Board", 10, "You dont have enough wood");
            index = AddCraft(typeof(BoatPlank), "Boat Parts", "Boat Plank", 100, 100, typeof(Board), "Board", 3, "You dont have enough wood");
            index = AddCraft(typeof(BoatMast), "Boat Parts", "Boat Mast", 100, 100, typeof(Board), "Board", 20, "You dont have enough wood");
            index = AddCraft(typeof(BoatSail), "Boat Parts", "Boat Sail", 100, 100, typeof(Cloth), "Cloth", 100, "You dont have enough cloth");
            index = AddCraft(typeof(BoatAnchor), "Boat Parts", "Boat Anchor", 100, 100, typeof(IronIngot), "Ingot", 50, "You dont have enough iron ingots");
            #endregion


            #region Galleon Comp.
            index = AddCraft(typeof(GalleonShipHold), "Galleon Parts", "Boat Hold", 100, 100, typeof(Board), "Board", 10, "You dont have enough wood");
            index = AddCraft(typeof(GalleonPlank), "Galleon Parts", "Boat Plank", 100, 100, typeof(Board), "Board", 3, "You dont have enough wood");
            index = AddCraft(typeof(GalleonMast), "Galleon Parts", "Boat Mast", 100, 100, typeof(Board), "Board", 20, "You dont have enough wood");
            index = AddCraft(typeof(GalleonSail), "Galleon Parts", "Boat Sail", 100, 100, typeof(Cloth), "Cloth", 100, "You dont have enough cloth");
            index = AddCraft(typeof(GalleonAnchor), "Galleon Parts", "Boat Anchor", 100, 100, typeof(IronIngot), "Ingot", 50, "You dont have enough iron ingots");
            #endregion
        }
    }
}

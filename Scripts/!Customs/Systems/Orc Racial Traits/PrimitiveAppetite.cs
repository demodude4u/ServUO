using Server.Mobiles;

namespace Server.Items
{
    /// <summary>Raw animal food that Orcs may eat without cooking.</summary>
    public abstract class PrimitiveAppetiteRawFood : CookableFood
    {
        protected PrimitiveAppetiteRawFood(int itemID) : base(itemID) { }
        protected PrimitiveAppetiteRawFood(Serial serial) : base(serial) { }

        protected virtual int AppetiteFillFactor => 2;

        public override void AddNameProperty(ObjectPropertyList list)
        {
            base.AddNameProperty(list);
            list.Add("Fill Factor: {0}", AppetiteFillFactor);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(GetWorldLocation(), 1))
                TryEat(from);
        }

        public virtual bool TryEat(Mobile from)
        {
            if (!(from is PlayerMobile player) || Deleted || !Movable || !from.CheckAlive() || !CheckItemUse(from))
                return false;

            if (!Food.FillHunger(from, AppetiteFillFactor))
                return false;

            from.PlaySound(Utility.Random(0x3A, 3));
            if (from.Body.IsHuman && !from.Mounted)
                from.Animate(AnimationType.Eat, 0);

            Consume();
            EventSink.InvokeOnConsume(new OnConsumeEventArgs(from, this));
            ApplyRawMeatPoison(player);
            return true;
        }

        private static void ApplyRawMeatPoison(PlayerMobile player)
        {
            if (player.Race == Race.Orc)
                return;

            int minimum = player.Race == Race.Gargoyle
                ? Config.Get("OrcRacialTraits.PrimitiveAppetiteGargoylePoisonMinimum", 0)
                : Config.Get("OrcRacialTraits.PrimitiveAppetiteHumanPoisonMinimum", 50);
            int maximum = player.Race == Race.Gargoyle
                ? Config.Get("OrcRacialTraits.PrimitiveAppetiteGargoylePoisonMaximum", 50)
                : Config.Get("OrcRacialTraits.PrimitiveAppetiteHumanPoisonMaximum", 75);
            if (Utility.Random(100) < Utility.RandomMinMax(minimum, maximum))
                player.ApplyPoison(player, Poison.Regular);
        }
    }
}

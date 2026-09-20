using Server.Mobiles;

namespace Server.Engines.EnvironmentalHazards
{
    public static class SwampHazard
    {
        public static void ApplyExposure(Mobile mobile)
        {
            int chance;
            Poison poison;

            if (mobile.PoisonResistance <= 10) { chance = 45; poison = Poison.Greater; }
            else if (mobile.PoisonResistance <= 25) { chance = 40; poison = Poison.Greater; }
            else if (mobile.PoisonResistance <= 40) { chance = 30; poison = Poison.Regular; }
            else if (mobile.PoisonResistance <= 50) { chance = 15; poison = Poison.Regular; }
            else if (mobile.PoisonResistance <= 60) { chance = 7; poison = Poison.Lesser; }
            else if (mobile.PoisonResistance <= 70) { chance = 3; poison = Poison.Lesser; }
            else { chance = 1; poison = Poison.Lesser; }

            if (Utility.Random(100) < chance)
            {
                mobile.ApplyPoison(mobile, poison);
            }
        }
    }
}

using Server.Items;
using Server.Mobiles;

namespace Server.RacialTraits
{
    public static class SavageStrikes
    {
        public static bool Enabled => Config.Get("OrcRacialTraits.SavageStrikesEnabled", true);
        public static int StrengthPerPercent => Config.Get("OrcRacialTraits.SavageStrikesStrengthPerPercent", 20);
        public static int MaximumChance => Config.Get("OrcRacialTraits.SavageStrikesMaximumChance", 5);

        public static int GetChance(Mobile attacker)
        {
            if (!Enabled || !(attacker is PlayerMobile) || attacker.Race != Race.Orc)
            {
                return 0;
            }

            int strengthPerPercent = StrengthPerPercent;
            int maximumChance = MaximumChance;

            if (strengthPerPercent <= 0 || maximumChance <= 0)
            {
                return 0;
            }

            return System.Math.Min(attacker.Str / strengthPerPercent, maximumChance);
        }

        public static bool TryProc(Mobile attacker, Mobile defender, BaseWeapon weapon, int damageGiven)
        {
            if (attacker == null || defender == null || weapon == null || damageGiven <= 0 || !(weapon is Fists))
            {
                return false;
            }

            // Fists is ServUO's invisible default weapon and uses Wrestling. The true race check
            // deliberately ignores body IDs, transformations, disguises, and Orcish Kin Masks.
            if (!(attacker is PlayerMobile) || attacker.Race != Race.Orc || weapon.Skill != SkillName.Wrestling ||
                attacker.Skills[SkillName.Wrestling] == null)
            {
                return false;
            }

            int chance = GetChance(attacker);
            if (chance <= 0 || Utility.Random(100) >= chance)
            {
                return false;
            }

            return BleedAttack.TryBeginBleed(defender, attacker);
        }
    }
}

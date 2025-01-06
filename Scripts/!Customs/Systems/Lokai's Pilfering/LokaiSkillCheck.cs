using System;
using Server.Misc;
using Server.Mobiles;
using Server.Items;

namespace Server
{
    public enum LokaiSuccessRating
    {
        TooDifficult,
        CriticalFailure,
        HazzardousFailure,
        Failure,
        PartialSuccess,
        Success,
        CompleteSuccess,
        ExceptionalSuccess,
        TooEasy
    }

    public class LokaiSkillCheck
    {
        public static LokaiSuccessRating CheckSkill(Mobile from, Skill skill, double minSkill, double maxSkill)
        {
            double value = skill.Value;

            if (value < minSkill)
                return LokaiSuccessRating.TooDifficult; // Too difficult
            else if (value >= maxSkill)
                return LokaiSuccessRating.TooEasy; // No challenge

            double chance = (value - minSkill) / (maxSkill - minSkill);

            LokaiSuccessRating rating = LokaiSuccessRating.PartialSuccess;

            double random = Utility.RandomDouble();
            bool success = (chance >= random);

            double gc = (double)(from.Skills.Cap - from.Skills.Total) / from.Skills.Cap;
            gc += (skill.Cap - skill.Base) / skill.Cap;
            gc /= 2;

            gc += (1.0 - chance) * (success ? 0.5 : (Core.AOS ? 0.0 : 0.2));
            gc /= 2;

            if (gc < 0.01)
                gc = 0.01;

            if (from is BaseCreature && ((BaseCreature)from).Controlled)
                gc *= 2;

            if (from.Alive && (gc >= Utility.RandomDouble() || skill.Base < 10.0))
                Gain(from, skill);

            if (chance - random <= -0.9)
                rating = LokaiSuccessRating.CriticalFailure;
            else if (chance - random <= -0.6)
                rating = LokaiSuccessRating.HazzardousFailure;
            else if (chance - random <= 0.0)
                rating = LokaiSuccessRating.Failure;
            else if (chance - random <= 0.15)
                rating = LokaiSuccessRating.PartialSuccess;
            else if (chance - random <= 0.45)
                rating = LokaiSuccessRating.Success;
            else if (chance - random <= 0.75)
                rating = LokaiSuccessRating.CompleteSuccess;
            else if (chance - random <= 0.9)
                rating = LokaiSuccessRating.ExceptionalSuccess;

            return rating;
        }

        public static void Gain(Mobile from, Skill skill)
        {
            if (from.Region.IsPartOf(typeof(Regions.Jail)))
                return;

            if (skill.Base < skill.Cap && skill.Lock == SkillLock.Up)
            {
                int oldSkill = skill.BaseFixedPoint;
                int toGain = 1;

                if (skill.Base <= 30.0)
                    toGain = Utility.Random(3) + 1;

                Skills skills = from.Skills;

                if (skills.Total >= skills.Cap)
                {
                    for (int i = 0; i < skills.Length; ++i)
                    {
                        Skill toLower = skills[i];

                        if (toLower != skill && toLower.Lock == SkillLock.Down && toLower.BaseFixedPoint >= toGain)
                        {
                            toLower.BaseFixedPoint -= toGain;
                            break;
                        }
                    }
                }

                if ((skills.Total + toGain) <= skills.Cap)
                {
                    skill.BaseFixedPoint += toGain;
                }
            }

            if (skill.Lock == SkillLock.Up)
            {
                SkillInfo info = skill.Info;

                if (from.StrLock == StatLockType.Up && (info.StrScale / 33.3) > Utility.RandomDouble())
                    SkillCheck.GainStat(from, SkillCheck.Stat.Str);
                else if (from.DexLock == StatLockType.Up && (info.IntScale / 33.3) > Utility.RandomDouble())
                    SkillCheck.GainStat(from, SkillCheck.Stat.Dex);
                else if (from.IntLock == StatLockType.Up && (info.DexScale / 33.3) > Utility.RandomDouble())
                    SkillCheck.GainStat(from, SkillCheck.Stat.Int);
            }
        }
    }
}

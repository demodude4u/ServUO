using System;

using Server.Mobiles;

namespace Server.Misc
{
    public static class OrcRelations
    {
        public static TimeSpan EnemyDuration { get; } = Config.Get("General.OrcEnemyDuration", TimeSpan.FromMinutes(30.0));

        public static PlayerMobile GetPlayer(Mobile mobile)
        {
            if (mobile is PlayerMobile player)
            {
                return player;
            }

            if (mobile is BaseCreature creature)
            {
                return creature.GetMaster() as PlayerMobile;
            }

            return null;
        }

        public static bool IsFriendlyToOrcs(Mobile mobile)
        {
            PlayerMobile player = GetPlayer(mobile);

            return player != null && player.Race == Race.Orc && !player.EnemyOfOrcs;
        }

        public static bool IsEnemyOfOrcs(Mobile mobile)
        {
            PlayerMobile player = GetPlayer(mobile);

            return player != null && player.Race == Race.Orc && player.EnemyOfOrcs;
        }

        public static void RegisterHostileAction(BaseCreature orc, Mobile aggressor)
        {
            if (orc == null || !orc.UsesOrcRacialRelations)
            {
                return;
            }

            PlayerMobile player = GetPlayer(aggressor);

            if (player != null && player.Race == Race.Orc)
            {
                player.MarkEnemyOfOrcs();
            }
        }
    }
}

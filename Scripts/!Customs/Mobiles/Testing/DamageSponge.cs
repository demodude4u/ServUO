using Server.Mobiles;

namespace Server.Customs.Mobiles.Testing
{
    /// <summary>
    /// Stationary test target with ServUO's maximum supported creature hit-point pool.
    /// It never acquires targets and deals no melee damage.
    /// </summary>
    public class DamageSponge : BaseCreature
    {
        [Constructable]
        public DamageSponge()
            : base(AIType.AI_Use_Default, FightMode.None, 10, 1, 0.2, 0.4)
        {
            Name = "a damage sponge";
            Body = 0x190;
            BaseSoundID = 0x1B0;

            SetStr(1);
            SetDex(1);
            SetInt(1);

            // BaseCreature caps HitsMax at 1,000,000.
            SetHits(1000000);
            SetDamage(0, 0);

            SetDamageType(ResistanceType.Physical, 100);

            CantWalk = true;
            Frozen = true;
        }

        public DamageSponge(Serial serial)
            : base(serial)
        {
        }

        public override bool DisallowAllMoves => true;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();

            // Preserve the test creature's invariant after a world load.
            CantWalk = true;
            Frozen = true;
            SetDamage(0, 0);
        }
    }
}

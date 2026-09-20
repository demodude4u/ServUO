using Server.Engines.EnvironmentalHazards;

namespace Server.Items
{
    [Flipable(0x170B, 0x170C)]
    public class SwampBoots : BaseShoes, IEnvironmentalHazardProtection
    {
        public override int InitMinHits => 255;
        public override int InitMaxHits => 255;

        [Constructable]
        public SwampBoots() : base(0x170B)
        {
            Name = "Swamp Boots";
            Weight = 3.0;
        }

        public SwampBoots(Serial serial) : base(serial)
        {
        }

        public bool ProtectsFrom(EnvironmentalHazardType hazard, Mobile mobile)
        {
            return hazard == EnvironmentalHazardType.Swamp && Parent == mobile && HitPoints > 0;
        }

        public void OnProtectionUsed(EnvironmentalHazardType hazard, Mobile mobile)
        {
            if (!ProtectsFrom(hazard, mobile))
            {
                return;
            }

            HitPoints--;
            if (HitPoints <= 0)
            {
                mobile.SendMessage("Your Swamp Boots have decayed.");
                Delete();
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }
    }
}

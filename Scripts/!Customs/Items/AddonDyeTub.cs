using Server.Items;
using Server.Multis;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Custom.ViewHue
{
    public class AddonDyeTub : DyeTub
    {
        public override string DefaultName => "Addon Dye Tub";

        [Constructable]
        public AddonDyeTub() { }
        public AddonDyeTub(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(GetWorldLocation(), 1))
            {
                from.SendLocalizedMessage(TargetMessage);
                from.Target = new InternalTarget(this);
            }
            else
            {
                from.SendLocalizedMessage(500446); // That is too far away.
            }
        }

        private class InternalTarget : Target
        {
            private readonly DyeTub m_Tub;

            public InternalTarget(DyeTub tub)
                : base(1, false, TargetFlags.None)
            {
                m_Tub = tub;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is AddonComponent)
                {
                    AddonComponent item = (AddonComponent)targeted;

                    BaseHouse house = BaseHouse.FindHouseAt(item.Addon);

                    if (house == null || !house.IsCoOwner(from))
                        from.SendMessage("You must be in your house to do that.");

                    // DO THE THING...
                    foreach (AddonComponent component in item.Addon.Components)
                        component.Hue = m_Tub.DyedHue;
                    from.PlaySound(0x23E);
                }
                else
                {
                    from.SendMessage("This is ONLY for Addons.");
                }
            }
        }
    }
}

using Server.Mobiles;
using Server.Multis;
using System;

namespace Server.Items
{
    public class HousingLimitIncreaseDeed : Item
    {
        private int m_AmountIncrease;

        [CommandProperty(AccessLevel.Administrator)]
        public int AmountIncrease { get { return m_AmountIncrease; } set { m_AmountIncrease = value; InvalidateProperties(); } }

        [Constructable]
        public HousingLimitIncreaseDeed() : this(1)
        {
        }

        [Constructable]
        public HousingLimitIncreaseDeed(int amountIncrease) : base(5360)
        {
            LootType = LootType.Blessed;
            Hue = 1161;
            m_AmountIncrease = amountIncrease;
        }

        public override string DefaultName
        {
            get { return string.Format("a Housing Limit Increase Deed: +{0}", m_AmountIncrease); }
        }

        public HousingLimitIncreaseDeed(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this.IsChildOf(from.Backpack))
            {
                if (BaseHouse.GetAccountHouseLimit(from) + m_AmountIncrease > HousingLimit.MAX)
                {
                    from.SendMessage("You may not use this deed, as you are already at the server Housing Limit ({0}).", HousingLimit.MAX);
                    return;
                }
                HousingLimit.Increase(from, m_AmountIncrease);
                from.SendMessage("Your Housing Limit has increased by {0} to {1}!", m_AmountIncrease, BaseHouse.GetAccountHouseLimit(from));
                from.PlaySound(0x1FA);
                Effects.SendLocationEffect(from, from.Map, 14201, 16);
                Delete();
            }
            else
            {
                from.SendMessage(1173, "This deed must be in your backpack to be used.");
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write((int)m_AmountIncrease);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_AmountIncrease = reader.ReadInt();
        }
    }
}


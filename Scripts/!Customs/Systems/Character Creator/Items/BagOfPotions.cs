using System;

namespace Server.Items
{
    public class BagOfPotions : Bag
    {
        [Constructable]
        public BagOfPotions()
            : this(10)
        {
            this.Name = "Bag Of Potions";
            this.Hue = 11;
            this.LootType = LootType.Regular;
        }

        [Constructable]
        public BagOfPotions(int amount)
        {
            LesserCurePotion curepotion = new LesserCurePotion();
            curepotion.Amount = amount;

            LesserHealPotion healpotion = new LesserHealPotion();
            healpotion.Amount = amount;

            NightSightPotion nspotion = new NightSightPotion();
            nspotion.Amount = amount;

                this.DropItem(curepotion);
                this.DropItem(healpotion);
                this.DropItem(nspotion);
        }

        public BagOfPotions(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
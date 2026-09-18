using System;

namespace Server.Items
{
    public class BagOfFood : Bag
    {


        [Constructable]
        public BagOfFood()
        {
            this.Name = "Bag Of Food";
            this.Hue = 11;
            this.LootType = LootType.Regular;


            switch (Utility.Random(7))//FRUIT
            {
                case 0:
                    {
                        DropItem(new Apple(Utility.Random(1,5)));
                        break;
                    }
                case 1:
                    {
                        DropItem(new Pear(Utility.Random(1, 5)));
                        break;
                    }
                case 2:
                    {
                        DropItem(new Carrot(Utility.Random(1, 5)));
                        break;
                    }
                case 3:
                    {
                        DropItem(new Lettuce(Utility.Random(1, 5)));
                        break;
                    }
                case 4:
                    {
                        DropItem(new GreenGourd(Utility.Random(1, 5)));
                        break;
                    }
                case 5:
                    {
                        DropItem(new Peach(Utility.Random(1, 5)));
                        break;
                    }
                case 6:
                    {
                        DropItem(new Grapes(Utility.Random(1, 5)));
                        break;
                    }
            }
            switch (Utility.Random(5))//MEATS
            {
                case 0:
                    {
                        DropItem(new CookedBird(2));
                        break;
                    }
                case 1:
                    {
                        DropItem(new FishSteak(Utility.Random(1, 5)));
                        break;
                    }
                case 2:
                    {
                        DropItem(new Bacon(Utility.Random(1, 5)));
                        break;
                    }
                case 3:
                    {
                        DropItem(new LambLeg(Utility.Random(1, 5)));
                        break;
                    }
                case 4:
                    {
                        DropItem(new ChickenLeg(Utility.Random(1, 5)));
                        break;
                    }
            }
            switch (Utility.Random(5))//BEVERAGE
            {
                case 0:
                    {
                        DropItem(new Pitcher(BeverageType.Water));
                        break;
                    }
                case 1:
                    {
                        DropItem(new BeverageBottle(BeverageType.Ale));
                        break;
                    }
                case 2:
                    {
                        DropItem(new BeverageBottle(BeverageType.Wine));
                        break;
                    }
                case 3:
                    {
                        DropItem(new BeverageBottle(BeverageType.Liquor));
                        break;
                    }
                case 4:
                    {
                        DropItem(new Pitcher(BeverageType.Milk));
                        break;
                    }
            }
            switch (Utility.Random(5))//BREAD
            {
                case 0:
                    {
                        DropItem(new BreadLoaf(Utility.Random(1, 3)));
                        break;
                    }
                case 1:
                    {
                        DropItem(new Muffins(Utility.Random(1, 5)));
                        break;
                    }
                case 2:
                    {
                        DropItem(new FrenchBread(Utility.Random(1, 3)));
                        break;
                    }
                case 3:
                    {
                        DropItem(new Quiche());
                        break;
                    }
                case 4:
                    {
                        DropItem(new Cookies());
                        break;
                    }
            }
        }

        public BagOfFood(Serial serial)
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
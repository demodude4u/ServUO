using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles 
{ 
    public class SBHairStylist : SBInfo 
    { 
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBHairStylist() 
        { 
        }

        public override IShopSellInfo SellInfo
        {
            get
            {
                return m_SellInfo;
            }
        }
        public override List<GenericBuyInfo> BuyInfo
        {
            get
            {
                return m_BuyInfo;
            }
        }

        public class InternalBuyInfo : List<GenericBuyInfo> 
        { 
            public InternalBuyInfo()
            {
                Add(new GenericBuyInfo("1041060", typeof(HairDye), 60, 20, 0xEFF, 0));
                Add(new GenericBuyInfo("Barber Scissors", typeof(BarberScissors), 47, 20, 0xDFC, 0));
                Add(new GenericBuyInfo("Brush", typeof(Brush), 53, 20, 0x1372, 0));
                Add(new GenericBuyInfo("Facial Razor", typeof(FacialRazor), 26, 20, 0x9F6, 0));
                Add(new GenericBuyInfo("Facial Trimmers", typeof(FacialTrimmers), 62, 20, 0xDFC, 0));
                Add(new GenericBuyInfo("Razor", typeof(Razor), 21, 20, 0xEC4, 0));
                Add(new GenericBuyInfo("Beard Growth Elixir", typeof(BeardGrowthElixir), 5000, 20, 0xE26, 0));
                Add(new GenericBuyInfo("Facial Horn Growth Elixir", typeof(FacialHornGrowthElixir), 5000, 20, 0xefc, 0));
                Add(new GenericBuyInfo("Hair Growth Elixir", typeof(HairGrowthElixir), 5000, 20, 0x5748, 0));
                Add(new GenericBuyInfo("Mustashe Growth Elixir", typeof(MustasheGrowthElixir), 5000, 20, 0xe26, 0));
                Add(new GenericBuyInfo("Special Beard Dye", typeof(SpecialBeardDye), 100000, 20, 0xE26, 0)); 
                Add(new GenericBuyInfo("Special Hair Dye", typeof(SpecialHairDye), 100000, 20, 0xE26, 0));

            }
        }

        public class InternalSellInfo : GenericSellInfo 
        { 
            public InternalSellInfo() 
            { 
                Add(typeof(HairDye), 30); 
                Add(typeof(SpecialBeardDye), 250000); 
                Add(typeof(SpecialHairDye), 250000); 
            }
        }
    }
}

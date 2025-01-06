using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles 
{ 
    public class SBFurtrader : SBInfo 
    { 
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBFurtrader() 
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
                Add(new GenericBuyInfo(typeof(Hides), 3, 40, 0x1079, 0, true));
                Add(new GenericBuyInfo(typeof(SpinedHides), 8, 40, 0x1079, 2220, true));
                Add(new GenericBuyInfo(typeof(HornedHides), 12, 40, 0x1079, 2117, true));
            }
        }

        public class InternalSellInfo : GenericSellInfo 
        { 
            public InternalSellInfo() 
            { 
                Add(typeof(Hides), 2);
                Add(typeof(SpinedHides), 4);
                Add(typeof(HornedHides), 7);
            }
        }
    }
}

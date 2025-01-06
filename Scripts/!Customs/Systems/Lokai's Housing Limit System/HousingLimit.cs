using Server.Accounting;
using System;

namespace Server.Multis
{
    public static class HousingLimit
    {
        public static int MAX { get { return 7; } }

        public static void Increase(Mobile from, int amount)
        {
            Account account = from.Account as Account;
            if (account == null) { return; }
            string tag = account.GetTag("HousingIncrease");
            if (string.IsNullOrEmpty(tag)) {
                account.SetTag("HousingIncrease", amount.ToString());
                return;
            }
            int limit = Int32.Parse(tag);
            if (limit >= 0)
                account.SetTag("HousingIncrease", (limit + amount).ToString());
        }

        public static int GetLimitIncrease(Mobile from)
        {
            Account account = from.Account as Account;
            if (account == null) { return 0; }
            string tag = account.GetTag("HousingIncrease");
            if (string.IsNullOrEmpty(tag))
                return 0;
            int limit = Int32.Parse(tag);
            return limit;
        }
    }
}

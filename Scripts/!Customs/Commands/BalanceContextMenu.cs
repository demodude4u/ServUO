using System;
using System.Collections.Generic;
using System.Linq;

using Server.Accounting;
using Server.ContextMenus;
using Server.Items;
using Server.Network;

using Acc = Server.Accounting.Account;

namespace Server.ContextMenus
{
    public class BalanceContextMenu : ContextMenuEntry
    {
        public BalanceContextMenu()
            : base(1154540, 0)
        {
        }

        public override void OnClick()
        {
            Mobile m = Owner.From;
            m.SendMessage("Your bank balance is {0}.",GetBalance(m));
            
            m.SendMessage("Your current fame : {0}", m.Fame);
            m.SendMessage("Your current karma : {0}", m.Karma);
            
            m.SendMessage("Short Term Murders : {0}", m.ShortTermMurders);
            m.SendMessage("Long Term Murders : {0}", m.Kills);
            
            m.SendMessage("Your current stat cap : {0}", m.StatCap);
            m.SendMessage("Your current skill cap : {0}", m.SkillsCap);
            
            //m.SendMessage("Your current hunger : {0}", m.Hunger);
            //m.SendMessage("Your current thirst : {0}", m.Thirst);
        }

        private int GetBalance(Mobile m)
        {
            double balance = 0;

            if (AccountGold.Enabled && m.Account != null)
            {
                int goldStub;
                m.Account.GetGoldBalance(out goldStub, out balance);

                if (balance > Int32.MaxValue)
                {
                    return Int32.MaxValue;
                }
            }

            Container bank = m.FindBankNoCreate();

            if (bank != null)
            {
                var gold = bank.FindItemsByType<Gold>();
                var checks = bank.FindItemsByType<BankCheck>();

                balance += gold.Aggregate(0.0, (c, t) => c + t.Amount);
                balance += checks.Aggregate(0.0, (c, t) => c + t.Worth);
            }

            return (int)Math.Max(0, Math.Min(Int32.MaxValue, balance));
        }
    }
}
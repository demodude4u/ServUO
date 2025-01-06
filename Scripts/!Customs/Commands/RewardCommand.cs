#region References
using System;
using Server;
using Server.Commands;
using Server.Network;
using Server.Gumps;
#endregion

namespace Server.Engines.VeteranRewards
{
	public class RewardCommand
	{
		public static void Initialize()
		{
            CommandSystem.Register("VetRewards", AccessLevel.Player, new CommandEventHandler(VetRewards_OnCommand));
		}

        public static void VetRewards_OnCommand(CommandEventArgs e)
        {
            if (!e.Mobile.Alive)
                return;

            int cur, max, level;

            RewardSystem.ComputeRewardInfo(e.Mobile, out cur, out max, out level);

            if (cur < max)
                e.Mobile.SendGump(new RewardNoticeGump(e.Mobile));
        }
	}
}
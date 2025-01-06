using Server.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Server.Commands
{
    public class OnlineCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("OCount", AccessLevel.Player, Online_OnCommand);
        }

        [Usage("OCount")]
        [Description("Display Online Count")]
        private static void Online_OnCommand(CommandEventArgs e)
        {
            int userCount = NetState.Instances.Count;
            int itemCount = World.Items.Count;
            int mobileCount = World.Mobiles.Count;

            Mobile m = e.Mobile;

            m.SendMessage("Welcome, {0}! There {1} currently {2} user{3} online, with {4} item{5} and {6} mobile{7} in the world.",
                e.Mobile.Name,
                userCount == 1 ? "is" : "are",
                userCount, userCount == 1 ? "" : "s",
                itemCount, itemCount == 1 ? "" : "s",
                mobileCount, mobileCount == 1 ? "" : "s");
        }
    }
}

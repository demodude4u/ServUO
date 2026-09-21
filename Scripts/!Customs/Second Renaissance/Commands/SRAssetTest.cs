using Server.Commands;
using Server.Gumps;
using Server.Network;

namespace Server.SecondRenaissance
{
    public static class SRAssetTest
    {
        public const int TestGumpId = 2240;

        public static void Initialize()
        {
            CommandSystem.Register("SRAssetTest", AccessLevel.Administrator, OnCommand);
        }

        [Usage("SRAssetTest")]
        [Description("Displays Official and Second Renaissance gump 2240 side by side.")]
        private static void OnCommand(CommandEventArgs e)
        {
            e.Mobile.CloseGump(typeof(SRAssetTestGump));
            e.Mobile.SendGump(new SRAssetTestGump());
        }

        private sealed class SRAssetTestGump : Gump
        {
            public SRAssetTestGump() : base(100, 100)
            {
                Closable = true;
                Disposable = true;
                Dragable = true;
                Resizable = false;

                AddBackground(0, 0, 300, 130, 9270);
                AddLabel(20, 15, 1152, "Classic / Official");
                AddLabel(160, 15, 1152, "Second Renaissance");

                AddImage(55, 45, TestGumpId);
                AddSecondRenaissanceImage(195, 45, TestGumpId);

                AddButton(135, 95, 4017, 4018, 0, GumpButtonType.Reply, 0);
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
            }
        }
    }
}

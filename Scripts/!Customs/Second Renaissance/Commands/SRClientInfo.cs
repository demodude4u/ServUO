using Server.Commands;
using Server.Network;

namespace Server.SecondRenaissance
{
    public static class SRClientInfo
    {
        public static void Initialize()
        {
            CommandSystem.Register("SRClientInfo", AccessLevel.Administrator, OnCommand);
        }

        [Usage("SRClientInfo")]
        [Description("Displays Second Renaissance capability negotiation for your current client connection.")]
        private static void OnCommand(CommandEventArgs e)
        {
            NetState state = e.Mobile?.NetState;

            if (state == null)
            {
                e.Mobile?.SendMessage("Second Renaissance client info is unavailable: no active client connection.");
                return;
            }

            SecondRenaissanceClientCapabilities capabilities =
                SecondRenaissanceCapabilityNegotiation.GetCapabilities(state);

            if (!capabilities.IsNegotiated)
            {
                e.Mobile.SendMessage(
                    "Second Renaissance capabilities: not negotiated (ordinary/unnegotiated client)."
                );
                return;
            }

            bool supportsExplicitGumpLayout = capabilities.Supports(
                SecondRenaissanceCapability.ExplicitGumpLayout
            );

            e.Mobile.SendMessage("Second Renaissance capabilities: negotiated.");
            e.Mobile.SendMessage("SR protocol version: {0}", capabilities.ProtocolVersion);
            e.Mobile.SendMessage(
                "Raw advertised capability flags: 0x{0:X16}",
                capabilities.AdvertisedCapabilities
            );
            e.Mobile.SendMessage(
                "Recognized/supported capability flags: {0} (0x{1:X16})",
                capabilities.SupportedCapabilities,
                (ulong)capabilities.SupportedCapabilities
            );
            e.Mobile.SendMessage(
                "ExplicitGumpLayout supported: {0}",
                supportsExplicitGumpLayout ? "Yes" : "No"
            );
        }
    }
}

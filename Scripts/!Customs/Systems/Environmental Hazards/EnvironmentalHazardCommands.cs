using Server.Commands;

using System;

namespace Server.Engines.EnvironmentalHazards
{
    public static class EnvironmentalHazardCommands
    {
        public static void Initialize()
        {
            CommandSystem.Register("HazardInfo", AccessLevel.GameMaster, OnCommand);
        }

        [Usage("HazardInfo")]
        [Description("Reports environmental-hazard detection and exposure status at your location.")]
        private static void OnCommand(CommandEventArgs e)
        {
            EnvironmentalHazardStatus status = EnvironmentalHazardSystem.GetStatus(e.Mobile);
            EnvironmentalHazardDetection detection = status.Detection;

            if (!detection.Detected)
            {
                e.Mobile.SendMessage("Environmental hazard: none detected at {0} ({1}).", e.Mobile.Location, e.Mobile.Map);
                return;
            }

            string identifier = detection.Source == EnvironmentalHazardSource.WorldItem && detection.WorldItem != null
                ? String.Format("item 0x{0:X} serial {1}", detection.SourceID, detection.WorldItem.Serial)
                : String.Format("tile 0x{0:X} ({1})", detection.SourceID, detection.SourceID);

            e.Mobile.SendMessage("Environmental hazard: {0}; source: {1}, {2}.", detection.Hazard, detection.Source, identifier);
            e.Mobile.SendMessage("Enabled: {0}; naturally immune: {1}; equipment protection: {2}.",
                status.Enabled, status.NaturallyImmune, status.Protection == null ? "none" : status.Protection.GetType().Name);
            e.Mobile.SendMessage("Exposure eligible: {0}; cooldown remaining: {1:F1} seconds.",
                status.CooldownEligible, Math.Max(0.0, status.CooldownRemaining.TotalSeconds));
        }
    }
}

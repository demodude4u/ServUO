// SPDX-License-Identifier: BSD-2-Clause
using Server.Commands;
using Server.Network;
using System;

namespace Server.SecondRenaissance
{
    public static class SRMetadata
    {
        public static void Initialize() { CommandSystem.Register("SRMetadata", AccessLevel.Administrator, OnCommand); }
        [Usage("SRMetadata Local | Static <uint-id> | Client [name]")]
        [Description("Displays read-only Second Renaissance metadata and per-client compatibility state.")]
        private static void OnCommand(CommandEventArgs e)
        {
            SecondRenaissanceMetadataProvider provider = SecondRenaissanceMetadataRuntime.Current;
            if (e.Arguments.Length == 0 || string.Equals(e.Arguments[0], "Local", StringComparison.OrdinalIgnoreCase))
            {
                e.Mobile.SendMessage("SR metadata local: {0}; source {1}; revision {2}; records {3}; SHA-256 {4}. {5}", provider.State,
                    provider.SourcePath, provider.DatasetRevision, provider.RecordCount, provider.DatasetHash ?? "(none)", provider.Diagnostic); return;
            }
            if (e.Arguments.Length == 2 && string.Equals(e.Arguments[0], "Static", StringComparison.OrdinalIgnoreCase))
            {
                uint id; if (!uint.TryParse(e.Arguments[1], out id)) { Usage(e); return; }
                SecondRenaissanceStaticMetadataView record; SecondRenaissanceAssetReference reference = SecondRenaissanceAssetReference.SecondRenaissanceStatic(id);
                if (!provider.TryGet(reference, out record)) { e.Mobile.SendMessage("SR metadata: no record for {0}.", reference); return; }
                e.Mobile.SendMessage("{0}: readiness {1}; flags {2}; height {3}; collision {4}; weight {5}; label {6}.", reference,
                    record.Readiness, record.Flags, record.Height, record.CollisionHeight, record.BaseWeightHundredths, record.EditorLabel ?? "(none)"); return;
            }
            if (string.Equals(e.Arguments[0], "Client", StringComparison.OrdinalIgnoreCase) && e.Arguments.Length <= 2)
            {
                Mobile mobile = e.Arguments.Length == 1 ? e.Mobile : FindConnected(e.Arguments[1]);
                if (mobile == null || mobile.NetState == null) { e.Mobile.SendMessage("Connected client not found."); return; }
                NetState state = mobile.NetState; SecondRenaissanceClientCapabilities capabilities = SecondRenaissanceCapabilityNegotiation.GetCapabilities(state);
                SecondRenaissanceMetadataSession session = SecondRenaissanceMetadataNegotiation.GetSession(state);
                e.Mobile.SendMessage("SR metadata client {0}: capability {1}; compatibility {2}.", mobile.Name,
                    capabilities.Supports(SecondRenaissanceCapability.MetadataNegotiation) ? "Supported" : "Unsupported", session.Compatibility);
                if (session.HasRemote)
                {
                    SecondRenaissanceRemoteMetadata remote = session.Remote;
                    e.Mobile.SendMessage("Client metadata: {0}; format {1}.{2}; schema {3}.{4}; revision {5}; records {6}; SHA-256 {7}.",
                        remote.State, remote.FormatMajor, remote.FormatMinor, remote.SchemaMajor, remote.SchemaMinor,
                        remote.DatasetRevision, remote.RecordCount, Hex(remote.DatasetHash));
                }
                else e.Mobile.SendMessage("Client metadata identity has not been received.");
                return;
            }
            Usage(e);
        }
        private static Mobile FindConnected(string name)
        {
            foreach (Mobile mobile in World.Mobiles.Values)
                if (mobile.NetState != null && string.Equals(mobile.Name, name, StringComparison.OrdinalIgnoreCase)) return mobile;
            return null;
        }
        private static string Hex(byte[] bytes)
        {
            if (bytes == null) return "(none)"; char[] c = new char[bytes.Length * 2]; const string a = "0123456789ABCDEF";
            for (int i=0;i<bytes.Length;i++){c[i*2]=a[bytes[i]>>4];c[i*2+1]=a[bytes[i]&15];} return new string(c);
        }
        private static void Usage(CommandEventArgs e) { e.Mobile.SendMessage("Usage: [SRMetadata Local | Static <uint-id> | Client [name]"); }
    }
}

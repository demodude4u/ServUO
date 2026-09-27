// SPDX-License-Identifier: BSD-2-Clause

namespace Server.SecondRenaissance
{
    public static class SecondRenaissanceMetadataConsumption
    {
        public static bool TryGetWorldReady(SecondRenaissanceAssetReference reference,
            out SecondRenaissanceStaticMetadataView metadata)
        {
            return TryGetWorldReady(SecondRenaissanceMetadataRuntime.Current, reference, out metadata);
        }

        public static bool TryGetWorldReady(SecondRenaissanceMetadataProvider provider,
            SecondRenaissanceAssetReference reference, out SecondRenaissanceStaticMetadataView metadata)
        {
            metadata = null;
            if (provider == null || !provider.IsAvailable
                || reference.Namespace != SecondRenaissanceAssetNamespace.SecondRenaissance
                || reference.Type != SecondRenaissanceAssetType.Static
                || !provider.TryGet(reference, out SecondRenaissanceStaticMetadataView candidate)
                || candidate.Readiness != SecondRenaissanceMetadataReadiness.WorldReady)
                return false;
            metadata = candidate;
            return true;
        }
    }
}

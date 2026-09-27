// SPDX-License-Identifier: BSD-2-Clause

using System;
using System.IO;

namespace Server.SecondRenaissance
{
    public static class SecondRenaissanceMetadataRuntime
    {
        private static readonly Lazy<SecondRenaissanceMetadataProvider> CurrentProvider =
            new Lazy<SecondRenaissanceMetadataProvider>(Create, true);

        public static SecondRenaissanceMetadataProvider Current { get { return CurrentProvider.Value; } }

        public static void Initialize()
        {
            SecondRenaissanceMetadataProvider provider = Current;
            Console.WriteLine("Second Renaissance metadata: {0}; source {1}; revision {2}; records {3}; SHA-256 {4}. {5}",
                provider.State, provider.SourcePath, provider.DatasetRevision, provider.RecordCount,
                provider.DatasetHash ?? "(none)", provider.Diagnostic);
        }

        private static SecondRenaissanceMetadataProvider Create()
        {
            return new SecondRenaissanceMetadataProvider(Path.Combine(Core.BaseDirectory, "Data", "SecondRenaissance", "Metadata", "SecondRenaissanceMetadata.bin"));
        }
    }
}

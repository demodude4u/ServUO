// SPDX-License-Identifier: BSD-2-Clause

using System;
using System.Collections.Generic;
using System.IO;

namespace Server.SecondRenaissance
{
    public enum SecondRenaissanceMetadataProviderState { Unavailable, Loaded, Invalid }

    public sealed class SecondRenaissanceStaticMetadataView
    {
        internal SecondRenaissanceStaticMetadataView(SecondRenaissanceStaticMetadata source)
        {
            Reference = source.Reference; RecordVersion = source.RecordVersion; Readiness = source.Readiness;
            LocalizationKey = source.LocalizationKey; PluralLocalizationKey = source.PluralLocalizationKey;
            EditorLabel = source.EditorLabel; ArticlePolicy = source.ArticlePolicy; Height = source.Height;
            CollisionHeight = source.CollisionHeight; BridgeMode = source.BridgeMode; Flags = source.Flags;
            BaseWeightHundredths = source.BaseWeightHundredths; EquipLayer = source.EquipLayer;
            HousingPolicy = source.HousingPolicy; AnimationProfile = source.AnimationProfile; LightProfile = source.LightProfile;
        }
        public SecondRenaissanceAssetReference Reference { get; private set; }
        public ushort RecordVersion { get; private set; }
        public SecondRenaissanceMetadataReadiness Readiness { get; private set; }
        public string LocalizationKey { get; private set; }
        public string PluralLocalizationKey { get; private set; }
        public string EditorLabel { get; private set; }
        public SecondRenaissanceArticlePolicy ArticlePolicy { get; private set; }
        public ushort Height { get; private set; }
        public ushort CollisionHeight { get; private set; }
        public SecondRenaissanceBridgeMode BridgeMode { get; private set; }
        public SecondRenaissanceStaticMetadataFlags Flags { get; private set; }
        public uint BaseWeightHundredths { get; private set; }
        public ushort EquipLayer { get; private set; }
        public SecondRenaissanceHousingPolicy HousingPolicy { get; private set; }
        public string AnimationProfile { get; private set; }
        public string LightProfile { get; private set; }
    }

    public sealed class SecondRenaissanceMetadataProvider
    {
        private readonly Dictionary<SecondRenaissanceAssetReference, SecondRenaissanceStaticMetadataView> _records =
            new Dictionary<SecondRenaissanceAssetReference, SecondRenaissanceStaticMetadataView>();

        public SecondRenaissanceMetadataProvider(string sourcePath)
        {
            SourcePath = Path.GetFullPath(sourcePath);
            if (!File.Exists(SourcePath)) { State = SecondRenaissanceMetadataProviderState.Unavailable; Diagnostic = "Metadata file is unavailable."; return; }
            try
            {
                SecondRenaissanceMetadataBundle bundle = SecondRenaissanceMetadataBundle.Read(File.ReadAllBytes(SourcePath));
                foreach (SecondRenaissanceStaticMetadata record in bundle.Records)
                    _records.Add(record.Reference, new SecondRenaissanceStaticMetadataView(record));
                DatasetRevision = bundle.DatasetRevision;
                DatasetHash = ToHex(bundle.DatasetHash);
                RecordCount = _records.Count;
                State = SecondRenaissanceMetadataProviderState.Loaded;
                Diagnostic = "Second Renaissance metadata loaded.";
            }
            catch (Exception e)
            {
                if (!(e is IOException) && !(e is UnauthorizedAccessException) && !(e is InvalidDataException) && !(e is OverflowException)) throw;
                State = SecondRenaissanceMetadataProviderState.Invalid; Diagnostic = e.Message;
            }
        }

        public string SourcePath { get; private set; }
        public SecondRenaissanceMetadataProviderState State { get; private set; }
        public bool IsAvailable { get { return State == SecondRenaissanceMetadataProviderState.Loaded; } }
        public uint DatasetRevision { get; private set; }
        public int RecordCount { get; private set; }
        public string DatasetHash { get; private set; }
        public string Diagnostic { get; private set; }

        public bool TryGet(SecondRenaissanceAssetReference reference, out SecondRenaissanceStaticMetadataView metadata)
        {
            metadata = null;
            return reference.Namespace == SecondRenaissanceAssetNamespace.SecondRenaissance
                && reference.Type == SecondRenaissanceAssetType.Static
                && _records.TryGetValue(reference, out metadata);
        }

        private static string ToHex(byte[] bytes)
        {
            char[] chars = new char[bytes.Length * 2]; const string alphabet = "0123456789ABCDEF";
            for (int i = 0; i < bytes.Length; i++) { chars[i * 2] = alphabet[bytes[i] >> 4]; chars[i * 2 + 1] = alphabet[bytes[i] & 15]; }
            return new string(chars);
        }
    }
}

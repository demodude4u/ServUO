using Server.Items;
using Server.Mobiles;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Server.Engines.EnvironmentalHazards
{
    public enum EnvironmentalHazardType
    {
        None,
        Swamp,
        Lava,
        Arctic,
        Desert
    }

    public enum EnvironmentalHazardSource
    {
        None,
        Land,
        Static,
        WorldItem
    }

    public interface IEnvironmentalHazardProtection
    {
        bool ProtectsFrom(EnvironmentalHazardType hazard, Mobile mobile);
        void OnProtectionUsed(EnvironmentalHazardType hazard, Mobile mobile);
    }

    public interface IEnvironmentalHazardImmunityProvider
    {
        bool IsImmune(Mobile mobile, EnvironmentalHazardType hazard);
    }

    public sealed class EnvironmentalHazardDetection
    {
        public static readonly EnvironmentalHazardDetection None = new EnvironmentalHazardDetection();

        public EnvironmentalHazardType Hazard { get; internal set; }
        public EnvironmentalHazardSource Source { get; internal set; }
        public int SourceID { get; internal set; }
        public Item WorldItem { get; internal set; }
        public bool Detected => Hazard != EnvironmentalHazardType.None;
    }

    public sealed class EnvironmentalHazardStatus
    {
        public EnvironmentalHazardDetection Detection { get; internal set; }
        public bool Enabled { get; internal set; }
        public bool NaturallyImmune { get; internal set; }
        public IEnvironmentalHazardProtection Protection { get; internal set; }
        public bool CooldownEligible { get; internal set; }
        public TimeSpan CooldownRemaining { get; internal set; }
    }

    internal sealed class EnvironmentalHazardDefinition
    {
        public EnvironmentalHazardType Type { get; }
        public HashSet<int> LandTiles { get; } = new HashSet<int>();
        public HashSet<int> StaticTiles { get; } = new HashSet<int>();
        public HashSet<int> WorldItemIDs { get; } = new HashSet<int>();

        public EnvironmentalHazardDefinition(EnvironmentalHazardType type)
        {
            Type = type;
        }

        public EnvironmentalHazardDefinition AddLandRange(int minimum, int maximum)
        {
            AddRange(LandTiles, minimum, maximum);
            return this;
        }

        public EnvironmentalHazardDefinition AddStaticRange(int minimum, int maximum)
        {
            AddRange(StaticTiles, minimum, maximum);
            return this;
        }

        public EnvironmentalHazardDefinition AddWorldItemRange(int minimum, int maximum)
        {
            AddRange(WorldItemIDs, minimum, maximum);
            return this;
        }

        private static void AddRange(HashSet<int> values, int minimum, int maximum)
        {
            if (minimum < 0 || maximum < minimum)
            {
                throw new ArgumentOutOfRangeException(nameof(minimum), "Hazard tile ranges must be non-negative and ordered.");
            }

            for (int id = minimum; id <= maximum; id++)
            {
                values.Add(id);
            }
        }
    }

    public static class EnvironmentalHazardSystem
    {
        private sealed class ExposureState
        {
            public readonly DateTime[] NextEligible = new DateTime[5];
        }

        private sealed class OrcSwampImmunity : IEnvironmentalHazardImmunityProvider
        {
            public bool IsImmune(Mobile mobile, EnvironmentalHazardType hazard)
            {
                // Actual playable race is authoritative; appearance and body modifications are intentionally ignored.
                return hazard == EnvironmentalHazardType.Swamp && mobile is PlayerMobile && mobile.Race == Race.Orc;
            }
        }

        private static readonly EnvironmentalHazardDefinition[] _Definitions = CreateDefinitions();
        private static readonly List<IEnvironmentalHazardImmunityProvider> _ImmunityProviders =
            new List<IEnvironmentalHazardImmunityProvider> { new OrcSwampImmunity() };
        private static readonly ConditionalWeakTable<Mobile, ExposureState> _Exposure =
            new ConditionalWeakTable<Mobile, ExposureState>();

        public static bool Enabled => Config.Get("EnvironmentalHazards.Enabled", true);
        public static TimeSpan SwampExposureCooldown => Config.Get("EnvironmentalHazards.SwampExposureCooldown", TimeSpan.FromSeconds(5));

        public static void Initialize()
        {
            ValidateDefinitions();
        }

        public static void RegisterImmunityProvider(IEnvironmentalHazardImmunityProvider provider)
        {
            if (provider != null && !_ImmunityProviders.Contains(provider))
            {
                _ImmunityProviders.Add(provider);
            }
        }

        public static void Check(Mobile mobile)
        {
            if (!CanApplyEffects(mobile))
            {
                return;
            }

            EnvironmentalHazardStatus status = GetStatus(mobile);

            if (!status.Detection.Detected || !status.Enabled || status.NaturallyImmune || !status.CooldownEligible)
            {
                return;
            }

            SetCooldown(mobile, status.Detection.Hazard);

            if (status.Protection != null)
            {
                status.Protection.OnProtectionUsed(status.Detection.Hazard, mobile);
                return;
            }

            switch (status.Detection.Hazard)
            {
                case EnvironmentalHazardType.Swamp:
                    SwampHazard.ApplyExposure(mobile);
                    break;
            }
        }

        public static EnvironmentalHazardStatus GetStatus(Mobile mobile)
        {
            EnvironmentalHazardDetection detection = Detect(mobile);
            var status = new EnvironmentalHazardStatus
            {
                Detection = detection,
                Enabled = detection.Detected && IsEnabled(detection.Hazard),
                CooldownEligible = true,
                CooldownRemaining = TimeSpan.Zero
            };

            if (!detection.Detected || mobile == null)
            {
                return status;
            }

            status.NaturallyImmune = IsNaturallyImmune(mobile, detection.Hazard);
            status.Protection = FindProtection(mobile, detection.Hazard);

            DateTime next = GetNextEligible(mobile, detection.Hazard);
            if (next > DateTime.UtcNow)
            {
                status.CooldownEligible = false;
                status.CooldownRemaining = next - DateTime.UtcNow;
            }

            return status;
        }

        public static EnvironmentalHazardDetection Detect(Mobile mobile)
        {
            if (mobile == null || mobile.Deleted || !mobile.Alive || mobile.Map == null || mobile.Map == Map.Internal)
            {
                return EnvironmentalHazardDetection.None;
            }

            Map map = mobile.Map;
            int x = mobile.X;
            int y = mobile.Y;
            int z = mobile.Z;

            // Read the sector list directly to avoid allocating a pooled range enumerator on every step.
            List<Item> items = map.GetSector(x, y).Items;
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                if (!item.Deleted && item.Visible && item.X == x && item.Y == y && item.Z == z)
                {
                    EnvironmentalHazardDefinition definition = FindByWorldItem(item.ItemID);
                    if (definition != null)
                    {
                        return CreateDetection(definition.Type, EnvironmentalHazardSource.WorldItem, item.ItemID, item);
                    }
                }
            }

            StaticTile[] staticTiles = map.Tiles.GetStaticTiles(x, y, false);
            for (int i = 0; i < staticTiles.Length; i++)
            {
                StaticTile tile = staticTiles[i];
                if (tile.Z == z)
                {
                    EnvironmentalHazardDefinition definition = FindByStatic(tile.ID);
                    if (definition != null)
                    {
                        return CreateDetection(definition.Type, EnvironmentalHazardSource.Static, tile.ID, null);
                    }
                }
            }

            LandTile land = map.Tiles.GetLandTile(x, y);
            if (land.Z == z)
            {
                EnvironmentalHazardDefinition definition = FindByLand(land.ID);
                if (definition != null)
                {
                    return CreateDetection(definition.Type, EnvironmentalHazardSource.Land, land.ID, null);
                }
            }

            return EnvironmentalHazardDetection.None;
        }

        public static bool IsEnabled(EnvironmentalHazardType hazard)
        {
            if (!Enabled)
            {
                return false;
            }

            switch (hazard)
            {
                case EnvironmentalHazardType.Swamp: return Config.Get("EnvironmentalHazards.SwampEnabled", true);
                case EnvironmentalHazardType.Lava: return Config.Get("EnvironmentalHazards.LavaEnabled", false);
                case EnvironmentalHazardType.Arctic: return Config.Get("EnvironmentalHazards.ArcticEnabled", false);
                case EnvironmentalHazardType.Desert: return Config.Get("EnvironmentalHazards.DesertEnabled", false);
                default: return false;
            }
        }

        public static bool IsNaturallyImmune(Mobile mobile, EnvironmentalHazardType hazard)
        {
            for (int i = 0; i < _ImmunityProviders.Count; i++)
            {
                if (_ImmunityProviders[i].IsImmune(mobile, hazard))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CanApplyEffects(Mobile mobile)
        {
            return mobile != null && !mobile.Deleted && mobile.Alive && mobile.Map != null && mobile.Map != Map.Internal
                && !mobile.IsStaff() && !mobile.Flying && (!(mobile is BaseCreature creature) || !creature.IsInvulnerable);
        }

        private static IEnvironmentalHazardProtection FindProtection(Mobile mobile, EnvironmentalHazardType hazard)
        {
            List<Item> equipment = mobile.Items;
            for (int i = 0; i < equipment.Count; i++)
            {
                if (equipment[i] is IEnvironmentalHazardProtection protection && protection.ProtectsFrom(hazard, mobile))
                {
                    return protection;
                }
            }

            return null;
        }

        private static DateTime GetNextEligible(Mobile mobile, EnvironmentalHazardType hazard)
        {
            return _Exposure.GetOrCreateValue(mobile).NextEligible[(int)hazard];
        }

        private static void SetCooldown(Mobile mobile, EnvironmentalHazardType hazard)
        {
            TimeSpan cooldown = hazard == EnvironmentalHazardType.Swamp ? SwampExposureCooldown : TimeSpan.Zero;
            if (cooldown < TimeSpan.Zero)
            {
                cooldown = TimeSpan.Zero;
            }

            _Exposure.GetOrCreateValue(mobile).NextEligible[(int)hazard] = DateTime.UtcNow + cooldown;
        }

        private static EnvironmentalHazardDetection CreateDetection(EnvironmentalHazardType hazard, EnvironmentalHazardSource source, int id, Item item)
        {
            return new EnvironmentalHazardDetection { Hazard = hazard, Source = source, SourceID = id, WorldItem = item };
        }

        private static EnvironmentalHazardDefinition FindByLand(int id) => Find(id, d => d.LandTiles);
        private static EnvironmentalHazardDefinition FindByStatic(int id) => Find(id, d => d.StaticTiles);
        private static EnvironmentalHazardDefinition FindByWorldItem(int id) => Find(id, d => d.WorldItemIDs);

        private static EnvironmentalHazardDefinition Find(int id, Func<EnvironmentalHazardDefinition, HashSet<int>> selector)
        {
            for (int i = 0; i < _Definitions.Length; i++)
            {
                if (selector(_Definitions[i]).Contains(id))
                {
                    return _Definitions[i];
                }
            }

            return null;
        }

        private static EnvironmentalHazardDefinition[] CreateDefinitions()
        {
            // Swamp IDs are retained from the archived Seeridens Reign implementation.
            var swamp = new EnvironmentalHazardDefinition(EnvironmentalHazardType.Swamp)
                .AddLandRange(15717, 15941)
                .AddStaticRange(12809, 12810).AddStaticRange(12813, 12817).AddStaticRange(12819, 12824)
                .AddStaticRange(12826, 12830).AddStaticRange(12832, 12836).AddStaticRange(12838, 12842)
                .AddStaticRange(12844, 12852).AddStaticRange(12854, 12863).AddStaticRange(12865, 12933)
                .AddWorldItemRange(12809, 12810).AddWorldItemRange(12813, 12817).AddWorldItemRange(12819, 12824)
                .AddWorldItemRange(12826, 12830).AddWorldItemRange(12832, 12836).AddWorldItemRange(12838, 12842)
                .AddWorldItemRange(12844, 12852).AddWorldItemRange(12854, 12863).AddWorldItemRange(12865, 12933);

            // Approved future hazard categories intentionally have no tile definitions or effects yet.
            return new[]
            {
                swamp,
                new EnvironmentalHazardDefinition(EnvironmentalHazardType.Lava),
                new EnvironmentalHazardDefinition(EnvironmentalHazardType.Arctic),
                new EnvironmentalHazardDefinition(EnvironmentalHazardType.Desert)
            };
        }

        private static void ValidateDefinitions()
        {
            ValidateSet("land", d => d.LandTiles);
            ValidateSet("static", d => d.StaticTiles);
            ValidateSet("world item", d => d.WorldItemIDs);
        }

        private static void ValidateSet(string source, Func<EnvironmentalHazardDefinition, HashSet<int>> selector)
        {
            var owners = new Dictionary<int, EnvironmentalHazardType>();
            for (int i = 0; i < _Definitions.Length; i++)
            {
                foreach (int id in selector(_Definitions[i]))
                {
                    if (owners.TryGetValue(id, out EnvironmentalHazardType owner) && owner != _Definitions[i].Type)
                    {
                        Console.WriteLine("Environmental Hazards: conflicting {0} ID {1} ({2} and {3}).", source, id, owner, _Definitions[i].Type);
                    }
                    else
                    {
                        owners[id] = _Definitions[i].Type;
                    }
                }
            }
        }
    }
}

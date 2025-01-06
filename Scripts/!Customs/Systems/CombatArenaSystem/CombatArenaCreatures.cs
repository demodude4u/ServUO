using System;
using Server.Items;

namespace Server.Mobiles
{
	public interface IArenaCreature
	{
	}
	
    public class ArenaAirElemental : AirElemental, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaAirElemental() : base() { NoLootOnDeath = true; }
        public ArenaAirElemental(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
	
    public class ArenaAncientLich : AncientLich, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaAncientLich() : base() { NoLootOnDeath = true; }
        public ArenaAncientLich(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaArcticOgreLord : ArcticOgreLord, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaArcticOgreLord() : base() { NoLootOnDeath = true; }
        public ArenaArcticOgreLord(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaBloodElemental : BloodElemental, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaBloodElemental() : base() { NoLootOnDeath = true; }
        public ArenaBloodElemental(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaBoneKnight : BoneKnight, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaBoneKnight() : base() { NoLootOnDeath = true; }
        public ArenaBoneKnight(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaBoneMagi : BoneMagi, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaBoneMagi() : base() { NoLootOnDeath = true; }
        public ArenaBoneMagi(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaBrigand : Brigand, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaBrigand() : base() { NoLootOnDeath = true; }
        public ArenaBrigand(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaCrystalElemental : CrystalElemental, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaCrystalElemental() : base() { NoLootOnDeath = true; }
        public ArenaCrystalElemental(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaCyclops : Cyclops, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaCyclops() : base() { NoLootOnDeath = true; }
        public ArenaCyclops(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaDaemon : Daemon, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaDaemon() : base() { NoLootOnDeath = true; }
        public ArenaDaemon(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaDireWolf : DireWolf, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaDireWolf() : base() { NoLootOnDeath = true; }
        public ArenaDireWolf(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaDragon : Dragon, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaDragon() : base() { NoLootOnDeath = true; }
        public ArenaDragon(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaDrake : Drake, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaDrake() : base() { NoLootOnDeath = true; }
        public ArenaDrake(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaEfreet : Efreet, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaEfreet() : base() { NoLootOnDeath = true; }
        public ArenaEfreet(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaElderGazer : ElderGazer, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaElderGazer() : base() { NoLootOnDeath = true; }
        public ArenaElderGazer(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaEttin : Ettin, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaEttin() : base() { NoLootOnDeath = true; }
        public ArenaEttin(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaEvilMage : EvilMage, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaEvilMage() : base() { NoLootOnDeath = true; }
        public ArenaEvilMage(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaEvilMageLord : EvilMageLord, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaEvilMageLord() : base() { NoLootOnDeath = true; }
        public ArenaEvilMageLord(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaExecutioner : Executioner, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaExecutioner() : base() { NoLootOnDeath = true; }
        public ArenaExecutioner(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaFireElemental : FireElemental, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaFireElemental() : base() { NoLootOnDeath = true; }
        public ArenaFireElemental(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaFrostTroll : FrostTroll, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaFrostTroll() : base() { NoLootOnDeath = true; }
        public ArenaFrostTroll(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaGargoyle : Gargoyle, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaGargoyle() : base() { NoLootOnDeath = true; }
        public ArenaGargoyle(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaGazer : Gazer, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaGazer() : base() { NoLootOnDeath = true; }
        public ArenaGazer(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaGhoul : Ghoul, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaGhoul() : base() { NoLootOnDeath = true; }
        public ArenaGhoul(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaHeadlessOne : HeadlessOne, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaHeadlessOne() : base() { NoLootOnDeath = true; }
        public ArenaHeadlessOne(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaHellCat : HellCat, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaHellCat() : base() { NoLootOnDeath = true; }
        public ArenaHellCat(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaIceElemental : IceElemental, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaIceElemental() : base() { NoLootOnDeath = true; }
        public ArenaIceElemental(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaIceFiend : IceFiend, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaIceFiend() : base() { NoLootOnDeath = true; }
        public ArenaIceFiend(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaIceSerpent : IceSerpent, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaIceSerpent() : base() { NoLootOnDeath = true; }
        public ArenaIceSerpent(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaLavaLizard : LavaLizard, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaLavaLizard() : base() { NoLootOnDeath = true; }
        public ArenaLavaLizard(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaLavaSerpent : LavaSerpent, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaLavaSerpent() : base() { NoLootOnDeath = true; }
        public ArenaLavaSerpent(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaLavaSnake : LavaSnake, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaLavaSnake() : base() { NoLootOnDeath = true; }
        public ArenaLavaSnake(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaLich : Lich, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaLich() : base() { NoLootOnDeath = true; }
        public ArenaLich(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaLichLord : LichLord, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaLichLord() : base() { NoLootOnDeath = true; }
        public ArenaLichLord(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaMummy : Mummy, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaMummy() : base() { NoLootOnDeath = true; }
        public ArenaMummy(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOgre : Ogre, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOgre() : base() { NoLootOnDeath = true; }
        public ArenaOgre(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOgreLord : OgreLord, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOgreLord() : base() { NoLootOnDeath = true; }
        public ArenaOgreLord(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOphidianArchmage : OphidianArchmage, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOphidianArchmage() : base() { NoLootOnDeath = true; }
        public ArenaOphidianArchmage(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOphidianKnight : OphidianKnight, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOphidianKnight() : base() { NoLootOnDeath = true; }
        public ArenaOphidianKnight(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOphidianMage : OphidianMage, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOphidianMage() : base() { NoLootOnDeath = true; }
        public ArenaOphidianMage(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOphidianMatriarch : OphidianMatriarch, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOphidianMatriarch() : base() { NoLootOnDeath = true; }
        public ArenaOphidianMatriarch(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOphidianWarrior : OphidianWarrior, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOphidianWarrior() : base() { NoLootOnDeath = true; }
        public ArenaOphidianWarrior(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOrc : Orc, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOrc() : base() { NoLootOnDeath = true; }
        public ArenaOrc(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOrcBomber : OrcBomber, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOrcBomber() : base() { NoLootOnDeath = true; }
        public ArenaOrcBomber(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOrcBrute : OrcBrute, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOrcBrute() : base() { NoLootOnDeath = true; }
        public ArenaOrcBrute(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOrcCaptain : OrcCaptain, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOrcCaptain() : base() { NoLootOnDeath = true; }
        public ArenaOrcCaptain(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOrcScout : OrcScout, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOrcScout() : base() { NoLootOnDeath = true; }
        public ArenaOrcScout(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOrcishLord : OrcishLord, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOrcishLord() : base() { NoLootOnDeath = true; }
        public ArenaOrcishLord(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaOrcishMage : OrcishMage, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaOrcishMage() : base() { NoLootOnDeath = true; }
        public ArenaOrcishMage(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaPoisonElemental : PoisonElemental, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaPoisonElemental() : base() { NoLootOnDeath = true; }
        public ArenaPoisonElemental(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaPredatorHellCat : PredatorHellCat, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaPredatorHellCat() : base() { NoLootOnDeath = true; }
        public ArenaPredatorHellCat(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaRatman : Ratman, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaRatman() : base() { NoLootOnDeath = true; }
        public ArenaRatman(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaRatmanArcher : RatmanArcher, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaRatmanArcher() : base() { NoLootOnDeath = true; }
        public ArenaRatmanArcher(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaRatmanMage : RatmanMage, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaRatmanMage() : base() { NoLootOnDeath = true; }
        public ArenaRatmanMage(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaRottingCorpse : RottingCorpse, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaRottingCorpse() : base() { NoLootOnDeath = true; }
        public ArenaRottingCorpse(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaSavage : Savage, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaSavage() : base() { NoLootOnDeath = true; }
        public ArenaSavage(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaSavageRider : SavageRider, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaSavageRider() : base() { NoLootOnDeath = true; }
        public ArenaSavageRider(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaShade : Shade, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaShade() : base() { NoLootOnDeath = true; }
        public ArenaShade(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaSilverSerpent : SilverSerpent, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaSilverSerpent() : base() { NoLootOnDeath = true; }
        public ArenaSilverSerpent(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaSkeletalKnight : SkeletalKnight, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaSkeletalKnight() : base() { NoLootOnDeath = true; }
        public ArenaSkeletalKnight(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaSkeletalMage : SkeletalMage, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaSkeletalMage() : base() { NoLootOnDeath = true; }
        public ArenaSkeletalMage(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaSkeleton : Skeleton, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaSkeleton() : base() { NoLootOnDeath = true; }
        public ArenaSkeleton(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaSnowElemental : SnowElemental, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaSnowElemental() : base() { NoLootOnDeath = true; }
        public ArenaSnowElemental(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaSpectralArmour : SpectralArmour, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaSpectralArmour() : base() { NoLootOnDeath = true; }
        public ArenaSpectralArmour(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaSpectre : Spectre, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaSpectre() : base() { NoLootOnDeath = true; }
        public ArenaSpectre(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaStoneGargoyle : StoneGargoyle, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaStoneGargoyle() : base() { NoLootOnDeath = true; }
        public ArenaStoneGargoyle(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaTerathanAvenger : TerathanAvenger, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaTerathanAvenger() : base() { NoLootOnDeath = true; }
        public ArenaTerathanAvenger(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaTerathanMatriarch : TerathanMatriarch, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaTerathanMatriarch() : base() { NoLootOnDeath = true; }
        public ArenaTerathanMatriarch(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaTerathanWarrior : TerathanWarrior, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaTerathanWarrior() : base() { NoLootOnDeath = true; }
        public ArenaTerathanWarrior(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaTitan : Titan, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaTitan() : base() { NoLootOnDeath = true; }
        public ArenaTitan(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaTroll : Troll, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaTroll() : base() { NoLootOnDeath = true; }
        public ArenaTroll(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaWaterElemental : WaterElemental, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaWaterElemental() : base() { NoLootOnDeath = true; }
        public ArenaWaterElemental(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaWhiteWyrm : WhiteWyrm, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaWhiteWyrm() : base() { NoLootOnDeath = true; }
        public ArenaWhiteWyrm(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaWraith : Wraith, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaWraith() : base() { NoLootOnDeath = true; }
        public ArenaWraith(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaWyvern : Wyvern, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaWyvern() : base() { NoLootOnDeath = true; }
        public ArenaWyvern(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class ArenaZombie : Zombie, IArenaCreature
    {
        public override bool CanBeParagon { get { return false; } }
        public override bool AllowMaleTamer { get { return false; } }
        public override bool AllowFemaleTamer { get { return false; } }
        public override bool Commandable { get { return false; } }
        public override bool GivesFameAndKarmaAward { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IgnoreYoungProtection { get { return true; } }
        public override void GenerateLoot() { }
        public override void OnKilledBy(Mobile mob) { }
        
		[Constructable]
        public ArenaZombie() : base() { NoLootOnDeath = true; }
        public ArenaZombie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write(0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}

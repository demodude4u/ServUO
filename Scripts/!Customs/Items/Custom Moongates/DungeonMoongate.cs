//By: MonZon, 6-14-2022.
#region References
using Server.Commands;
using Server.Engines.CityLoyalty;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Server.Items
{
    public class DungeonMoongate : Item
    {
        public static List<DungeonMoongate> Moongates { get; private set; }

        static DungeonMoongate()
        {
            Moongates = new List<DungeonMoongate>();
        }

        public static IEnumerable<DungeonMoongate> FindGates(Map map)
        {
            DungeonMoongate o;

            int i = Moongates.Count;

            while (--i >= 0)
            {
                o = Moongates[i];

                if (o == null || o.Deleted)
                {
                    Moongates.RemoveAt(i);
                }
                else if (o.Map == map)
                {
                    yield return o;
                }
            }
        }

        public override int LabelNumber => 1023952;  // Blue Moongate

        public override bool HandlesOnMovement => true;
        public override bool ForceShowProperties => true;

        [Constructable]
        public DungeonMoongate()
            : base(0xF6C)
        {
			Name = "Dungeon Moongate";
			Hue = 126;
            Movable = false;
            Light = LightType.Circle300;

            Moongates.Add(this);
        }

        public DungeonMoongate(Serial serial)
            : base(serial)
        {
            Moongates.Add(this);
        }

        public override void OnDelete()
        {
            base.OnDelete();

            Moongates.Remove(this);
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            Moongates.Remove(this);
        }

        public override void OnDoubleClick(Mobile m)
        {
            if (m.InRange(GetWorldLocation(), 1))
            {
                UseGate(m);
            }
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (m.Player && m.CanSee(this))
            {
                UseGate(m);
            }

            return m.Map == Map && m.InRange(this, 1);
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m.Player && !Utility.InRange(m.Location, Location, 1) && Utility.InRange(oldLocation, Location, 1))
            {
                m.CloseGump(typeof(DMoongateGump));
            }
        }

        public virtual bool CanUseGate(Mobile m, bool message)
        {
            if (m.IsStaff())
            {
                //Staff can always use a gate!
                return true;
            }

            if (m.Criminal)
            {
                // Thou'rt a criminal and cannot escape so easily.
                m.SendLocalizedMessage(1005561, "", 0x22);
                return false;
            }

            if (SpellHelper.CheckCombat(m))
            {
                // Wouldst thou flee during the heat of battle??
                m.SendLocalizedMessage(1005564, "", 0x22);
                return false;
            }

            if (m.Spell != null)
            {
                // You are too busy to do that at the moment.
                m.SendLocalizedMessage(1049616);
                return false;
            }

            if (m.Holding != null)
            {
                // You cannot teleport while dragging an object.
                m.SendLocalizedMessage(1071955);
                return false;
            }

            return true;
        }

        public bool UseGate(Mobile m)
        {
            if (!CanUseGate(m, true))
            {
                return false;
            }

            m.CloseGump(typeof(DMoongateGump));
            m.SendGump(new DMoongateGump(m, this));

            PlaySound(m);

            return true;
        }

        public virtual void PlaySound(Mobile m)
        {
            if (!m.Hidden || m.IsPlayer())
            {
                Effects.PlaySound(m.Location, m.Map, 0x20E);
            }
            else
            {
                m.SendSound(0x20E);
            }
        }

        protected DMEntry FindEntry()
        {
            return FindEntry(DMList.GetList(Map));
        }

        protected DMEntry FindEntry(DMList list)
        {
            if (list != null)
            {
                return DMList.FindEntry(list, Location);
            }

            return null;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            reader.ReadInt();
        }
    }

    public class DMEntry
    {
        public Point3D Location { get; private set; }
        public Map Map { get; private set; }
        public TextDefinition Number { get; private set; }
        public TextDefinition Desc { get; private set; }

        public DMEntry(Point3D loc, TextDefinition number)
            : this(loc, number, string.Empty, null)
        { }

        public DMEntry(Point3D loc, TextDefinition number, Map map)
            : this(loc, number, string.Empty, map)
        { }

        public DMEntry(Point3D loc, TextDefinition number, TextDefinition desc)
            : this(loc, number, desc, null)
        { }

        public DMEntry(Point3D loc, TextDefinition number, TextDefinition desc, Map map)
        {
            Location = loc;
            Map = map;
            Number = number;
            Desc = desc;
        }
    }

    public class DMList
    {
        public static readonly DMList Trammel = new DMList(
            1012000,
            1012012,
            Map.Trammel,
            new[]
            {
					new DMEntry ( new Point3D( 2499,  919,   0 ),  "Covetous" ),
			        new DMEntry ( new Point3D( 4111,  432,   5 ),  "Deceit" ),
			        new DMEntry ( new Point3D( 1298, 1080,   0 ),  "Despise" ),
			        new DMEntry ( new Point3D( 1176, 2637,   0 ),  "Destard" ),
			        new DMEntry ( new Point3D( 4721, 3822,   0 ),  "Hythloth" ),
			        new DMEntry ( new Point3D(  514, 1561,   0 ),  "Shame" ),
					new DMEntry ( new Point3D( 2043,  238,  10 ),  "Wrong" ),
			        new DMEntry ( new Point3D( 5451, 3143, -60 ),  "Terathan Keep" ),
					new DMEntry ( new Point3D( 5760, 2908,  15 ),  "Fire" ),
					new DMEntry ( new Point3D( 5210, 2322,  30 ),  "Ice" ),
					new DMEntry ( new Point3D( 1716, 2993,   0 ),  "Painted Caves" ),
					new DMEntry ( new Point3D( 5576, 3018,  25 ),  "Palace of Paroxysmus" ),
					new DMEntry ( new Point3D( 3784, 1097,  14 ),  "Prism of Light" ),
					new DMEntry ( new Point3D( 764, 1646,    0 ),  "Sanctuary" ),
					new DMEntry ( new Point3D( 586, 1643,   -5 ),  "Blighted Grove" ),
					new DMEntry ( new Point3D( 4194, 3269,   0 ),  "The Underworld" )
			});

        public static readonly DMList Felucca = new DMList(
            1012001,
            1012013,
            Map.Felucca,
            new[]
            {
					new DMEntry ( new Point3D( 2499,  919,   0 ),  "Covetous" ),
			        new DMEntry ( new Point3D( 4111,  432,   5 ),  "Deceit" ),
			        new DMEntry ( new Point3D( 1298, 1080,   0 ),  "Despise" ),
			        new DMEntry ( new Point3D( 1176, 2637,   0 ),  "Destard" ),
			        new DMEntry ( new Point3D( 4721, 3822,   0 ),  "Hythloth" ),
			        new DMEntry ( new Point3D(  514, 1561,   0 ),  "Shame" ),
					new DMEntry ( new Point3D( 2043,  238,  10 ),  "Wrong" ),
			        new DMEntry ( new Point3D( 5451, 3143, -60 ),  "Terathan Keep" ),
					new DMEntry ( new Point3D( 5760, 2908,  15 ),  "Fire" ),
					new DMEntry ( new Point3D( 5210, 2322,  30 ),  "Ice" ),
					new DMEntry ( new Point3D( 1716, 2993,   0 ),  "Painted Caves" ),
					new DMEntry ( new Point3D( 5576, 3018,  25 ),  "Palace of Paroxysmus" ),
					new DMEntry ( new Point3D( 3784, 1097,  14 ),  "Prism of Light" ),
					new DMEntry ( new Point3D( 764, 1646,    0 ),  "Sanctuary" ),
					new DMEntry ( new Point3D( 586, 1643,   -5 ),  "Blighted Grove" ),
					new DMEntry ( new Point3D( 6009, 3775,  19 ),  "Khaldun" )
			});

        /*public static readonly DMList Ilshenar = new DMList(
            1012002,
            1012014,
            Map.Ilshenar,
            new[]
            {
                new DMEntry(new Point3D(1215, 467, -13), 1012015), // Compassion
				new DMEntry(new Point3D(722, 1366, -60), 1012016), // Honesty
				new DMEntry(new Point3D(744, 724, -28), 1012017), // Honor
				new DMEntry(new Point3D(281, 1016, 0), 1012018), // Humility
				new DMEntry(new Point3D(987, 1011, -32), 1012019), // Justice
				new DMEntry(new Point3D(1174, 1286, -30), 1012020), // Sacrifice
				new DMEntry(new Point3D(1532, 1340, -3), 1012021), // Spirituality
				new DMEntry(new Point3D(528, 216, -45), 1012022), // Valor
				new DMEntry(new Point3D(1721, 218, 96), 1019000) // Chaos
			});*/

        public static readonly DMList Malas = new DMList(
            1060643,
            1062039,
            Map.Malas,
            new[]
            {
                new DMEntry ( new Point3D( 2367, 1268, -85 ), "Doom" )
			});

        public static readonly DMList Tokuno = new DMList(
            1063258,
            1063415,
            Map.Tokuno,
            new[]
            {
                new DMEntry( new Point3D( 977,  218, 23 ),  "Fan Dancer's Dojo" ),     
				new DMEntry( new Point3D(  259, 785, 64 ),  "Yomotsu Mines" )
			});

        public static readonly DMList TerMur = new DMList(
            1113602,
            1113604,
            Map.TerMur,
            new[]
            {
                new DMEntry( new Point3D( 997, 3848, -41 ),  "Tomb of Kings" )
            });

        public static readonly DMList[] Lists = { Trammel, Felucca, /*Ilshenar,*/ Malas, Tokuno, TerMur };
        public static readonly DMList[] ListsYoung = { Trammel, /*Ilshenar,*/ Malas, Tokuno, TerMur };
        public static readonly DMList[] RedLists = { Felucca };
        public static readonly DMList[] SigilLists = { Felucca };

        public static readonly DMList[] AllLists = { Trammel, Felucca, /*Ilshenar,*/ Malas, Tokuno, TerMur };

        public static DMList GetList(Map map)
        {
            if (map == null || map == Map.Internal)
            {
                return null;
            }

            if (map == Map.Trammel)
            {
                return Trammel;
            }

            if (map == Map.Felucca)
            {
                return Felucca;
            }

            /*if (map == Map.Ilshenar)
            {
                return Ilshenar;
            }*/

            if (map == Map.Malas)
            {
                return Malas;
            }

            if (map == Map.Tokuno)
            {
                return Tokuno;
            }

            if (map == Map.TerMur)
            {
                return TerMur;
            }

            return null;
        }

        public static int IndexOfEntry(DMEntry entry)
        {
            DMList list = AllLists.FirstOrDefault(o => o.Entries.Contains(entry));

            return IndexOfEntry(list, entry);
        }

        public static int IndexOfEntry(DMList list, DMEntry entry)
        {
            if (list != null && entry != null)
            {
                return Array.IndexOf(list.Entries, entry);
            }

            return -1;
        }

        public static DMEntry FindEntry(DMList list, Point3D loc)
        {
            if (list != null)
            {
                return list.Entries.FirstOrDefault(o => o.Location == loc);
            }

            return null;
        }

        public static DMEntry FindEntry(Map map, Point3D loc)
        {
            DMList list = GetList(map);

            if (list != null)
            {
                return FindEntry(list, loc);
            }

            return null;
        }

        private readonly TextDefinition m_Number;
        private readonly TextDefinition m_SelNumber;
        private readonly Map m_Map;
        private readonly DMEntry[] m_Entries;

        public DMList(TextDefinition number, TextDefinition selNumber, Map map, DMEntry[] entries)
        {
            m_Number = number;
            m_SelNumber = selNumber;
            m_Map = map;
            m_Entries = entries;
        }

        public TextDefinition Number => m_Number;
        public TextDefinition SelNumber => m_SelNumber;
        public Map Map => m_Map;
        public DMEntry[] Entries => m_Entries;
    }

    public class DMoongateGump : Gump
    {
        private readonly Mobile m_Mobile;
        private readonly Item m_Moongate;
        private readonly DMList[] m_Lists;

        public DMoongateGump(Mobile mobile, Item moongate)
            : base(100, 100)
        {
            m_Mobile = mobile;
            m_Moongate = moongate;

            DMList[] checkLists;

            if (mobile.Player)
            {
                if (mobile.IsStaff())
                {
                    checkLists = DMList.Lists;
                }
                /*else if (Engines.VvV.VvVSigil.ExistsOn(mobile))
                {
                    checkLists = DMList.SigilLists;
                }*/
                else if (SpellHelper.RestrictRedTravel && mobile.Murderer && !Siege.SiegeShard)
                {
                    checkLists = DMList.RedLists;
                }
                else
                {
                    bool young = mobile is PlayerMobile && ((PlayerMobile)mobile).Young;

                    checkLists = young ? DMList.ListsYoung : DMList.Lists;
                }
            }
            else
            {
                checkLists = DMList.Lists;
            }

            m_Lists = new DMList[checkLists.Length];

            for (int i = 0; i < m_Lists.Length; ++i)
            {
                m_Lists[i] = checkLists[i];
            }

            for (int i = 0; i < m_Lists.Length; ++i)
            {
                if (m_Lists[i].Map == mobile.Map)
                {
                    DMList temp = m_Lists[i];

                    m_Lists[i] = m_Lists[0];
                    m_Lists[0] = temp;

                    break;
                }
            }

            AddPage(0);

            AddBackground( 0, 0, 380, 485, 9390 );
            AddButton( 23, 400, 4005, 4007, 1, GumpButtonType.Reply, 0);
            AddHtmlLocalized(55, 400, 140, 25, 1011036, false, false); // OKAY
            AddButton( 23, 425, 4005, 4007, 0, GumpButtonType.Reply, 0);
            AddHtmlLocalized(55, 425, 425, 25, 1011012, false, false); // CANCEL
            AddHtmlLocalized( 25, 5, 200, 20, 1012011, false, false); // Pick your destination:

            for (int i = 0; i < checkLists.Length; ++i)
            {
                if (Siege.SiegeShard && checkLists[i].Number == 1012000) // Trammel
                {
                    continue;
                }

                AddButton(25, 35 + (i * 25), 2117, 2118, 0, GumpButtonType.Page, Array.IndexOf(m_Lists, checkLists[i]) + 1);

                if (checkLists[i].Number.Number > 0)
                {
                    AddHtmlLocalized(43, 35 + (i * 25), 150, 20, checkLists[i].Number.Number, false, false);
                }
                else if (!string.IsNullOrEmpty(checkLists[i].Number.String))
                {
                    AddHtml(43, 35 + (i * 25), 150, 20, checkLists[i].Number.String, false, false);
                }
            }

            for (int i = 0; i < m_Lists.Length; ++i)
            {
                RenderPage(i, Array.IndexOf(checkLists, m_Lists[i]));
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0) // Cancel
            {
                return;
            }
            if (m_Mobile.Deleted || m_Moongate.Deleted || m_Mobile.Map == null)
            {
                return;
            }

            int[] switches = info.Switches;

            if (switches.Length == 0)
            {
                return;
            }

            int switchID = switches[0];
            int listIndex = switchID / 100;
            int listEntry = switchID % 100;

            if (listIndex < 0 || listIndex >= m_Lists.Length)
            {
                return;
            }

            var list = m_Lists[listIndex];

            if (listEntry < 0 || listEntry >= list.Entries.Length)
            {
                return;
            }

            var entry = list.Entries[listEntry];
            var map = entry.Map ?? list.Map;

            if (m_Mobile.Map == map && m_Mobile.InRange(entry.Location, 1))
            {
                m_Mobile.SendLocalizedMessage(1019003); // You are already there.
                return;
            }
            if (m_Mobile.IsStaff())
            {
                //Staff can always use a gate!
            }
            else if (!m_Mobile.InRange(m_Moongate.GetWorldLocation(), 1) || m_Mobile.Map != m_Moongate.Map)
            {
                m_Mobile.SendLocalizedMessage(1019002); // You are too far away to use the gate.
                return;
            }
            else if (m_Mobile.Player && SpellHelper.RestrictRedTravel && m_Mobile.Murderer && map != Map.Felucca && !Siege.SiegeShard)
            {
                m_Mobile.SendLocalizedMessage(1019004); // You are not allowed to travel there.
                return;
            }
            /*else if (Engines.VvV.VvVSigil.ExistsOn(m_Mobile) && map != Engines.VvV.ViceVsVirtueSystem.Facet)
            {
                m_Mobile.SendLocalizedMessage(1019004); // You are not allowed to travel there.
                return;
            }*/
            else if (m_Mobile.Criminal)
            {
                m_Mobile.SendLocalizedMessage(1005561, "", 0x22); // Thou'rt a criminal and cannot escape so easily.
                return;
            }
            else if (SpellHelper.CheckCombat(m_Mobile))
            {
                m_Mobile.SendLocalizedMessage(1005564, "", 0x22); // Wouldst thou flee during the heat of battle??
                return;
            }
            else if (m_Mobile.Spell != null)
            {
                m_Mobile.SendLocalizedMessage(1049616); // You are too busy to do that at the moment.
                return;
            }

            BaseCreature.TeleportPets(m_Mobile, entry.Location, map);

            m_Mobile.Combatant = null;
            m_Mobile.Warmode = false;
            m_Mobile.Hidden = true;

            m_Mobile.MoveToWorld(entry.Location, map);

            Effects.PlaySound(entry.Location, map, 0x1FE);

            //CityTradeSystem.OnQuickTravelUsed(m_Mobile);
        }

        private void RenderPage(int index, int offset)
        {
            DMList list = m_Lists[index];

            if (Siege.SiegeShard && list.Number == 1012000) // Trammel
                return;

            AddPage(index + 1);

            AddButton(25, 35 + (offset * 25), 2117, 2118, 0, GumpButtonType.Page, index + 1);

            if (list.SelNumber.Number > 0)
            {
                AddHtmlLocalized(43, 35 + (offset * 25), 150, 20, list.SelNumber.Number, false, false);
            }
            else if (!string.IsNullOrEmpty(list.SelNumber.String))
            {
                AddHtml(43, 35 + (offset * 25), 150, 20, list.SelNumber.String, false, false);
            }

            DMEntry[] entries = list.Entries;

            for (int i = 0; i < entries.Length; ++i)
            {
                AddRadio(200, 35 + (i * 25), 210, 211, false, (index * 100) + i);

                if (entries[i].Number.Number > 0)
                {
                    AddHtmlLocalized(225, 35 + (i * 25), 150, 20, entries[i].Number.Number, false, false);
                }
                else if (!string.IsNullOrEmpty(entries[i].Number.String))
                {
                    AddHtml(225, 35 + (i * 25), 150, 20, entries[i].Number.String, false, false);
                }
            }
        }
    }
}

using System;
using System.Xml;

namespace Server.Regions
{
    public class DungeonRegion : BaseRegion
    {
        private Point3D m_EntranceLocation;
        private Map m_EntranceMap;

        public DungeonRegion(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
        {
            XmlElement entrEl = xml["entrance"];

            Map entrMap = map;
            ReadMap(entrEl, "map", ref entrMap, false);

            if (ReadPoint3D(entrEl, entrMap, ref this.m_EntranceLocation, false))
                this.m_EntranceMap = entrMap;
        }

        public override void OnEnter(Mobile m)
        {
            if (this.Name != "")
            {
                m.SendMessage(1161, "You have entered " + this.Name);
            }
        }

        public override void OnExit(Mobile m)
        {
            if (this.Name != "")
            {
                m.SendMessage(38, "You are leaving " + this.Name);
            }

            base.OnExit(m);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override bool YoungProtected
        {
            get
            {
                return false;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D EntranceLocation
        {
            get
            {
                return this.m_EntranceLocation;
            }
            set
            {
                this.m_EntranceLocation = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Map EntranceMap
        {
            get
            {
                return this.m_EntranceMap;
            }
            set
            {
                this.m_EntranceMap = value;
            }
        }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            return false;
        }

        public override void AlterLightLevel(Mobile m, ref int global, ref int personal)
        {
            global = LightCycle.DungeonLevel;
        }
    }
}

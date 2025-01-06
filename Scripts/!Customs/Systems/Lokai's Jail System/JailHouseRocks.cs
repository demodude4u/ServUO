using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
    public abstract class JailHouseRocks : Item
    {
        private MyTimer myTimer;

        private class MyTimer : Timer
        {
            private JailHouseRocks mRocks;

            public MyTimer(JailHouseRocks rocks)
                : base(TimeSpan.FromSeconds(50))
            {
                mRocks = rocks;
                mRocks.Hue = 0x7E3;
                mRocks.InvalidateProperties();
            }
            
            protected override void OnTick()
            {
                mRocks.OrePresent = true;
                mRocks.Hue = 0x7E3;
                mRocks.ReleaseWorldPackets();
                mRocks.InvalidateProperties();
            }
        }

        private bool mOrePresent;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool OrePresent
        {
            get { return mOrePresent; }
            set
            {
                mOrePresent = value;
                InvalidateProperties();
            }
        }

        public void GiveOreTo(Mobile from)
        {
			if (JailUtility.Convicts.ContainsKey(from.Serial.Value))
			{
				JailStatus js = JailUtility.Convicts[from.Serial.Value];
				js.CoalCollected++;
				from.SendMessage("You mined some coal.");
				from.SendMessage("You have {0} out of {1} required.", js.CoalCollected, js.CoalRequired);
			}
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(OrePresent ? "* Double-click to mine. *" : "* No ore present. *");
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(this.Location, 1))
            {
				if (OrePresent)
				{
					from.SendMessage("You start mining the rocks.");
					new InternalTimer(from, this).Start();
				}
				else
				{
					from.SendMessage("There is no ore left in this rock.");
				}
            }
        }

        private void ResetOreSpawn()
        {
            OrePresent = false;
            Hue = 0;
            myTimer.Start();
            InvalidateProperties();
            ReleaseWorldPackets();
        }

        private class InternalTimer : Timer
        {
            private Mobile player;
            private JailHouseRocks rocks;
            private bool successful;
            private int count;

            public InternalTimer(Mobile from, JailHouseRocks miningRocks)
                : base(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(2), 30)
            {
                player = from;
                rocks = miningRocks;
                successful = false;
                count = 0;
            }

            protected override void OnTick()
            {
                count++;
                // Face toward the Rocks
                player.Direction = player.GetDirectionTo(rocks.Location);

                if (rocks == null || !rocks.OrePresent || !player.InRange(rocks.Location, 1))
                {
                    player.SendMessage("You stop mining.");
                    Stop();
                }

                // Do Mining animation
                player.Animate(Utility.RandomBool() ? 11 : 12, 4, 2, true, true, 1);

                // Keep doing it until successful
                successful = Utility.Random(100) - (40 - count) > 50;

                if (successful)
                {
                    rocks.GiveOreTo(player);
                    rocks.ResetOreSpawn();
                    player.Animate(4, 1, 1, true, false, 0);
                    player.InvalidateProperties();
                    Stop();
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override int Hue
        {
            get { return OrePresent ? 0x7E3 : 0; }
            set { base.Hue = value; InvalidateProperties(); }
        }

        public JailHouseRocks()
            : base(6009)
        {
            OrePresent = true;
            Movable = false;
            Stackable = false;
            myTimer = new MyTimer(this);
        }

        public JailHouseRocks(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    OrePresent = true;
                    myTimer = new MyTimer(this);
                    break;
                }
            }
        }
    }

    public class CoalRocks : JailHouseRocks
    {
        [Constructable]
        public CoalRocks() : base() { }

        public CoalRocks(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
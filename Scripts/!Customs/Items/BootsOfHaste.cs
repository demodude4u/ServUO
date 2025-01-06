using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class BootsOfHaste : BaseShoes
    {
		public override bool IsArtifact { get { return true; } }
		private int m_Charges;
		[CommandProperty(AccessLevel.GameMaster)]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}
		
		private bool m_Active;
		[CommandProperty(AccessLevel.GameMaster)]
		public bool Active
		{
			get{ return m_Active;}
			set{ m_Active = true;}
		}
		
		[Constructable]
        public BootsOfHaste()
            : base(12228, 1164)
        {
			this.Name = "Boots of Haste";
			this.ItemID = 12228;
			this.Hue = 1164;
            this.Weight = 3.0;
			this.m_Charges = 300;
			this.m_Active = false;
        }
		
		public static void EventSink_MovementEventHandler(MovementEventArgs args)
		{
			Mobile from = args.Mobile;
			BootsOfHaste boots = from.FindItemOnLayer(Layer.Shoes) as BootsOfHaste;
			if (boots != null && boots.Charges < 300 && !boots.Active)
				boots.AddCharge(1);
		}
		
		public void AddCharge(int toAdd)
		{
			this.m_Charges += toAdd;
			if (this.m_Charges > 300)
				this.m_Charges = 300;
			InvalidateProperties();
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (this.Parent != from)
				from.SendMessage(0, "You must equip these boots to activate their magic!");
			else if (from.Mounted)
				from.SendMessage(0, "You must be on foot to activate this item's magic!");
			else if (this.m_Active)
				this.DoDisable(from);
			else if (this.Charges <= 30)
				from.SendMessage(0, "This item does not yet have enough magic charge to activate! You must wait until at least 30 charges are available.");
			else
				this.DoEnable(from);
			base.OnDoubleClick(from);
		}
		
		public void TimerTick(Mobile from)
		{
			if (this.Parent == from && this.m_Active)
			{
				this.m_Charges -= 1;
				InvalidateProperties();
				if (this.m_Charges <= 0)
				{
					this.m_Charges = 0;
					from.SendMessage(0, "You have depleted the magic of your boots of haste.");
					this.DoDisable(from);
				}
				else
					Timer.DelayCall(TimeSpan.FromSeconds(1.0), delegate{ this.TimerTick(from); });
			}
		}
		
		public void DoDisable(Mobile from)
		{
			this.m_Active = false;
			from.Flying = false;
			from.SendMessage(0, "You stop channeling the magic stored in the boots.");
		}
		
		public void DoEnable(Mobile from)
		{
			this.m_Active = true;
			from.Flying = true;
			from.SendMessage(0,"You channel the magic stored in the boots. You feel as light as a feather!");
			Timer.DelayCall(TimeSpan.FromSeconds(1.0), delegate{ this.TimerTick(from);});
		}
		
		public override void OnAdded(object parent)
		{
			if (parent is PlayerMobile && this.m_Charges > 0)
			{
				PlayerMobile pm = parent as PlayerMobile;
				pm.SendMessage(0, "You feel as if these boots could make you faster. Double click them to activate their magic!");
			}
			else if (parent is PlayerMobile && this.m_Charges <= 0)
			{
				PlayerMobile pm = parent as PlayerMobile;
				pm.SendMessage(0, "You can feel magic accumulating in these boots as you move around. When they have at least 30 charges, double click them to activate their magic!");
			}
			base.OnAdded(parent);
		}
		
		public override void OnRemoved(object parent)
		{
			if (this.m_Active && parent is Mobile)
				this.DoDisable((Mobile)parent);
			
			base.OnRemoved(parent);
		}

        public BootsOfHaste(Serial serial)
            : base(serial)
        {
        }
		
		public static void Initialize()
        {
            EventSink.Movement += new MovementEventHandler(EventSink_MovementEventHandler);
        }
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			
			list.Add("Charges: " + this.m_Charges.ToString());
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
			writer.Write((int)m_Charges);
			writer.Write((bool)m_Active);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
			m_Charges = reader.ReadInt();
			m_Active = reader.ReadBool();
        }
    }
}
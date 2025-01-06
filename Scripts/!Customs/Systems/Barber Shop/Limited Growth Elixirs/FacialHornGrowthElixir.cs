using System;
using Server;
using System.Collections;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class FacialHornGrowthElixir : Item
	{
		[Constructable]
		public FacialHornGrowthElixir() : base( 3836 )
		{
            Name = "Facial Horn Growth Elixir";
		}

		public FacialHornGrowthElixir( Serial serial ) : base( serial )
		{
		}

        public override void OnDoubleClick(Mobile from)
        {
			//dissalow list
			if ( from.Race == Race.Human || from.Race == Race.Elf ) 
               { 
                    from.SendMessage( "Only Gargoyles can use this item!" ); 
                    return; 
               }
 			if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                return;
            }
		if (from.Female)
		{
    		from.SendMessage("Only males can use this item!");
    		return;
        }
            else
            {
                // none 
                if (from.FacialHairItemID == 0)
                {
                    Delete();
                    from.SendMessage("You use the elixir on your chin.");
                    from.FacialHairItemID = 0x42AD; 
                    return;
                }

                //Horn Style 1
                if (from.FacialHairItemID == 0x42AD) 
                {
                    Delete();
                    from.SendMessage("You use the elixir on your chin.");
                    from.FacialHairItemID = 0x42AE;
                    return;
                }                

                //Horn Style 2
                if (from.FacialHairItemID == 0x42AE) 
                {
                    Delete();
                    from.SendMessage("You use the elixir on your chin.");
                    from.FacialHairItemID = 0x42AF; 
                    return;
                }

				//Horn Style 3
                if (from.FacialHairItemID == 0x42AF) 
                {
                    Delete();
                    from.SendMessage("You use the elixir on your chin.");
                    from.FacialHairItemID = 0x42B0; 
                    return;
                }

				//Horn Style 4
                if (from.FacialHairItemID == 0x42B0) 
                {
                    Delete();
                    from.SendMessage("You use the elixir on your chin.");
                    from.FacialHairItemID = 0; 
                    return;
                }

                else
                {
                    
                    from.SendMessage("Your Beard cant get any longer!");
                    return;
                }
            }
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
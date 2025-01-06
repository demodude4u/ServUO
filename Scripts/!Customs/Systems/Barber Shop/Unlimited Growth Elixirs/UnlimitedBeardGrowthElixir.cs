using System;
using Server;
using System.Collections;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class UnlimitedBeardGrowthElixir : Item
	{
		[Constructable]
		public UnlimitedBeardGrowthElixir() : base( 3836 )
		{
            Name = "Beard growth elixir";
		}

		public UnlimitedBeardGrowthElixir( Serial serial ) : base( serial )
		{
		}

        public override void OnDoubleClick(Mobile from)
        {
			//dissalow list
	        if ( from.Race == Race.Gargoyle ) 
            { 
                from.SendMessage( "Gargoyles cannot use this item!" ); 
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
                    from.SendMessage("You use the elixir on your chin.");
                    from.FacialHairItemID = 0x2040; 
                    return;
                }

                //goatee
                if (from.FacialHairItemID == 0x2040) 
                {
                    from.SendMessage("You use the elixir on your chin.");
                    from.FacialHairItemID = 0x203F;
                    return;
                }                

                //shortbeard
                if (from.FacialHairItemID == 0x203F) 
                {
                    from.SendMessage("You use the elixir on your chin.");
                    from.FacialHairItemID = 0x203E; 
                    return;
                }

		//mustashe
                if (from.FacialHairItemID == 0x2041) 
                {
                    from.SendMessage("You use the elixir on your chin.");
                    from.FacialHairItemID = 0x204D; 
                    return;
                }

		//vandyke
                if (from.FacialHairItemID == 0x204D) 
                {
                    from.SendMessage("You use the elixir on your chin.");
                    from.FacialHairItemID = 0x204B; 
                    return;
                }
		
		//mediumshortbeard
                if (from.FacialHairItemID == 0x204B) 
                {
                    from.SendMessage("You use the elixir on your chin.");
                    from.FacialHairItemID = 0x204C; 
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
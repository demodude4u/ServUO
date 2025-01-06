using System;
using Server;

namespace Server.Items
{
   public class LargeStoneStatue : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new LargeStoneStatueDeed(); } }

      [Constructable]
      public LargeStoneStatue()
      {

         AddComponent( new AddonComponent( 0x12A4 ), -1, 0, 0 );    
         AddComponent( new AddonComponent( 0x12A2 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 0x12A3 ), 0, -1, 0 );            
           
      }

      public LargeStoneStatue( Serial serial ) : base( serial )
      {
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

   public class LargeStoneStatueDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new LargeStoneStatue(); } }
      
      [Constructable]
      public LargeStoneStatueDeed()
      {
		Name = "A Large Stone Statue Deed";
   
		}

      public LargeStoneStatueDeed( Serial serial ) : base( serial )
      {

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
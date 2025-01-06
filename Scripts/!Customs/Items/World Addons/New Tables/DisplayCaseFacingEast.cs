using System;
using Server;

namespace Server.Items
{
   public class DisplayCaseFacingEast : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new DisplayCaseFacingEastDeed(); } }

      [Constructable]
      public DisplayCaseFacingEast()
      {
           
         AddComponent( new AddonComponent( 2824 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 2821 ), 0, 0, 3 );    
         AddComponent( new AddonComponent( 2823 ), 0, 1, 0 );    
         AddComponent( new AddonComponent( 2820 ), 0, 1, 3 );    
         AddComponent( new AddonComponent( 2822 ), 0, 2, 0 );    
         AddComponent( new AddonComponent( 2819 ), 0, 2, 3 );    
         AddComponent( new AddonComponent( 2819 ), 0, 2, 3 );               
           
      }

      public DisplayCaseFacingEast( Serial serial ) : base( serial )
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

   public class DisplayCaseFacingEastDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new DisplayCaseFacingEast(); } }
      
      [Constructable]
      public DisplayCaseFacingEastDeed()
      {
Name = "A Display Case Facing East Deed";
   
}

      public DisplayCaseFacingEastDeed( Serial serial ) : base( serial )
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
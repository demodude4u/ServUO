using System;
using Server;

namespace Server.Items
{
   public class DisplayCaseFacingSouth : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new DisplayCaseFacingSouthDeed(); } }

      [Constructable]
      public DisplayCaseFacingSouth()
      {

         AddComponent( new AddonComponent( 2818 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 2815 ), 0, 0, 3 );    
         AddComponent( new AddonComponent( 2817 ), 1, 0, 0 );    
         AddComponent( new AddonComponent( 2814 ), 1, 0, 3 );    
         AddComponent( new AddonComponent( 2816 ), 2, 0, 0 );    
         AddComponent( new AddonComponent( 2813 ), 2, 0, 3 );    
         AddComponent( new AddonComponent( 2813 ), 2, 0, 3 );               
           
      }

      public DisplayCaseFacingSouth( Serial serial ) : base( serial )
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

   public class DisplayCaseFacingSouthDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new DisplayCaseFacingSouth(); } }
      
      [Constructable]
      public DisplayCaseFacingSouthDeed()
      {
Name = "A Display Case Facing South Deed";
   
}

      public DisplayCaseFacingSouthDeed( Serial serial ) : base( serial )
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
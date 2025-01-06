using System;
using Server;

namespace Server.Items
{
   public class HammockSouth : BaseAddon
   {
     public override BaseAddonDeed Deed{ get{ return new HammockSouthDeed(); } }

      [Constructable]
      public HammockSouth()
      {

         AddComponent( new AddonComponent( 4592 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 4593 ), 2, 0, 0 );    
         AddComponent( new AddonComponent( 4593 ), 2, 0, 0 );               
           
      }

      public HammockSouth( Serial serial ) : base( serial )
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

   public class HammockSouthDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new HammockSouth(); } }
      
      [Constructable]
      public HammockSouthDeed()
      {
Name = "A Hammock Facing South Deed";
   
}

      public HammockSouthDeed( Serial serial ) : base( serial )
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
using System;
using Server;

namespace Server.Items
{
   public class HammockEast : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new HammockEastDeed(); } }

      [Constructable]
      public HammockEast()
      {

         AddComponent( new AddonComponent( 4595 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 4594 ), 0, 2, 0 );    
         AddComponent( new AddonComponent( 4594 ), 0, 2, 0 );               
           
      }

      public HammockEast( Serial serial ) : base( serial )
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

   public class HammockEastDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new HammockEast(); } }
      
      [Constructable]
      public HammockEastDeed()
      {
Name = "A Hammock Facing East Deed";
   
}

      public HammockEastDeed( Serial serial ) : base( serial )
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
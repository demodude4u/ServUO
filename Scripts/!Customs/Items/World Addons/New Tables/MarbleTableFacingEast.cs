using System;
using Server;

namespace Server.Items
{
   public class MarbleTableFacingEast : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new MarbleTableFacingEastDeed(); } }

      [Constructable]
      public MarbleTableFacingEast()
      {

         AddComponent( new AddonComponent( 7618 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 7619 ), 0, 1, 0 );    
         AddComponent( new AddonComponent( 7617 ), 0, 2, 0 );    
         AddComponent( new AddonComponent( 7617 ), 0, 2, 0 );               
           
      }

      public MarbleTableFacingEast( Serial serial ) : base( serial )
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

   public class MarbleTableFacingEastDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new MarbleTableFacingEast(); } }
      
      [Constructable]
      public MarbleTableFacingEastDeed()
      {
Name = "A Marble Table Facing East Deed";
   
}

      public MarbleTableFacingEastDeed( Serial serial ) : base( serial )
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
using System;
using Server;

namespace Server.Items
{
   public class MarbleTableFacingSouth : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new MarbleTableFacingSouthDeed(); } }

      [Constructable]
      public MarbleTableFacingSouth()
      {

         AddComponent( new AddonComponent( 7621 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 7622 ), 1, 0, 0 );    
         AddComponent( new AddonComponent( 7620 ), 2, 0, 0 );    
         AddComponent( new AddonComponent( 7620 ), 2, 0, 0 );               
           
      }

      public MarbleTableFacingSouth( Serial serial ) : base( serial )
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

   public class MarbleTableFacingSouthDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new MarbleTableFacingSouth(); } }
      
      [Constructable]
      public MarbleTableFacingSouthDeed()
      {
Name = "A Marble Table Facing South Deed";
   
}

      public MarbleTableFacingSouthDeed( Serial serial ) : base( serial )
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
using System;
using Server;

namespace Server.Items
{
   public class LogTableFacingSouth : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new LogTableFacingSouthDeed(); } }

      [Constructable]
      public LogTableFacingSouth()
      {

         AddComponent( new AddonComponent( 4573 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 4574 ), 1, 0, 0 );    
         AddComponent( new AddonComponent( 4572 ), 2, 0, 0 );    
         AddComponent( new AddonComponent( 4572 ), 2, 0, 0 );               
           
      }

      public LogTableFacingSouth( Serial serial ) : base( serial )
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

   public class LogTableFacingSouthDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new LogTableFacingSouth(); } }
      
      [Constructable]
      public LogTableFacingSouthDeed()
      {
Name = "A Log Table Facing South Deed";
   
}

      public LogTableFacingSouthDeed( Serial serial ) : base( serial )
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
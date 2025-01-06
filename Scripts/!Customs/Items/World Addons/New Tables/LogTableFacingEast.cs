using System;
using Server;

namespace Server.Items
{
   public class LogTableFacingEast : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new LogTableFacingEastDeed(); } }

      [Constructable]
      public LogTableFacingEast()
      {

         AddComponent( new AddonComponent( 4576 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 4577 ), 0, 1, 0 );    
         AddComponent( new AddonComponent( 4575 ), 0, 2, 0 );    
         AddComponent( new AddonComponent( 4575 ), 0, 2, 0 );               
           
      }

      public LogTableFacingEast( Serial serial ) : base( serial )
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

   public class LogTableFacingEastDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new LogTableFacingEast(); } }
      
      [Constructable]
      public LogTableFacingEastDeed()
      {
Name = "A Log Table Facing East Deed";
   
}

      public LogTableFacingEastDeed( Serial serial ) : base( serial )
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
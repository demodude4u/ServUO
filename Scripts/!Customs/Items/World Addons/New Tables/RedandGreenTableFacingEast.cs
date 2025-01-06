using System;
using Server;

namespace Server.Items
{
   public class RedandGreenTableFacingEast : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new RedandGreenTableFacingEastDeed(); } }

      [Constructable]
      public RedandGreenTableFacingEast()
      {

         AddComponent( new AddonComponent( 5737 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 5736 ), 0, 1, 0 );    
         AddComponent( new AddonComponent( 5735 ), 0, 2, 0 );    
         AddComponent( new AddonComponent( 5735 ), 0, 2, 0 );               
           
      }

      public RedandGreenTableFacingEast( Serial serial ) : base( serial )
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

   public class RedandGreenTableFacingEastDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new RedandGreenTableFacingEast(); } }
      
      [Constructable]
      public RedandGreenTableFacingEastDeed()
      {
Name = "A Covered Table Facing East Deed";
   
}

      public RedandGreenTableFacingEastDeed( Serial serial ) : base( serial )
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
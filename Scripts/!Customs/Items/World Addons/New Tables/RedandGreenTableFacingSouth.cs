using System;
using Server;

namespace Server.Items
{
   public class RedandGreenTableFacingSouth : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new RedandGreenTableFacingSouthDeed(); } }

      [Constructable]
      public RedandGreenTableFacingSouth()
      {

         AddComponent( new AddonComponent( 5738 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 5739 ), 1, 0, 0 );    
         AddComponent( new AddonComponent( 5740 ), 2, 0, 0 );    
         AddComponent( new AddonComponent( 5740 ), 2, 0, 0 );               
           
      }

      public RedandGreenTableFacingSouth( Serial serial ) : base( serial )
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

   public class RedandGreenTableFacingSouthDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new RedandGreenTableFacingSouth(); } }
      
      [Constructable]
      public RedandGreenTableFacingSouthDeed()
      {
Name = "A Covered Table Facing South Deed";
   
}

      public RedandGreenTableFacingSouthDeed( Serial serial ) : base( serial )
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
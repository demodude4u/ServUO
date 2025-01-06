using System;
using Server;

namespace Server.Items
{
   public class LightWoodTableFacingEast : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new LightWoodTableFacingEastDeed(); } }

      [Constructable]
      public LightWoodTableFacingEast()
      {

         AddComponent( new AddonComponent( 2924 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 2925 ), 0, 1, 0 );    
         AddComponent( new AddonComponent( 2923 ), 0, 2, 0 );    
         AddComponent( new AddonComponent( 2923 ), 0, 2, 0 );               
           
      }

      public LightWoodTableFacingEast( Serial serial ) : base( serial )
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

   public class LightWoodTableFacingEastDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new LightWoodTableFacingEast(); } }
      
      [Constructable]
      public LightWoodTableFacingEastDeed()
      {
Name = "A Light Wood Table Facing East Deed";
   
}

      public LightWoodTableFacingEastDeed( Serial serial ) : base( serial )
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
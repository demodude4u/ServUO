using System;
using Server;

namespace Server.Items
{
   public class LightWoodTableFacingSouth : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new LightWoodTableFacingSouthDeed(); } }

      [Constructable]
      public LightWoodTableFacingSouth()
      {

         AddComponent( new AddonComponent( 2943 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 2944 ), 1, 0, 0 );    
         AddComponent( new AddonComponent( 2942 ), 2, 0, 0 );    
         AddComponent( new AddonComponent( 2942 ), 2, 0, 0 );               
           
      }

      public LightWoodTableFacingSouth( Serial serial ) : base( serial )
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

   public class LightWoodTableFacingSouthDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new LightWoodTableFacingSouth(); } }
      
      [Constructable]
      public LightWoodTableFacingSouthDeed()
      {
Name = "A Light Wood Table Facing South Deed";
   
}

      public LightWoodTableFacingSouthDeed( Serial serial ) : base( serial )
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
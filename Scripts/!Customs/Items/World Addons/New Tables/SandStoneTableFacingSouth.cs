using System;
using Server;

namespace Server.Items
{
   public class SandStoneTableFacingSouth : BaseAddon
   {
      public override BaseAddonDeed Deed{ get{ return new SandStoneTableFacingSouthDeed(); } }

      [Constructable]
      public SandStoneTableFacingSouth()
      {

         AddComponent( new AddonComponent( 7615 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 7616 ), 1, 0, 0 );    
         AddComponent( new AddonComponent( 7614 ), 2, 0, 0 );    
         AddComponent( new AddonComponent( 7614 ), 2, 0, 0 );               
           
      }

      public SandStoneTableFacingSouth( Serial serial ) : base( serial )
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

   public class SandStoneTableFacingSouthDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new SandStoneTableFacingSouth(); } }
      
      [Constructable]
      public SandStoneTableFacingSouthDeed()
      {
Name = "A Sandstone Table Facing South Deed";
   
}

      public SandStoneTableFacingSouthDeed( Serial serial ) : base( serial )
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
using System;
using Server;

namespace Server.Items
{
   public class SandStoneTableFacingEast : BaseAddon
   {
     public override BaseAddonDeed Deed{ get{ return new SandStoneTableFacingEastDeed(); } }

      [Constructable]
      public SandStoneTableFacingEast()
      {

         AddComponent( new AddonComponent( 7612 ), 0, 0, 0 );    
         AddComponent( new AddonComponent( 7613 ), 0, 1, 0 );    
         AddComponent( new AddonComponent( 7611 ), 0, 2, 0 );    
         AddComponent( new AddonComponent( 7611 ), 0, 2, 0 );               
           
      }

      public SandStoneTableFacingEast( Serial serial ) : base( serial )
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

   public class SandStoneTableFacingEastDeed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new SandStoneTableFacingEast(); } }
      
      [Constructable]
      public SandStoneTableFacingEastDeed()
      {
Name = "A Sandstone Table Facing East Deed";
   
}

      public SandStoneTableFacingEastDeed( Serial serial ) : base( serial )
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
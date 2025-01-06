using System;
using Server;

namespace Server.Items
{
   public class PineTable2x3 : BaseAddon
   {
     public override BaseAddonDeed Deed{ get{ return new PineTable2x3Deed(); } }

      [Constructable]
      public PineTable2x3()
      {

         AddComponent( new AddonComponent( 2928 ), 0, 0, 0 );
         AddComponent( new AddonComponent( 2929 ), 1, 0, 0 );
         AddComponent( new AddonComponent( 2931 ), 0, -2, 0 );
         AddComponent( new AddonComponent( 2930 ), 1, -2, 0 );
         AddComponent( new AddonComponent( 2931 ), 0, -1, 0 );
         AddComponent( new AddonComponent( 2931 ), 1, -1, 0 );
           
      }

      public PineTable2x3( Serial serial ) : base( serial )
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

   public class PineTable2x3Deed : BaseAddonDeed
   {
      public override BaseAddon Addon{ get{ return new PineTable2x3(); } }
      
      [Constructable]
      public PineTable2x3Deed()
      {
Name = "A 2x3 Pine Table Deed";
   
}

      public PineTable2x3Deed( Serial serial ) : base( serial )
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
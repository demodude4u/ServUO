using System;
using Server;

namespace Server.Items
{
	public class OrcFace : Item
	{

		[Constructable]
		public OrcFace() : base( 0x141B )
		{
			Name = " ";
			Layer = Layer.Face;
			Hue = 2212;
			Movable = false;			
		}

		public OrcFace( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
//(Using is the keyword, System is the Identifier, these make up the Syntax)
using System;
using Server.Gumps;

namespace Server.Items
{
    //Class has data members and functions
    //Class defines how to make an object (like a blueprint)
    //RubberDucky is the name of the Class
    //RubberDucky is-an Item and therefor "Inherets" Item's Classes and Data Members (Inherited Class)
    public class RubberDucky : Item 
	{
		[Constructable] // Ask Aric
		public RubberDucky() : base(0x2839)
		{
            //Weight, Name and Hue are Data Members of Item
			Weight = 1.0;
			Name = "Rubber Ducky";
			Hue = 1169;
		}

        //(OnDoubleClick is the function, everything within the parenthesis would be the function's parameters)
		public override void OnDoubleClick( Mobile from )
		{
			from.PlaySound( 0xCD );
            from.SendGump(new CharacterCreatorGump(from));
		}				
		
		public RubberDucky(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
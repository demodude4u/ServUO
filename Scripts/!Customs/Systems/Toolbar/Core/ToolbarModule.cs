#region References
using Server;

using Services.Toolbar.Core;
#endregion

namespace Services.Toolbar
{
	public class ToolbarModule
	{
		private ToolbarInfo _ToolbarInfo;
		private readonly Mobile _Mobile;

		public ToolbarModule(Mobile from)
		{
			_Mobile = from;
			_ToolbarInfo = ToolbarCore.GetToolbarInfo(from);
		}

		[CommandProperty(AccessLevel.Developer)]
		public ToolbarInfo ToolbarInfo
		{
			get { return _ToolbarInfo; }
			set
			{
				_ToolbarInfo = value;
				ToolbarCore.SetToolbarInfo(_Mobile, value);
			}
		}
	}
}

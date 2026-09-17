#region References
using System;
using System.Collections.Generic;

using Server;
using Server.Commands;
using Server.Gumps;

using Services.Toolbar.Gumps;
#endregion

namespace Services.Toolbar.Core
{
	public static class ToolbarCore
	{
		public const string SystemVersion = "2.3";
		public const string ReleaseDate = "October 28, 2013";

		private const string PersistencePath = "Saves/Toolbar.bin";
		private static readonly Dictionary<Serial, ToolbarInfo> _Toolbars = new Dictionary<Serial, ToolbarInfo>();

		public static void Initialize()
		{
			CommandHandlers.Register("Toolbar", AccessLevel.VIP, Toolbar_OnCommand);

			EventSink.WorldLoad += Load;
			EventSink.WorldSave += e => Persistence.Serialize(PersistencePath, Save);
			EventSink.Login += OnLogin;
			EventSink.PlayerDeath += OnPlayerDeath;
		}

		public static ToolbarModule GetModule(Mobile mobile)
		{
			return new ToolbarModule(mobile);
		}

		internal static ToolbarInfo GetToolbarInfo(Mobile mobile)
		{
			if (!_Toolbars.TryGetValue(mobile.Serial, out var info))
			{
				info = ToolbarInfo.CreateNew(mobile);
				_Toolbars[mobile.Serial] = info;
			}

			return info;
		}

		internal static void SetToolbarInfo(Mobile mobile, ToolbarInfo info)
		{
			_Toolbars[mobile.Serial] = info;
		}

		private static void Save(GenericWriter writer)
		{
			writer.Write(0); // version
			writer.Write(_Toolbars.Count);

			foreach (var entry in _Toolbars)
			{
				writer.Write(entry.Key);
				entry.Value.Serialize(writer);
			}
		}

		private static void Load()
		{
			_Toolbars.Clear();
			Persistence.Deserialize(PersistencePath, reader =>
			{
				var version = reader.ReadInt();

				if (version != 0)
				{
					return;
				}

				var count = reader.ReadInt();

				for (var i = 0; i < count; i++)
				{
					_Toolbars[reader.ReadInt()] = new ToolbarInfo(reader);
				}
			});
		}

		private static void OnLogin(LoginEventArgs e)
		{
			if (e.Mobile.AccessLevel >= AccessLevel.VIP)
			{
				SendToolbar(e.Mobile);
			}
		}

		public static void OnPlayerDeath(PlayerDeathEventArgs e)
		{
			if (e.Mobile.AccessLevel < AccessLevel.VIP || e.Mobile.NetState == null)
			{
				return;
			}

			e.Mobile.CloseGump(typeof(ToolbarGump));

			Timer.DelayCall(TimeSpan.FromSeconds(2.0), SendToolbar, e.Mobile);
		}

		[Usage("Toolbar")]
		public static void Toolbar_OnCommand(CommandEventArgs e)
		{
			SendToolbar(e.Mobile);
		}

		public static void SendToolbar(Mobile m)
		{
			var module = GetModule(m);

			m.CloseGump(typeof(ToolbarGump));
			m.SendGump(new ToolbarGump(module.ToolbarInfo, m));
		}
	}
}

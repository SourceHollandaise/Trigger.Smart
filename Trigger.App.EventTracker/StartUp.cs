using GLib;
using System;

namespace Trigger.App.EventTracker
{
	public class StartUp
	{
		[STAThread]
		static void Main()
		{
			var application = new AppEventTracker();

			application.InitalizeApplication();
			application.Run();

			ExceptionManager.UnhandledException += (args) =>
			{
				Log.DefaultHandler("App", LogLevelFlags.FlagFatal & LogLevelFlags.Critical, args.ExceptionObject.ToString());
			};

		}
	}
}

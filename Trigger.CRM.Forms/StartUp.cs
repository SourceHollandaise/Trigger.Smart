//using GLib;
using System;

namespace Trigger.Application.WinForms
{
	public class StartUp
	{
		[STAThread]
		static void Main()
		{
			var generator = Eto.EtoEnvironment.Platform.IsMac;

			var application = new CrossPlatformApplication();

			application.InitalizeApplication();
			application.Run();

			/*
			ExceptionManager.UnhandledException += (args) =>
			{
				Log.DefaultHandler("App", LogLevelFlags.FlagFatal & LogLevelFlags.Critical, args.ExceptionObject.ToString());
			};
			*/
		}
	}
}

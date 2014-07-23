//using GLib;
using System;

namespace Trigger.Application.WinForms
{
	public class StartUp
	{
		[STAThread]
		static void Main()
		{
			new CrossPlatformApplication().Run();

			/*
			ExceptionManager.UnhandledException += (args) =>
			{
				Log.DefaultHandler("App", LogLevelFlags.FlagFatal & LogLevelFlags.Critical, args.ExceptionObject.ToString());
			};
			*/
		}
	}
}

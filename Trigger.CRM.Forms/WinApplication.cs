using GLib;
using Trigger.CRM.Model;
using Trigger.WinForms.Layout;

namespace Trigger.Application.WinForms
{

	public static class WinApplication
	{
		static void Main()
		{
			new Bootstrapper().StartUpApplication();
		
			var application = new Eto.Forms.Application();
		
			var types = new []
			{
				typeof(IssueTracker),
				typeof(Project),
				typeof(Document),
				typeof(TimeTracker),
		
			};
		
			application.Initialized += (sender, e) =>
			{
				application.MainForm = new MainForm(types);
				application.MainForm.BringToFront();
				application.MainForm.Show();
		
			};

			ExceptionManager.UnhandledException += (args) =>
			{
				args.ExitApplication = false;

				if (args.IsTerminating)
				{
					Log.DefaultHandler("App", LogLevelFlags.FlagFatal & LogLevelFlags.Critical, args.ExceptionObject.ToString());

					application.RunIteration();
				}
			};

			application.Run();
		}
	}
}

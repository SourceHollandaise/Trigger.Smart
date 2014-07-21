using GLib;
using Trigger.CRM.Model;
using Trigger.WinForms.Layout;
using Trigger.Datastore.Security;

namespace Trigger.Application.WinForms
{
	public static class WinApplication
	{
		static System.Type[] DeclaredTypes()
		{
			return new[]
			{
				typeof(IssueTracker),
				typeof(Project),
				typeof(Document),
				typeof(TimeTracker),
				typeof(User)
			};
		}

		static void Main()
		{
			new Bootstrapper().StartUpApplication();
		
			var application = new Eto.Forms.Application();
		
			application.Initialized += (sender, e) =>
			{
				application.MainForm = new MainViewTemplate(DeclaredTypes());
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

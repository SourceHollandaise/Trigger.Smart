using GLib;
using Trigger.CRM.Model;
using Trigger.WinForms.Actions;
using Trigger.WinForms.Layout;
using Trigger.Datastore.Security;
using System;

namespace Trigger.Application.WinForms
{
	public static class WinApplication
	{
		static Type[] DeclaredTypes()
		{
			return new[]
			{
				typeof(IssueTracker),
				typeof(Area),
				typeof(Document),
				typeof(TimeTracker),
				typeof(Contact),
				typeof(Person),
				typeof(User)
			};
		}

		static Type[] DeclaredControllers()
		{
			return new []
			{
				typeof(ActionActiveWindowsController),
				typeof(ActionApplicationExitController),
				typeof(ActionCloseController),
				typeof(ActionDeleteController),
				typeof(ActionFileDataDetailController),
				typeof(ActionFileDataListController),
				typeof(ActionLinkedListController),
				typeof(ActionNewController),
				typeof(ActionOpenObjectListController),
				typeof(ActionRefreshDetailController),
				typeof(ActionRefreshListController),
				typeof(ActionSaveController),
			
			};
		}

		[STAThread]
		static void Main()
		{
			new Bootstrapper().StartUpApplication();
		
			var application = new Eto.Forms.Application();
		
			ModelTypesDeclaration.DeclareModelTypes(DeclaredTypes());
			ActionControllerDeclaration.DeclareControllerTypes(DeclaredControllers());

			application.Initialized += (sender, e) =>
			{
				application.MainForm = new MainViewTemplate();
				application.MainForm.BringToFront();
				application.MainForm.Show();
			};

			application.Run();

			ExceptionManager.UnhandledException += (args) =>
			{
				//args.ExitApplication = false;

				if (args.IsTerminating)
				{
					Log.DefaultHandler("App", LogLevelFlags.FlagFatal & LogLevelFlags.Critical, args.ExceptionObject.ToString());

					//application.RunIteration();

					//application.Restart();
				}
			};
		}
	}
}

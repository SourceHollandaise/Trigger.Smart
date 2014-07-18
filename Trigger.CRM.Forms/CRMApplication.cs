using System;
using Trigger.CRM.Persistent;
using System.Configuration;
using Trigger.Datastore.Security;
using Trigger.Datastore.Persistent;
using Trigger.Dependency;
using System.Linq;
using Eto.Forms;
using Trigger.CRM.Model;

namespace Trigger.CRM.Forms
{
	public static class CRMApplication
	{
		static void Main()
		{
			new Bootstrapper().StartUpApplication();
			var app = new Application();
			var item = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<IssueTracker>().FirstOrDefault();
			app.Initialized += (sender, e) =>
			{
				app.MainForm = new ModelDetailTemplate(item);
				app.MainForm.Show();
			};


			app.Run();

		}
	}
}

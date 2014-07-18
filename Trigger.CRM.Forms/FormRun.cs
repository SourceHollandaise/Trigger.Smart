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
	public static class FormRun
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


	class Bootstrapper
	{
		IDependencyMap Map
		{
			get
			{
				return DependencyMapProvider.Instance;
			}
		}

		public void StartUpApplication()
		{
			StoreConfigurator.InitStore();

			Register();

			CreateInitialObjects();
		}

		protected void Register()
		{
			var logonType = ConfigurationManager.AppSettings["LogonType"];
			if (!string.IsNullOrWhiteSpace(logonType))
			{
				if (logonType == "System")
					Map.RegisterType<IAuthenticate, SystemAuthenticate>();
				if (logonType == "DataStore")
					Map.RegisterType<IAuthenticate, DataStoreAuthenticate>();
			}
			else
				Map.RegisterType<IAuthenticate, SystemAuthenticate>();

			Map.RegisterType<IdGenerator, GuidIdGenerator>();
			Map.RegisterType<IStore, FileStore>();
		}

		protected void CreateInitialObjects()
		{
			var user = Map.ResolveType<IStore>().LoadAll<User>().FirstOrDefault(p => p.UserName == "Admin");

			if (user == null)
			{
				user = new User{ UserName = "Admin" };
				user.SetPassword("admin");
				user.Save();
			}
		}
	}
}

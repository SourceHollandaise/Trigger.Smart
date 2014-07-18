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

			Map.RegisterType<IAuthenticate, SystemAuthenticate>();

			new SystemAuthenticate().LogOn(new LogonParameters(){ UserName = "trigger", Password = "1234" });

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

using System.Linq;
using Trigger.CRM.Persistent;
using Trigger.Datastore.Persistent;
using Trigger.Datastore.Security;
using Trigger.Dependency;

namespace Trigger.Application.WinForms
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
			Map.RegisterType<IAuthenticate, DataStoreAuthenticate>();
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

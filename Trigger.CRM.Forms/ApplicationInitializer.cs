using System;
using System.Linq;
using Trigger.CRM.Model;
using Trigger.CRM.Persistent;
using Trigger.CRM.Services;
using Trigger.Datastore.Persistent;
using Trigger.Datastore.Security;
using Trigger.Dependency;
using Trigger.WinForms.Actions;
using Trigger.WinForms.Layout;

namespace Trigger.Application.WinForms
{
	public class ApplicationInitializer
	{
		IDependencyMap Map
		{
			get
			{
				return DependencyMapProvider.Instance;
			}
		}

		public virtual void InitalizeDataStore()
		{
			StoreConfigurator.InitStore();
		}

		public virtual void RegisterDependencies()
		{
			Map.RegisterType<IAuthenticate, DataStoreAuthenticate>();
			Map.RegisterType<IdGenerator, GuidIdGenerator>();
			Map.RegisterType<IStore, FileStore>();
			Map.RegisterType<IFileDataService, DocumentDataService>();
		}

		public virtual void CreateInitialObjects()
		{
			var user = Map.ResolveType<IStore>().LoadAll<User>().FirstOrDefault(p => p.UserName == "Admin");

			if (user == null)
			{
				user = new User{ UserName = "Admin", Password = "admin" };
			
				user.Save();
			}
		}

		public virtual void RegisterDeclaredTypes()
		{
			ModelTypesDeclaration.DeclareModelTypes(DeclaredTypes());
			ActionControllerDeclaration.DeclareControllerTypes(DeclaredControllers());
		}

		protected virtual Type[] DeclaredTypes()
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

		protected virtual Type[] DeclaredControllers()
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
	}
}

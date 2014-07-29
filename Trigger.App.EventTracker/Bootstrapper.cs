using System;
using System.Linq;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.EventTracker.Services;
using Trigger.XForms.Controllers;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Model;
using Trigger.BCL.Common.Services;
using Trigger.BCL.Common.Security;
using Trigger.XForms.Visuals;
using Trigger.BCL.EventTracker;
using Trigger.XForms;

namespace Trigger.App.EventTracker
{
    public class Bootstrapper
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
            var config = new StoreConfiguration();
            config.InitStore();

            Map.RegisterInstance<IStoreConfiguration>(config);
        }

        public virtual void RegisterDependencies()
        {
            Map.RegisterType<IViewTemplateConfiguration, ViewTemplateConfiguration>();
            Map.RegisterType<IAuthenticate, DataStoreAuthenticate>();
            Map.RegisterType<IdGenerator, GuidIdGenerator>();
            Map.RegisterType<IStore, FileStore>();
            Map.RegisterType<IFileDataService, DocumentFileDataService>();

            ViewDescriptorDeclarator.Declare<Area, AreaViewDescriptor>();
            ViewDescriptorDeclarator.Declare<Contact, ContactViewDescriptor>();
            ViewDescriptorDeclarator.Declare<Document, DocumentViewDescriptor>();
            ViewDescriptorDeclarator.Declare<IssueTracker, IssueTrackerViewDescriptor>();
            ViewDescriptorDeclarator.Declare<Person, PersonViewDescriptor>();
            ViewDescriptorDeclarator.Declare<User, UserViewDescriptor>();
        }

        public virtual void CreateInitialObjects()
        {
            var user = Map.ResolveType<IStore>().LoadAll<User>().FirstOrDefault(p => p.UserName == "Admin");

            if (user == null)
            {
                user = new User
                {
                    UserName = "Admin",
                    Password = "admin"
                };
			
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

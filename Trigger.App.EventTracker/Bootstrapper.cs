using System;
using System.Linq;
using Trigger.BCL.Common.Datastore;
using Trigger.BCL.Common.Model;
using Trigger.BCL.Common.Security;
using Trigger.BCL.Common.Services;
using Trigger.BCL.EventTracker;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.EventTracker.Services;
using Trigger.XForms;
using Trigger.XForms.Controllers;
using Trigger.XForms.Visuals;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Commands;

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
            Map.RegisterType<IStore, FileDataStore>();
            Map.RegisterType<IFileDataService, DocumentFileDataService>();

            RegisterCommands();

            RegisterViewDescriptors();
        }

        void RegisterCommands()
        {
            Map.RegisterType<ISaveObjectCommand, SaveObjectCommand>();
            Map.RegisterType<IDeleteObjectCommand, DeleteObjectCommand>();
            Map.RegisterType<ICloseWindowCommand, CloseWindowCommand>();
            Map.RegisterType<IOpenObjectCommand, OpenObjectCommand>();
            Map.RegisterType<IRefreshListViewCommand, RefreshListViewCommand>();
            Map.RegisterType<IRefreshDetailViewCommand, RefreshDetailViewCommand>();
            Map.RegisterType<IUpdateDocumentStoreCommand, UpdateDocumentStoreCommand>();
            Map.RegisterType<IAddFileCommand, AddFileCommand>();
            Map.RegisterType<ICreateObjectCommand, CreateObjectCommand>();
            Map.RegisterType<IApplicationExitCommand, ApplicationExitCommand>();
            Map.RegisterType<ILogOffCommand, LogOffCommand>();

            Map.RegisterType<ITimeTrackerCommand, TimeTrackerCommand>();
        }

        void RegisterViewDescriptors()
        {
            DetailViewDescriptorProvider.Declare<Area, AreaViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Contact, ContactViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Document, DocumentViewDescriptor>();
            DetailViewDescriptorProvider.Declare<IssueTracker, IssueTrackerViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Person, PersonViewDescriptor>();
            DetailViewDescriptorProvider.Declare<TimeTracker, TimeTrackerViewDescriptor>();
            DetailViewDescriptorProvider.Declare<User, UserViewDescriptor>();

            ListViewDescriptorProvider.Declare<Area, AreaListDescriptor>();
            ListViewDescriptorProvider.Declare<Contact, ContactListDescriptor>();
            ListViewDescriptorProvider.Declare<Document, DocumentListDescriptor>();
            ListViewDescriptorProvider.Declare<IssueTracker, IssueTrackerListDescriptor>();
            ListViewDescriptorProvider.Declare<Person, PersonListDescriptor>();
            ListViewDescriptorProvider.Declare<TimeTracker, TimeTrackerListDescriptor>();
            ListViewDescriptorProvider.Declare<User, UserListDescriptor>();
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
            ModelTypesDeclarator.DeclareModelTypes(DeclaredTypes());
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
    }
}

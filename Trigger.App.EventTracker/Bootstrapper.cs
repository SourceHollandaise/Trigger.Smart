using System.Linq;
using Trigger.BCL.Common.Datastore;
using Trigger.BCL.Common.Model;
using Trigger.BCL.Common.Security;
using Trigger.BCL.Common.Services;
using Trigger.BCL.EventTracker;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.EventTracker.Services;
using Trigger.XForms;
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

        public virtual void InitialiteSecurityProvider()
        {
            var provider = new SecurityInfoProvider();

            DependencyMapProvider.Instance.RegisterInstance<ISecurityInfoProvider>(provider);
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
            Map.RegisterType<IOpenObjectListViewCommand, OpenObjectListViewCommand>();
            Map.RegisterType<IRefreshListViewCommand, RefreshListViewCommand>();
            Map.RegisterType<IRefreshDetailViewCommand, RefreshDetailViewCommand>();
            Map.RegisterType<IUpdateDocumentStoreListViewCommand, UpdateDocumentStoreListViewCommand>();
            Map.RegisterType<IAddFileCommand, AddFileCommand>();
            Map.RegisterType<ICreateObjectListViewCommand, CreateObjectListViewCommand>();
            Map.RegisterType<IApplicationExitCommand, ApplicationExitCommand>();
            Map.RegisterType<ILogOffCommand, LogOffCommand>();
            Map.RegisterType<ITagCommand, TagCommand>();
            Map.RegisterType<ITimeTrackerCommand, TimeTrackerCommand>();
            Map.RegisterType<ISearchListViewCommand, SearchListViewCommand>();
            Map.RegisterType<ICurrentUserListViewCommand, CurrentUserListViewCommand>();
        }

        void RegisterViewDescriptors()
        {
            Map.RegisterType<IMainViewDescriptor, ApplicationMainViewDescriptor>();

            DetailViewDescriptorProvider.Declare<Area, AreaViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Contact, ContactViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Document, DocumentViewDescriptor>();
            DetailViewDescriptorProvider.Declare<ImageGallery, ImageGalleryViewDescriptor>();
            DetailViewDescriptorProvider.Declare<ImageItem, ImageItemViewDescriptor>();
            DetailViewDescriptorProvider.Declare<IssueTracker, IssueTrackerViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Person, PersonViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Tag, TagViewDescriptor>();
            DetailViewDescriptorProvider.Declare<TimeTracker, TimeTrackerViewDescriptor>();
            DetailViewDescriptorProvider.Declare<User, UserViewDescriptor>();

            ListViewDescriptorProvider.Declare<Area, AreaListDescriptor>();
            ListViewDescriptorProvider.Declare<Contact, ContactListDescriptor>();
            ListViewDescriptorProvider.Declare<Document, DocumentListDescriptor>();
            ListViewDescriptorProvider.Declare<ImageGallery, ImageGalleryListDescriptor>();
            ListViewDescriptorProvider.Declare<ImageItem, ImageItemListDescriptor>();
            ListViewDescriptorProvider.Declare<IssueTracker, IssueTrackerListDescriptor>();
            ListViewDescriptorProvider.Declare<Person, PersonListDescriptor>();
            ListViewDescriptorProvider.Declare<Tag, TagListDescriptor>();
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
    }
}

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
            var provider = new ApplicationSecurityInfoProvider();

            DependencyMapProvider.Instance.RegisterInstance<ISecurityInfoProvider>(provider);

            provider.UserName = "Jörg";
        }

        public virtual void RegisterDependencies()
        {
            Map.RegisterType<IViewTemplateConfiguration, ViewTemplateConfiguration>();
            Map.RegisterType<IAuthenticate, ApplicationDataStoreAuthenticate>();
            Map.RegisterType<IdGenerator, GuidIdGenerator>();
            Map.RegisterType<IStore, FileDataStore>();
            Map.RegisterType<IFileDataService, DocumentFileDataService>();

            RegisterCommands();

            RegisterViewDescriptors();
        }

        void RegisterCommands()
        {
            new XFormsBaseComands().Register();

            Map.RegisterType<ITrackTimeDetailViewCommand, TrackTimeDetailViewCommand>();
            Map.RegisterType<ILinkAreaWithUserDetailViewCommand, LinkAreaWithUserDetailViewCommand>();
        }

        void RegisterViewDescriptors()
        {
            Map.RegisterType<IMainViewDescriptor, ApplicationMainViewDescriptor>();

            DetailViewDescriptorProvider.Declare<AreaUser, AreaUserViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Area, AreaViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Contact, ContactViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Document, DocumentViewDescriptor>();
            DetailViewDescriptorProvider.Declare<ImageGallery, ImageGalleryViewDescriptor>();
            DetailViewDescriptorProvider.Declare<ImageItem, ImageItemViewDescriptor>();
            DetailViewDescriptorProvider.Declare<IssueTracker, IssueTrackerViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Person, PersonViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Tag, TagViewDescriptor>();
            DetailViewDescriptorProvider.Declare<TimeTracker, TimeTrackerViewDescriptor>();
            DetailViewDescriptorProvider.Declare<ApplicationUser, UserViewDescriptor>();

            ListViewDescriptorProvider.Declare<AreaUser, AreaUserListDescriptor>();
            ListViewDescriptorProvider.Declare<Area, AreaListDescriptor>();
            ListViewDescriptorProvider.Declare<Contact, ContactListDescriptor>();
            ListViewDescriptorProvider.Declare<Document, DocumentListDescriptor>();
            ListViewDescriptorProvider.Declare<ImageGallery, ImageGalleryListDescriptor>();
            ListViewDescriptorProvider.Declare<ImageItem, ImageItemListDescriptor>();
            ListViewDescriptorProvider.Declare<IssueTracker, IssueTrackerListDescriptor>();
            ListViewDescriptorProvider.Declare<Person, PersonListDescriptor>();
            ListViewDescriptorProvider.Declare<Tag, TagListDescriptor>();
            ListViewDescriptorProvider.Declare<TimeTracker, TimeTrackerListDescriptor>();
            ListViewDescriptorProvider.Declare<ApplicationUser, UserListDescriptor>();
        }

        public virtual void CreateInitialObjects()
        {
            var user = Map.ResolveType<IStore>().LoadAll<ApplicationUser>().FirstOrDefault(p => p.UserName == "Admin");

            if (user == null)
                user = new ApplicationUser();

            user.AllowAdministration = true;
            user.Password = "admin";
            user.Role = ApplicationUserRole.Administrator;
            user.UserName = "Admin";
            user.UserSex = Sex.Female;
            user.Save();
        }
    }
}

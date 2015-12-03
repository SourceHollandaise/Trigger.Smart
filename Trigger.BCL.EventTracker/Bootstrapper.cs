using System.Linq;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.EventTracker.Services;
using XForms.Commands;
using XForms.Design;
using XForms.Model;
using XForms.Platform;
using XForms.Security;
using XForms.Store;

namespace Trigger.BCL.EventTracker
{
    public class Bootstrapper : BootstrapperBase
    {
        public override void InitalizeDataStore()
        {
            var config = new StoreConfiguration();
            config.InitStore();

            Map.RegisterInstance<IStoreConfiguration>(config);
        }

        public override void InitialiteSecurityProvider()
        {
            var provider = new ApplicationSecurityInfoProvider();

            Map.RegisterInstance<ISecurityInfoProvider>(provider);
        }

        public override void RegisterDependencies()
        {
            base.RegisterDependencies();

            Map.RegisterType<IFileDataService, FileDataService>();
            Map.RegisterType<IAuthenticate, ApplicationDataStoreAuthenticate>();
        }

        public override void RegisterViewDescriptors()
        {
            Map.RegisterType<IMainViewDescriptor, ApplicationMainViewDescriptor>();
            Map.RegisterType<IMainViewTemplate, MainViewTemplate>();
            Map.RegisterType<IViewTemplateConfiguration, ViewTemplateDefaultConfiguration>();

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
            DetailViewDescriptorProvider.Declare<ApplicationUser, ApplicationUserViewDescriptor>();

            ListViewDescriptorProvider.Declare<ApplicationUser, ApplicationUserListDescriptor>();
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
            ListViewDescriptorProvider.Declare<ApplicationUser, ApplicationUserListDescriptor>();
        }

        public override void RegisterCommands()
        {
            base.RegisterCommands();

            Map.RegisterType<ITrackTimeDetailViewCommand, TrackTimeDetailViewCommand>();
            Map.RegisterType<ILinkAreaWithUserDetailViewCommand, LinkAreaWithUserDetailViewCommand>();
            Map.RegisterType<ICurrentUserDetailsCommand, CurrentUserDetailsViewCommand>();
            Map.RegisterType<IAddMultipleFilesDetailViewCommand, AddMultipleFilesDetailViewCommand>();
        }

        public override void CreateDefaultUser()
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

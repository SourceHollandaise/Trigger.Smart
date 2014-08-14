using System.Collections.Generic;
using XForms.Commands;
using XForms.Design;
using XForms.Model;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class ApplicationMainViewDescriptor : MainViewDescriptor
    {
        public ApplicationMainViewDescriptor()
        {
            RegisterCommands<ICurrentUserDetailsCommand>();
            RegisterCommands<IApplicationExitCommand>();

            NavigationGroups = new List<NavigationGroupItem>
            {
                new NavigationGroupItem("Favorites", 1)
                {
                    NavigationItems = new List<NavigationItemDescription>
                    {
                        new NavigationItemDescription(typeof(IssueTracker), "Open Issues", 1) { ImageName = "hand_thumb_down", ListView = new IssueTrackerListProgressDescriptor() },
                        new NavigationItemDescription(typeof(IssueTracker), "Done Issues", 2) { ImageName = "hand_thumb_up", ListView = new IssueTrackerListDoneDescriptor() },
                        new NavigationItemDescription(typeof(Area), "Areas", 3){ ImageName = "blueprint", ListView = new AreaListDescriptor() },
                        new NavigationItemDescription(typeof(Contact), "Contacts", 4){ ImageName = "address_book2", ListView = new ContactListDescriptor() },
                       
                    }
                },
                new NavigationGroupItem("Additional Data", 2)
                {
                    NavigationItems = new List<NavigationItemDescription>
                    {
                        new NavigationItemDescription(typeof(Person), "Person", 1){ ImageName = "businesspeople", ListView = new PersonListDescriptor() },
                        new NavigationItemDescription(typeof(Document), "Documents", 2){ ImageName = "folder3_document", ListView = new DocumentListDescriptor() },
                        new NavigationItemDescription(typeof(ImageGallery), "Image Galleries", 4){ ImageName = "photos", ListView = new ImageGalleryListDescriptor() },
                        new NavigationItemDescription(typeof(ImageItem), "Images", 5){ ImageName = "photo_landscape", ListView = new ImageItemListDescriptor() },
                    }
                },
                new NavigationGroupItem("Settings", 3)
                {
                    Visible = ApplicationQuery.CurrentUserIsAdministrator,
                    NavigationItems = new List<NavigationItemDescription>
                    {
                        new NavigationItemDescription(typeof(ApplicationUser), "Users", 1){ ImageName = "users2", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new ApplicationUserListDescriptor() },
                        new NavigationItemDescription(typeof(AreaUser), "Area and User", 1){ ImageName = "user_monitor", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new AreaUserListDescriptor() }
                    }
                }
            };
        }
    }
}

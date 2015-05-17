using System.Collections.Generic;
using XForms.Design;
using XForms.Model;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class ApplicationMainViewDescriptor : MainViewDescriptor
    {
        public ApplicationMainViewDescriptor()
        {
            NavigationGroups = new List<NavigationGroupItem>
            {
                new NavigationGroupItem("Favorites", 1)
                {
                    NavigationItems = new List<NavigationItemDescription>
                    {
                        new NavigationItemDescription(typeof(IssueTracker), "New Issues", 10) { ImageName = "signboard_for_sale", ListView = new IssueTrackerListOpenDescriptor() },
                        new NavigationItemDescription(typeof(IssueTracker), "Accepted Issues", 11) { ImageName = "signboard_open", ListView = new IssueTrackerListAcceptedDescriptor() },
                        new NavigationItemDescription(typeof(IssueTracker), "Done Issues", 12) { ImageName = "signboard_closed", ListView = new IssueTrackerListDoneDescriptor() },
                        new NavigationItemDescription(typeof(Area), "Areas", 13){ ImageName = "blueprint", ListView = new AreaListDescriptor() },
                        new NavigationItemDescription(typeof(Contact), "Contacts", 14){ ImageName = "address_book2", ListView = new ContactListDescriptor() },
                       
                    }
                },
                new NavigationGroupItem("Additional Data", 2)
                {
                    NavigationItems = new List<NavigationItemDescription>
                    {
                        new NavigationItemDescription(typeof(Person), "Person", 20){ ImageName = "businesspeople", ListView = new PersonListDescriptor() },
                        new NavigationItemDescription(typeof(Document), "Documents", 21){ ImageName = "folder3_document", ListView = new DocumentListDescriptor() },
                        new NavigationItemDescription(typeof(ImageGallery), "Image Galleries", 22){ ImageName = "photos", ListView = new ImageGalleryListDescriptor() },
                        new NavigationItemDescription(typeof(ImageItem), "Images", 23){ ImageName = "photo_landscape", ListView = new ImageItemListDescriptor() },
                    }
                },
                new NavigationGroupItem("Settings", 3)
                {
                    Visible = ApplicationQuery.CurrentUserIsAdministrator,
                    NavigationItems = new List<NavigationItemDescription>
                    {
                        new NavigationItemDescription(typeof(ApplicationUser), "Users", 30){ ImageName = "users2", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new ApplicationUserListDescriptor() },
                        new NavigationItemDescription(typeof(AreaUser), "Area and User", 31){ ImageName = "user_monitor", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new AreaUserListDescriptor() }
                    }
                }
            };
        }
    }
}

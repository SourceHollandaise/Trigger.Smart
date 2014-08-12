using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Design;
using XForms.Model;
using XForms.Commands;

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
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(IssueTracker), "Open Issues", 1) { ImageName = "note_edit", ListView = new IssueTrackerListProgressDescriptor() },
                        new NavigationItem(typeof(IssueTracker), "Done Issues", 1) { ImageName = "note_edit", ListView = new IssueTrackerListDoneDescriptor() },
                        new NavigationItem(typeof(Area), "Areas", 2){ ImageName = "application", ListView = new AreaListDescriptor() },
                        new NavigationItem(typeof(Document), "Documents", 3){ ImageName = "blog_post", ListView = new DocumentListDescriptor() }
                    }
                },
                new NavigationGroupItem("Personal Data", 2)
                {
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(Person), "Person", 1){ ImageName = "user", ListView = new PersonListDescriptor() },
                        new NavigationItem(typeof(Contact), "Contacts", 2){ ImageName = "user_comments", ListView = new ContactListDescriptor() },
                        new NavigationItem(typeof(ImageGallery), "Image Galleries", 4){ ImageName = "image_multi", ListView = new ImageGalleryListDescriptor() },
                        new NavigationItem(typeof(ImageItem), "Images", 5){ ImageName = "image", ListView = new ImageItemListDescriptor() },
                    }
                },
                new NavigationGroupItem("Settings", 3)
                {
                    Visible = ApplicationQuery.CurrentUserIsAdministrator,
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(ApplicationUser), "Users", 1){ ImageName = "community_users", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new ApplicationUserListDescriptor() },
                        new NavigationItem(typeof(AreaUser), "Area and User", 1){ ImageName = "community_users", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new AreaUserListDescriptor() }
                    }
                }
            };
        }
    }
}

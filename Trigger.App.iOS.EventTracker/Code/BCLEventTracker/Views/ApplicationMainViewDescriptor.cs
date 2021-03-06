using System.Collections.Generic;
using Trigger.XForms;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.Common.Model;

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
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(IssueTracker), "Issues", 1) { ImageName = "note_edit" },
                        new NavigationItem(typeof(Area), "Areas", 2){ ImageName = "application" },
                        new NavigationItem(typeof(Document), "Documents", 3){ ImageName = "blog_post" }
                    }
                },
                new NavigationGroupItem("Personal Data", 2)
                {
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(Person), "Person", 1){ ImageName = "user" },
                        new NavigationItem(typeof(Contact), "Contacts", 2){ ImageName = "user_comments" },
                        new NavigationItem(typeof(TimeTracker), "Tracked Times", 3){ ImageName = "clock" },
                        new NavigationItem(typeof(ImageGallery), "Image Galleries", 4){ ImageName = "image_multi" },
                    }
                },
                new NavigationGroupItem("Settings", 3)
                {
                    Visible = ApplicationQuery.CurrentUserIsAdministrator,
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(ApplicationUser), "Users", 1){ ImageName = "community_users", Visible = ApplicationQuery.CurrentUserIsAdministrator },
                        new NavigationItem(typeof(AreaUser), "Area and User", 1){ ImageName = "community_users", Visible = ApplicationQuery.CurrentUserIsAdministrator }
                    }
                }
            };
        }
    }
}

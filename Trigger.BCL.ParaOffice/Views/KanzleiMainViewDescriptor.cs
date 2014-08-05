using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.ParaOffice
{

    public class KanzleiMainViewDescriptor : MainViewDescriptor
    {
        public KanzleiMainViewDescriptor()
        {
            NavigationGroups = new List<NavigationGroupItem>
            {
                new NavigationGroupItem("Kanzlei", 1)
                {
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(Telefonat), "Telefonate", 2){ ImageName = "user_comment" },
                        new NavigationItem(typeof(Kontakt), "Kontakte", 3){ ImageName = "user_comments" },
                        new NavigationItem(typeof(Dokument), "Postbuch", 4){ ImageName = "blog_post" },
                        new NavigationItem(typeof(Termin), "Termine", 5){ ImageName = "calendar_date" },
                        new NavigationItem(typeof(Person), "Personen", 6){ ImageName = "user" }, 
                        new NavigationItem(typeof(Akt), "Akten", 7) { ImageName = "folder_full" },
                    }
                },
                new NavigationGroupItem("Stammdaten", 2)
                {
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(AktArt), "Aktarten", 1){ ImageName = "folder_process" },
                        new NavigationItem(typeof(AktPerson), "Aktpersonen", 2){ ImageName = "user_add", Visible = UserQuery.CurrentUserIsAdministrator },
                        new NavigationItem(typeof(SB), "Sachbearbeiter", 3){ ImageName = "user_accept", Visible = UserQuery.CurrentUserIsAdministrator },
                        new NavigationItem(typeof(User), "Benutzer", 4) { ImageName = "community_users", Visible = UserQuery.CurrentUserIsAdministrator }
                    }
                }
            };
        }
    }
}

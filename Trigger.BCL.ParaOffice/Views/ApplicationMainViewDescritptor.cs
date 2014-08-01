using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.ParaOffice
{
    public class ApplicationMainViewDescriptor : MainViewDescriptor
    {
        public ApplicationMainViewDescriptor()
        {
            NavigationGroups = new List<NavigationGroupItem>
            {
                new NavigationGroupItem("Favoriten", 1)
                {
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(Akt), "Akten", 1) { ImageName = "folder_full" },
                        new NavigationItem(typeof(Kontakt), "Kontakte", 2){ ImageName = "user_comments" },
                        new NavigationItem(typeof(Dokument), "Dokumente", 3){ ImageName = "blog_post" }
                    }
                },
                new NavigationGroupItem("Termine", 2)
                {
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(Termin), "Termine", 1){ ImageName = "calendar_date" },
                    }
                },
                new NavigationGroupItem("Stammdaten", 3)
                {
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(AktPerson), "Aktpersonen", 1){ ImageName = "user_add" },
                        new NavigationItem(typeof(AktArt), "Aktarten", 2){ ImageName = "folder_process" },
                        new NavigationItem(typeof(Person), "Personen", 3){ ImageName = "user" },  
                        new NavigationItem(typeof(SB), "Sachbearbeiter", 4){ ImageName = "user_accept" },
                        new NavigationItem(typeof(User), "Benutzer", 5) { ImageName = "community_users" }
                    }
                }
            };
        }
    }
}


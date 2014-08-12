using System.Collections.Generic;
using XForms.Design;
using XForms.Model;
using XForms.Commands;

namespace Trigger.BCL.ParaOffice
{

    public class ApplicationMainViewDescriptor : MainViewDescriptor
    {
        public ApplicationMainViewDescriptor()
        {
            RegisterCommands<ICurrentUserDetailsCommand>();
            RegisterCommands<ILogonCommand>();
            RegisterCommands<IApplicationExitCommand>();

            NavigationGroups = new List<NavigationGroupItem>
            {
                new NavigationGroupItem("Kanzlei", 1)
                {
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(Telefonat), "Telefonate", 2){ ImageName = "user_comment", ListView = new TelefonatListDescriptor() },
                        new NavigationItem(typeof(Kontakt), "Kontakte", 3){ ImageName = "user_comments", ListView = new KontaktListDescriptor() },
                        new NavigationItem(typeof(Dokument), "Postbuch", 4){ ImageName = "blog_post", ListView = new DokumentListDescriptor() },
                        new NavigationItem(typeof(Termin), "Termine", 5){ ImageName = "calendar_date", ListView = new TerminListDescriptor() },
                        new NavigationItem(typeof(Person), "Personen", 6){ ImageName = "user", ListView = new PersonListDescriptor() }, 
                        new NavigationItem(typeof(Akt), "Akten", 7) { ImageName = "folder_full", ListView = new AktListDescriptor() },
                    }
                },
                new NavigationGroupItem("Stammdaten", 2)
                {
                    NavigationItems = new List<NavigationItem>
                    {
                        new NavigationItem(typeof(AktArt), "Aktarten", 1){ ImageName = "folder_process", ListView = new AktArtListDescriptor() },
                        new NavigationItem(typeof(AktPerson), "Aktpersonen", 2){ ImageName = "user_add", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new AktPersonListDescriptor() },
                        new NavigationItem(typeof(SB), "Sachbearbeiter", 3){ ImageName = "user_accept", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new SBListDescriptor() },
                        new NavigationItem(typeof(User), "Benutzer", 4) { ImageName = "community_users", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new UserListDescriptor() }
                    }
                }
            };
        }
    }
}

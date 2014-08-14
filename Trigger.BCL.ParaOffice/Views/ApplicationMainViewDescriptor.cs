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
                    NavigationItems = new List<NavigationItemDescription>
                    {
                        new NavigationItemDescription(typeof(Telefonat), "Telefonate", 2){ ImageName = "user_mobilephone", ListView = new TelefonatListDescriptor() },
                        new NavigationItemDescription(typeof(Kontakt), "Kontakte", 3){ ImageName = "address_book2", ListView = new KontaktListDescriptor() },
                        new NavigationItemDescription(typeof(Dokument), "Postbuch", 4){ ImageName = "folder3_document", ListView = new DokumentListDescriptor() },
                        new NavigationItemDescription(typeof(Termin), "Termine", 5){ ImageName = "date_time", ListView = new TerminListDescriptor() },
                        new NavigationItemDescription(typeof(Person), "Personen", 6){ ImageName = "businesspeople", ListView = new PersonListDescriptor() }, 
                        new NavigationItemDescription(typeof(Akt), "Akten", 7) { ImageName = "folder2", ListView = new AktListDescriptor() },
                    }
                },
                new NavigationGroupItem("Stammdaten", 2)
                {
                    NavigationItems = new List<NavigationItemDescription>
                    {
                        new NavigationItemDescription(typeof(AktArt), "Aktarten", 1){ ImageName = "magazine_folder", ListView = new AktArtListDescriptor() },
                        new NavigationItemDescription(typeof(AktPerson), "Aktpersonen", 2){ ImageName = "id_card", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new AktPersonListDescriptor() },
                        new NavigationItemDescription(typeof(SB), "Sachbearbeiter", 3){ ImageName = "user_monitor", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new SBListDescriptor() },
                        new NavigationItemDescription(typeof(User), "Benutzer", 4) { ImageName = "users2", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new UserListDescriptor() }
                    }
                }
            };
        }
    }
}

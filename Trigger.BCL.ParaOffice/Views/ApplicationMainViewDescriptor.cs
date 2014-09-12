using System.Collections.Generic;
using XForms.Design;
using XForms.Model;

namespace Trigger.BCL.ParaOffice
{

    public class ApplicationMainViewDescriptor : MainViewDescriptor
    {
        public ApplicationMainViewDescriptor()
        { 
            NavigationGroups = new List<NavigationGroupItem>
            {
                new NavigationGroupItem("Kanzlei", 1)
                {
                    NavigationItems = new List<NavigationItemDescription>
                    {
                        new NavigationItemDescription(typeof(Telefonat), "Telefonate", 1){ ImageName = "user_mobilephone", ListView = new TelefonatListDescriptor() },
                        new NavigationItemDescription(typeof(Termin), "Termine", 2){ ImageName = "date_time", ListView = new TerminListDescriptor() },
                        new NavigationItemDescription(typeof(Kontakt), "Kontakte", 3){ ImageName = "address_book2", ListView = new KontaktListDescriptor() },
                        new NavigationItemDescription(typeof(Dokument), "Postbuch", 4){ ImageName = "folder3_document", ListView = new DokumentListDescriptor() },
                    }
                },
                new NavigationGroupItem("ERV", 2)
                {
                    NavigationItems = new List<NavigationItemDescription>
                    {
                        new NavigationItemDescription(typeof(ErvRueckverkehr), "Rückverkehr", 1) { ImageName = "folder2", ListView = new RueckverkehrListDescriptor() }
                    }
                },

                new NavigationGroupItem("Stammdaten", 3)
                {
                    NavigationItems = new List<NavigationItemDescription>
                    {
                        new NavigationItemDescription(typeof(Akt), "Akten", 1) { ImageName = "folder2", ListView = new AktListDescriptor() },
                        new NavigationItemDescription(typeof(AktArt), "Aktarten", 2){ ImageName = "magazine_folder", ListView = new AktArtListDescriptor() },
                        new NavigationItemDescription(typeof(AktPerson), "Aktpersonen", 3){ ImageName = "id_card", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new AktPersonListDescriptor() },
                        new NavigationItemDescription(typeof(Person), "Personen", 11){ ImageName = "businesspeople", ListView = new PersonListDescriptor() },
                        new NavigationItemDescription(typeof(SB), "Sachbearbeiter", 21){ ImageName = "user_monitor", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new SBListDescriptor() },
                        new NavigationItemDescription(typeof(User), "Benutzer", 2) { ImageName = "users2", Visible = ApplicationQuery.CurrentUserIsAdministrator, ListView = new UserListDescriptor() }
                    }
                }
            };
        }
    }
}

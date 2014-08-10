using XForms.Store;
using XForms.Model;

namespace Trigger.BCL.ParaOffice
{
    [System.ComponentModel.DefaultProperty("PersonAlias")]
    [System.ComponentModel.DisplayName("Kontakt")]
    [ImageName("user_comments")]
    public class Kontakt : ExportableBase
    {

        [System.ComponentModel.DisplayName("Person")]
        public string PersonAlias
        {
            get
            {
                return Person != null ? (Person.Vorname + " " + Person.Nachname) : null;
            }
        }

        Person person;

        [System.ComponentModel.DisplayName("Person")]
        [LinkedObject]
        public Person Person
        {
            get
            {
                return person;
            }
            set
            {
                if (Equals(person, value))
                    return;
                person = value;

                OnPropertyChanged();
            }
        }

        KontaktArt art;

        [System.ComponentModel.DisplayName("Art")]
        public KontaktArt Art
        {
            get
            {
                return art;
            }
            set
            {
                if (Equals(art, value))
                    return;
                art = value;

                OnPropertyChanged();
            }
        }

        string telefon;

        [System.ComponentModel.DisplayName("Telefon")]
        public string Telefon
        {
            get
            {
                return telefon;
            }
            set
            {
                if (Equals(telefon, value))
                    return;
                telefon = value;

                OnPropertyChanged();
            }
        }

        string mobilTelefon;

        [System.ComponentModel.DisplayName("Mobiltelefon")]
        public string MobilTelefon
        {
            get
            {
                return mobilTelefon;
            }
            set
            {
                if (Equals(mobilTelefon, value))
                    return;
                mobilTelefon = value;

                OnPropertyChanged();
            }
        }

        string email;

        [System.ComponentModel.DisplayName("E-Mail")]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (Equals(email, value))
                    return;
                email = value;

                OnPropertyChanged();
            }
        }

        string webSite;

        [System.ComponentModel.DisplayName("Web")]
        public string WebSite
        {
            get
            {
                return webSite;
            }
            set
            {
                if (Equals(webSite, value))
                    return;
                webSite = value;

                OnPropertyChanged();
            }
        }

        string organisation;

        [System.ComponentModel.DisplayName("Unternehmen")]
        public string Organisation
        {
            get
            {
                return organisation;
            }
            set
            {
                if (Equals(organisation, value))
                    return;
                organisation = value;

                OnPropertyChanged();
            }
        }
    }
}

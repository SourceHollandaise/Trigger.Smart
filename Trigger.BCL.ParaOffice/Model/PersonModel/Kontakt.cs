using Trigger.XStorable.Model;
using Trigger.XStorable.DataStore;

namespace Trigger.BCL.ParaOffice
{
    [System.ComponentModel.DefaultProperty("Telefon")]
    [System.ComponentModel.DisplayName("Kontakt")]
    [ViewCompact]
    [ViewNavigation]
    public class Kontakt : StorableBase
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
    }
}

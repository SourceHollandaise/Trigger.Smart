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
        [System.ComponentModel.DisplayName("Kontakt")]
        [FieldVisible(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.Append(string.Format("{0}", PersonAlias));
                if (!string.IsNullOrEmpty(Telefon))
                    sb.Append(string.Format(" {0}", Telefon));
                //if (!string.IsNullOrEmpty(MobilTelefon))
                //   sb.Append(string.Format(" {0}", MobilTelefon));
                if (!string.IsNullOrEmpty(Email))
                    sb.Append(string.Format(" {0}", Email));
                return sb.ToString();
            }
        }

        [System.ComponentModel.DisplayName("Person")]
        [FieldVisible(TargetView.ListOnly)]
        public string PersonAlias
        {
            get
            {
                return Person != null ? (Person.Vorname + " " + Person.Nachname) : null;
            }
        }

        Person person;

        [FieldGroup("Zuweisung", 1, 1)]
        [System.ComponentModel.DisplayName("Person")]
        [LinkedObject]
        [FieldVisible(TargetView.DetailOnly)]
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

        [FieldGroup("Zuweisung", 1, 2)]
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

        [FieldGroup("Kontaktdaten", 2, 1)]
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

        [FieldGroup("Kontaktdaten", 2, 2)]
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

        [FieldGroup("Kontaktdaten", 2, 3)]
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

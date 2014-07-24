using System;
using Trigger.XStorable.Model;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.ParaOffice
{
    [System.ComponentModel.DefaultProperty("Telefon")]
    [System.ComponentModel.DisplayName("Kontakt")]
    [CompactViewRepresentationAttribute]
    [MainViewItem]
    public class Kontakt : StorableBase
    {
        [System.ComponentModel.DisplayName("Kontakt")]
        [VisibleOnView(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("{0}", PersonAlias));
                if (!string.IsNullOrEmpty(Telefon))
                    sb.AppendLine(string.Format("Phone: {0}", Telefon));
                if (!string.IsNullOrEmpty(MobilTelefon))
                    sb.AppendLine(string.Format("Mobile: {0}", MobilTelefon));
                if (!string.IsNullOrEmpty(Email))
                    sb.AppendLine(string.Format("E-Mail: {0}", Email));
                return sb.ToString();
            }
        }

        [System.ComponentModel.DisplayName("Person")]
        [VisibleOnView(TargetView.ListOnly)]
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
        [VisibleOnView(TargetView.DetailOnly)]
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

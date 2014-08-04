using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Datastore;
using Trigger.XForms;
using System;

namespace Trigger.BCL.ParaOffice
{

    public enum TelefonatArt
    {
        Ein,
        Aus,
        Intern
    }

    public enum TelefonatStatus
    {
        OK,
        KN,
        RR,
        RWA
    }

    [System.ComponentModel.DefaultProperty("Teilnehmer")]
    [System.ComponentModel.DisplayName("Telefonat")]
    [ImageName("user_comment")]
    public class Telefonat : ExportableBase
    {

        public override void Initialize()
        {
            Beginn = DateTime.Now;
            SB1 = CurrentSBService.CurrentSB;
            Art = TelefonatArt.Ein;
            Status = TelefonatStatus.OK;
        }

        DateTime beginn;

        public DateTime Beginn
        {
            get
            {
                return beginn;
            }
            set
            {
                if (Equals(beginn, value))
                    return;
                beginn = value;

                OnPropertyChanged();
            }
        }

        string teilnehmer;

        public string Teilnehmer
        {
            get
            {
                return teilnehmer;
            }
            set
            {
                if (Equals(teilnehmer, value))
                    return;
                teilnehmer = value;

                OnPropertyChanged();
            }
        }

        string beschreibung;

        [FieldTextArea]
        public string Beschreibung
        {
            get
            {
                return beschreibung;
            }
            set
            {
                if (Equals(beschreibung, value))
                    return;
                beschreibung = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Akt")]
        [System.Runtime.Serialization.IgnoreDataMember]
        public string AktAlias
        {
            get
            {
                return Akt != null ? Akt.Bezeichnung : null;
            }
        }

        Akt akt;

        [LinkedObject]
        public Akt Akt
        {
            get
            {
                return akt;
            }
            set
            {
                if (Equals(akt, value))
                    return;
                akt = value;

                OnPropertyChanged();
            }
        }

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

        public string SB1Alias
        {
            get
            {
                return SB1 != null ? SB1.ID : null;
            }
        }

        SB sb1;

        [LinkedObject]
        public SB SB1
        {
            get
            {
                return sb1;
            }
            set
            {
                if (Equals(sb1, value))
                    return;
                sb1 = value;

                OnPropertyChanged();
            }
        }

        public string SB2Alias
        {
            get
            {
                return SB2 != null ? SB2.ID : null;
            }
        }


        SB sb2;

        [LinkedObject]
        public SB SB2
        {
            get
            {
                return sb2;
            }
            set
            {
                if (Equals(sb2, value))
                    return;
                sb2 = value;

                OnPropertyChanged();
            }
        }

        TelefonatArt art;

        public TelefonatArt Art
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

        TelefonatStatus status;

        public TelefonatStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                if (Equals(status, value))
                    return;
                status = value;

                OnPropertyChanged();
            }
        }

        string nummer;

        public string Nummer
        {
            get
            {
                return nummer;
            }
            set
            {
                if (Equals(nummer, value))
                    return;
                nummer = value;

                OnPropertyChanged();
            }
        }
    }
}
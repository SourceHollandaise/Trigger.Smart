using System.Linq;
using Trigger.XForms;
using Trigger.XStorable.DataStore;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Akt")]
    [System.ComponentModel.DisplayName("Person zu Akt")]
    [ImageName("user_add")]
    public class AktPerson : ExportableBase
    {
        public override void Initialize()
        {
            base.Initialize();

            Streitgenosse = true;
            Partei = Partei.Partei1;
        }

        public override void Save()
        {
            if (Akt != null && Person != null)
            {
                var aktPerson = Store.LoadAll<AktPerson>().FirstOrDefault(p => p.Akt != null && p.Akt.MappingId.Equals(Akt.MappingId) && p.Person != null && p.Person.MappingId.Equals(Person.MappingId));
                if (aktPerson != null)
                    return;

                base.Save();
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

        [System.ComponentModel.DisplayName("Vertreter")]
        public string VertreterAlias
        {
            get
            {
                return Person != null && Vertreter != null ? Vertreter.Vorname + " " + Vertreter.Nachname : null;
            }
        }

        Person vertreter;

        [System.ComponentModel.DisplayName("Vertreter")]
        [LinkedObject]
        public Person Vertreter
        {
            get
            {
                return vertreter;
            }
            set
            {
                if (Equals(vertreter, value))
                    return;
                vertreter = value;

                OnPropertyChanged();
            }
        }

        Partei partei;

        [System.ComponentModel.DisplayName("Klient/Gegner")]
        public Partei Partei
        {
            get
            {
                return partei;
            }
            set
            {
                if (Equals(partei, value))
                    return;
                partei = value;

                OnPropertyChanged();
            }
        }

        bool streitgenosse;

        public bool Streitgenosse
        {
            get
            {
                return streitgenosse;
            }
            set
            {
                if (Equals(streitgenosse, value))
                    return;
                streitgenosse = value;

                OnPropertyChanged();
            }
        }

        int reihung;

        public int Reihung
        {
            get
            {
                return reihung;
            }
            set
            {
                if (Equals(reihung, value))
                    return;
                reihung = value;

                OnPropertyChanged();
            }
        }
    }
}
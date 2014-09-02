using System;
using System.Linq;
using XForms.Model;
using XForms.Store;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("GetRepresentation")]
    [System.ComponentModel.DisplayName("Termin")]
    [ImageName("calendar_date")]
    public class Termin : ExportableBase
    {
        public override string GetSearchString()
        {
            return GetRepresentation + Beschreibung;
        }

        [System.ComponentModel.DisplayName("Termin")]
        public override string GetRepresentation
        {
            get
            {
                return SBAlias + " - " + Betreff + " " + BeginnAlias + " " + DauerAlias;
            }
        }

        public override void Initialize()
        {
            Art = TerminArt.Termin;
            Erzeuger = ApplicationModelQuery.CurrentSB;
            Ort = "Kanzlei";
            Beginn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour + 1, 0, 0);
        }

        TerminArt art;

        public TerminArt Art
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

        public string BeginnAlias
        {
            get
            {
                return Beginn.ToShortDateString();
            }
        }

        public string DauerAlias
        {
            get
            {
                return Beginn.ToShortTimeString() + " - " + Ende.ToShortTimeString();
            }
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

                Ende = Beginn.AddHours(1);
            }
        }

        DateTime ende;

        public DateTime Ende
        {
            get
            {
                return ende;
            }
            set
            {
                if (Equals(ende, value))
                    return;
                ende = value;

                OnPropertyChanged();
            }
        }

        string betreff;

        public string Betreff
        {
            get
            {
                return betreff;
            }
            set
            {
                if (Equals(betreff, value))
                    return;
                betreff = value;

                OnPropertyChanged();
            }
        }

        string ort;

        public string Ort
        {
            get
            {
                return ort;
            }
            set
            {
                if (Equals(ort, value))
                    return;
                ort = value;

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

        public string SBAlias
        {
            get
            {
                return SB != null ? SB.ID : null;
            }
        }

        SB sb;

        public SB SB
        {
            get
            {
                return sb;
            }
            set
            {
                if (Equals(sb, value))
                    return;
                sb = value;

                OnPropertyChanged();
            }
        }

        SB erzeuger;

        public SB Erzeuger
        {
            get
            {
                return erzeuger;
            }
            set
            {
                if (Equals(erzeuger, value))
                    return;
                erzeuger = value;

                OnPropertyChanged();
            }
        }

        bool ok;

        public bool OK
        {
            get
            {
                return ok;
            }
            set
            {
                if (Equals(ok, value))
                    return;
                ok = value;

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

                KlientGegner = GetKlientGegner();
            }
        }

        string klientGegner;

        public string KlientGegner
        {
            get
            {
                return klientGegner;
            }
            set
            {
                if (Equals(klientGegner, value))
                    return;
                klientGegner = value;

                OnPropertyChanged();
            }
        }

        string GetKlientGegner()
        {
            var aktPersonKlient = Store.LoadAll<AktPerson>()
                .FirstOrDefault(p => p.Akt != null && p.Akt.MappingId.Equals(Akt.MappingId) && p.Partei == Partei.Partei1 && p.Reihung == 1);

            var aktPersonGegner = Store.LoadAll<AktPerson>()
                .FirstOrDefault(p => p.Akt != null && p.Akt.MappingId.Equals(Akt.MappingId) && p.Partei == Partei.Partei2 && p.Reihung == 1);

            if (aktPersonKlient != null && aktPersonGegner != null)
                return aktPersonKlient.Person.PersonenName + "/" + aktPersonGegner.Person.PersonenName;

            if (aktPersonKlient != null && aktPersonGegner == null)
                return aktPersonKlient.Person.PersonenName;

            if (aktPersonKlient == null && aktPersonGegner != null)
                return aktPersonGegner.Person.PersonenName;

            return null;
        }
    }
}

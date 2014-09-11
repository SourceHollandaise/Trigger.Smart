using System;
using XForms.Store;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("SparteAlias")]
    [System.ComponentModel.DisplayName("Leistung")]
    public class Leistung : ExportableBase
    {
        public override string GetSearchString()
        {
            return SparteAlias + RAAlias + SKAlias;
        }

        public override string GetRepresentation
        {
            get
            {
                return RAAlias + " " + SparteAlias + " " + Datum.ToShortDateString();
            }
        }

        public override void Initialize()
        {
            RA = ApplicationModelQuery.CurrentSB;
            Datum = DateTime.Today;
        }

        public string RAAlias
        {
            get
            {
                return RA != null ? RA.ID : null;
            }
        }

        public string SparteAlias
        {
            get
            {
                return Sparte != null ? Sparte.IdText : null;
            }
        }

        LeistungKatalog sparte;

        [LinkedObject]
        public LeistungKatalog Sparte
        {
            get
            {
                return sparte;
            }
            set
            {
                if (Equals(sparte, value))
                    return;
                sparte = value;

                OnPropertyChanged();
            }
        }

        SB ra;

        [LinkedObject]
        public SB RA
        {
            get
            {
                return ra;
            }
            set
            {
                if (Equals(ra, value))
                    return;
                ra = value;

                OnPropertyChanged();
            }
        }

        public string SKAlias
        {
            get
            {
                return SK != null ? SK.ID : null;
            }
        }

        SB sk;

        [LinkedObject]
        public SB SK
        {
            get
            {
                return sk;
            }
            set
            {
                if (Equals(sk, value))
                    return;
                sk = value;

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

        TimeSpan rAZeit;

        public TimeSpan RAZeit
        {
            get
            {
                return rAZeit;
            }
            set
            {
                if (Equals(rAZeit, value))
                    return;
                rAZeit = value;

                OnPropertyChanged();
            }
        }

        TimeSpan sKZeit;

        public TimeSpan SKZeit
        {
            get
            {
                return sKZeit;
            }
            set
            {
                if (Equals(sKZeit, value))
                    return;
                sKZeit = value;

                OnPropertyChanged();
            }
        }

        DateTime datum;

        public DateTime Datum
        {
            get
            {
                return datum;
            }
            set
            {
                if (Equals(datum, value))
                    return;
                datum = value;

                OnPropertyChanged();
            }
        }
    }
}

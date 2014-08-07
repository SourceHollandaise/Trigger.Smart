using System;
using XForms.Store;
using XForms.Model;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Subject")]
    [System.ComponentModel.DisplayName("Dokument")]
    [ImageName("blog_post")]
    public class Dokument : ExportableBase, IFileData
    {
        public override void Initialize()
        {
            SK = CurrentSBService.CurrentSB;
        }

        string subject;

        [System.ComponentModel.DisplayName("Bezeichnung")]
        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                if (Equals(subject, value))
                    return;
                subject = value;

                OnPropertyChanged();
            }
        }

        public string RAAlias
        {
            get
            {
                return RA != null ? RA.ID : null;
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

                if (RA == null && akt != null)
                    RA = akt.SB1;
            }
        }

        DokumentArt art;

        public DokumentArt Art
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

        DokumentMedium medium;

        public DokumentMedium Medium
        {
            get
            {
                return medium;
            }
            set
            {
                if (Equals(medium, value))
                    return;
                medium = value;

                OnPropertyChanged();
            }
        }


        DokumentStatus status;

        public DokumentStatus Status
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

        string bemerkung;

        [FieldTextArea]
        public string Bemerkung
        {
            get
            {
                return bemerkung;
            }
            set
            {
                if (Equals(bemerkung, value))
                    return;
                bemerkung = value;

                OnPropertyChanged();
            }
        }

        string fileName;

        [System.ComponentModel.DisplayName("Datei")]
        [FieldFileData]
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                if (Equals(fileName, value))
                    return;
                fileName = value;

                OnPropertyChanged();
            }
        }

        DateTime anlageDatum;

        [System.ComponentModel.DisplayName("Anlage")]
        public DateTime AnlageDatum
        {
            get
            {
                return anlageDatum;
            }
            set
            {
                if (Equals(anlageDatum, value))
                    return;
                anlageDatum = value;

                OnPropertyChanged();
            }
        }

        DateTime? erledigungDatum;

        [System.ComponentModel.DisplayName("Erledigung")]
        public DateTime? ErledigungDatum
        {
            get
            {
                return erledigungDatum;
            }
            set
            {
                if (Equals(erledigungDatum, value))
                    return;
                erledigungDatum = value;

                OnPropertyChanged();
            }
        }

    }
}

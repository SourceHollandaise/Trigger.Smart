using System;
using Trigger.XStorable.Model;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.DataStore;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Bezeichnung")]
    [System.ComponentModel.DisplayName("Akt")]
    [CompactViewRepresentation]
    [MainViewItem]
    public class Akt : StorableBase
    {
        [System.ComponentModel.DisplayName("Akt")]
        [VisibleOnView(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("{0} - {1}", Bezeichnung, AktArtAlias));
                sb.AppendLine(string.Format("RA: {0} - SK: {1}", SB1 != null ? SB1.ID : "Kein", SB2 != null ? SB2.ID : "Kein"));
                if (LinkedDokumente.Any())
                    sb.AppendLine(string.Format("Dokumente: {0}", LinkedDokumente.Count()));
                return sb.ToString();
            }
        }

        public override void Initialize()
        {
            AnlageDatum = DateTime.Now;

            SB2 = CurrentSBService.CurrentSB;
        }

        string bezeichnung;

        public string Bezeichnung
        {
            get
            {
                return bezeichnung;
            }
            set
            {
                if (Equals(bezeichnung, value))
                    return;
                bezeichnung = value;

                OnPropertyChanged();
            }
        }

        string bemerkung;

        [VisibleOnView(TargetView.DetailOnly)]
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

        string archivZahl;

        [System.ComponentModel.DisplayName("Archivzahl")]
        [VisibleOnView(TargetView.DetailOnly)]
        public string ArchivZahl
        {
            get
            {
                return archivZahl;
            }
            set
            {
                if (Equals(archivZahl, value))
                    return;
                archivZahl = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Aktart")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [VisibleOnView(TargetView.ListOnly)]
        public string AktArtAlias
        {
            get
            {
                return AktArt != null ? AktArt.Art : null;
            }
        }

        AktArt aktArt;

        [System.ComponentModel.DisplayName("Aktart")]
        [LinkedObject]
        [VisibleOnView(TargetView.DetailOnly)]
        public AktArt AktArt
        {
            get
            {
                return aktArt;
            }
            set
            {
                if (Equals(aktArt, value))
                    return;
                aktArt = value;

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

        SB sb3;

        [LinkedObject]
        [VisibleOnView(TargetView.DetailOnly)]
        public SB SB3
        {
            get
            {
                return sb3;
            }
            set
            {
                if (Equals(sb3, value))
                    return;
                sb3 = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Dokumente zu Akt")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Dokument))]
        public IEnumerable<Dokument> LinkedDokumente
        {
            get
            {
                return Store.LoadAll<Dokument>().Where(p => p.Akt != null && p.Akt.MappingId.Equals(MappingId));
            }
        }

        [System.ComponentModel.DisplayName("Personen zu Akt")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Person))]
        public IEnumerable<Person> LinkedPersonen
        {
            get
            {
                return Store.LoadAll<AktPerson>().Where(p => p.Akt != null && p.Akt.MappingId.Equals(MappingId)).Select(p => p.Person);
            }
        }

        [System.ComponentModel.DisplayName("Termine zu Akt")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Termin))]
        public IEnumerable<Termin> LinkedTermine
        {
            get
            {
                return Store.LoadAll<Termin>().Where(p => p.Akt != null && p.Akt.MappingId.Equals(MappingId));
            }
        }
    }
}


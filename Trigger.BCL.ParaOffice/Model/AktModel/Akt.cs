using System;
using System.Collections.Generic;
using System.Linq;
using XForms.Model;
using XForms.Store;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Bezeichnung")]
    [System.ComponentModel.DisplayName("Akt")]
    [ImageName("folder2")]
    public class Akt : ExportableBase
    {
        public override string GetRepresentation
        {
            get
            {
                return Bezeichnung + " " + AktArtAlias;
            }
        }

        public override string GetSearchString()
        {
            return Bezeichnung + Bemerkung + AktArtAlias;
        }

        public override void Initialize()
        {
            AnlageDatum = DateTime.Now;
            SB2 = ApplicationModelQuery.CurrentSB;
            AktArt = Store.LoadAll<AktArt>().FirstOrDefault(p => p.Art.Equals("Zivil"));
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

        string archivZahl;

        [System.ComponentModel.DisplayName("Archivzahl")]
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

        SB sb3;

        [LinkedObject]
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

        [System.ComponentModel.DisplayName("Kontakte zu Akt")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Kontakt))]
        public IEnumerable<Kontakt> LinkedKontakte
        {
            get
            {
                return Store.LoadAll<AktPerson>().Where(p => p.Akt != null && p.Akt.MappingId.Equals(MappingId))
                    .SelectMany(p => p.Person.LinkedKontakte);
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

        [System.ComponentModel.DisplayName("Termine zu Akt")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Termin))]
        public IEnumerable<Termin> LinkedTermine
        {
            get
            {
                return Store.LoadAll<Termin>().Where(p => p.Akt != null && p.Akt.MappingId.Equals(MappingId))
                    .OrderByDescending(p => p.Beginn);
            }
        }

        [System.ComponentModel.DisplayName("Personen zu Akt")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(AktPerson))]
        public IEnumerable<AktPerson> LinkedPersonen
        {
            get
            {
                return Store.LoadAll<AktPerson>().Where(p => p.Akt != null && p.Akt.MappingId.Equals(MappingId))
                    .OrderBy(p => p.Partei).ThenBy(p => p.Reihung);
            }
        }

        [System.ComponentModel.DisplayName("Leistungen zu Akt")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Leistung))]
        public IEnumerable<Leistung> LinkedLeistungen
        {
            get
            {
                return Store.LoadAll<Leistung>().Where(p => p.Akt != null && p.Akt.MappingId.Equals(MappingId))
                    .OrderBy(p => p.Datum);
            }
        }
    }
}


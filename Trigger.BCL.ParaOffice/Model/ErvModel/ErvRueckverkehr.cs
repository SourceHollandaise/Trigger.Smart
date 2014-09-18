using System;
using XForms.Store;
using System.Collections.Generic;
using System.Linq;
using XForms.Model;

namespace Trigger.BCL.ParaOffice
{
    [System.ComponentModel.DefaultProperty("ErvCode")]
    [System.ComponentModel.DisplayName("ERV-Rückverkehr")]
    [ImageName("folder2")]
    public class ErvRueckverkehr : ExportableBase
    {
        const string RepresentationSeparator = " - ";

        public override string GetSearchString()
        {
            return ErvCode + Art + EmpfangDatum.ToLongDateString() + HinterlegungDatum.ToLongDateString();
        }

        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();

                //sb.Append(ErvCode);
                //sb.Append(RepresentationSeparator);
                sb.Append(Art);
                sb.Append(RepresentationSeparator);

                if (!string.IsNullOrWhiteSpace(AktenZeichen))
                {
                    sb.Append(RepresentationSeparator);
                    sb.Append("Akzenzeichen ");
                    sb.Append(AktenZeichen);
                }

                sb.Append("Hinterlegt am ");
                sb.Append(HinterlegungDatum.ToShortDateString() + " " + HinterlegungDatum.ToShortTimeString());

                if (AnzahlDokumentAnhang > 0)
                {
                    sb.Append(RepresentationSeparator);
                    sb.Append(string.Format(" {0} Dokumente", AnzahlDokumentAnhang));
                }

                return sb.ToString();
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        string ervCode;

        public string ErvCode
        {
            get
            {
                return ervCode;
            }
            set
            {
                if (Equals(ervCode, value))
                    return;
                ervCode = value;

                OnPropertyChanged();
            }
        }

        string messageId;

        public string MessageId
        {
            get
            {
                return messageId;
            }
            set
            {
                if (Equals(messageId, value))
                    return;
                messageId = value;

                OnPropertyChanged();
            }
        }

        string eingabeMessageId;

        public string EingabeMessageId
        {
            get
            {
                return eingabeMessageId;
            }
            set
            {
                if (Equals(eingabeMessageId, value))
                    return;
                eingabeMessageId = value;

                OnPropertyChanged();
            }
        }

        string gericht;

        public string Gericht
        {
            get
            {
                return gericht;
            }
            set
            {
                if (Equals(gericht, value))
                    return;
                gericht = value;

                OnPropertyChanged();
            }
        }

        string art;

        //TODO: Enum?
        public string Art
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

        string aktenZeichen;

        public string AktenZeichen
        {
            get
            {
                return aktenZeichen;
            }
            set
            {
                if (Equals(aktenZeichen, value))
                    return;
                aktenZeichen = value;

                OnPropertyChanged();
            }
        }

        public string ParteienAlias
        {
            get
            {
                return Partei1 + "/" + Partei2;
            }
        }

        string partei1;

        public string Partei1
        {
            get
            {
                return partei1;
            }
            set
            {
                if (Equals(partei1, value))
                    return;
                partei1 = value;

                OnPropertyChanged();
            }
        }

        string partei2;

        public string Partei2
        {
            get
            {
                return partei2;
            }
            set
            {
                if (Equals(partei2, value))
                    return;
                partei2 = value;

                OnPropertyChanged();
            }
        }

        int anzahlDokumentAnhang;

        public int AnzahlDokumentAnhang
        {
            get
            {
                return anzahlDokumentAnhang;
            }
            set
            {
                if (Equals(anzahlDokumentAnhang, value))
                    return;
                anzahlDokumentAnhang = value;

                OnPropertyChanged();
            }
        }

        DateTime empfangDatum;

        public DateTime EmpfangDatum
        {
            get
            {
                return empfangDatum;
            }
            set
            {
                if (Equals(empfangDatum, value))
                    return;
                empfangDatum = value;

                OnPropertyChanged();
            }
        }

        DateTime hinterlegungDatum;

        public DateTime HinterlegungDatum
        {
            get
            {
                return hinterlegungDatum;
            }
            set
            {
                if (Equals(hinterlegungDatum, value))
                    return;
                hinterlegungDatum = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Dokumente zu Rückverkehr")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(ErvRueckverkehrDokument))]
        public IEnumerable<ErvRueckverkehrDokument> LinkedRueckverkehrDokumente
        {
            get
            {
                return Store.LoadAll<ErvRueckverkehrDokument>().Where(p => p.Rueckverkehr != null && p.Rueckverkehr.MappingId.Equals(MappingId));
            }
        }
    }
}

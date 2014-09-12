using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{
    public class RueckverkehrViewDescriptor : DetailViewDescriptor<ErvRueckverkehr>
    {
        public RueckverkehrViewDescriptor()
        {
            IsTaggable = false;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Rückverkehr", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.ErvCode), 1){ LabelText = "ERV-Code" },
                        new ViewItemDescription(Fields.GetName(m => m.Art), 2){ LabelText = "Art"  },
                        new ViewItemDescription(Fields.GetName(m => m.Gericht), 3){ LabelText = "Gericht" },
                        new ViewItemDescription(Fields.GetName(m => m.AktenZeichen), 4){ LabelText = "Aktenzeichen" },
                        new ViewItemDescription(Fields.GetName(m => m.ParteienAlias), 5){ LabelText = "Parteien" },
                        new ViewItemDescription(Fields.GetName(m => m.EmpfangDatum), 6){ LabelText = "Empfangen" },
                        new ViewItemDescription(Fields.GetName(m => m.HinterlegungDatum), 7){ LabelText = "Hinterlegt" },
                    }
                },
                new GroupItemDescription("Dokumente", 1)
                {
                    Fill = true,
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.LinkedRueckverkehrDokumente), 1){ LabelText = "Dokumente", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                    }
                }
            };
        }
    }
}

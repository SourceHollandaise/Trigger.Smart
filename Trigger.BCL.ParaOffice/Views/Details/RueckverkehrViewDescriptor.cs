using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{
    public class RueckverkehrViewDescriptor : DetailViewDescriptor<Rueckverkehr>
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
                        new ViewItemDescription(Fields.GetName(m => m.ErvCode), 1){ LabelText = "ERV-Code", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.Art), 2){ LabelText = "Art", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.EmpfangDatum), 3){ LabelText = "Empfangen", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.HinterlegungDatum), 4){ LabelText = "Hinterlegt", Required = true },
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

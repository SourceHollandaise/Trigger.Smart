using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class RueckverkehrDokumentViewDescriptor : DetailViewDescriptor<RueckverkehrDokument>
    {
        public RueckverkehrDokumentViewDescriptor()
        {
            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Dokument", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Dokument", Required = true  },
                       
                    }
                },
                new GroupItemDescription(null, 1)
                {
                    Fill = true,
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.FileName), 1){ LabelText = "Vorschau", ShowLabel = false, Fill = true },
                    }
                }
            };
        }
    }
    
}

using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{
    public class KontaktViewDescriptor : DetailViewDescriptor<Kontakt>
    {
        public KontaktViewDescriptor()
        {
//            TabItemDescriptions = new List<TabItemDescription>
//            {
//                new TabItemDescription("Kontakt", 1)
//                {
            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Person", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Person), 1){ LabelText = "Person", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.Art), 2){ LabelText = "Art", Required = true },
                    }
                },
                new GroupItemDescription("Details", 2)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.MobilTelefon), 1){ LabelText = "Mobil" },
                        new ViewItemDescription(Fields.GetName(m => m.Telefon), 2){ LabelText = "Telefon" },
                        new ViewItemDescription(Fields.GetName(m => m.Email), 3){ LabelText = "E-Mail" },
                    }
                },
//                    }
//                }
            };
        }
    }
}

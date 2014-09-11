using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{
    public class KontaktViewDescriptor : DetailViewDescriptor<Kontakt>
    {
        public KontaktViewDescriptor()
        {
            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Kontaktdaten", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Person), 1){ LabelText = "Person", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.Organisation), 2){ LabelText = "Unternehmen", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.MobilTelefon), 3){ LabelText = "Mobil" },
                        new ViewItemDescription(Fields.GetName(m => m.Telefon), 4){ LabelText = "Telefon" },
                        new ViewItemDescription(Fields.GetName(m => m.Email), 5){ LabelText = "E-Mail" },
                        new ViewItemDescription(Fields.GetName(m => m.WebSite), 6){ LabelText = "Web" },
                    }
                },
            };
        }
    }
}

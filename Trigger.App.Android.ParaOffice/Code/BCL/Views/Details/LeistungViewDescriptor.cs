using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{
    public class LeistungViewDescriptor : DetailViewDescriptor<Leistung>
    {
        public LeistungViewDescriptor()
        {
            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Leistung", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Datum), 1){ LabelText = "Datum" },
                        new ViewItemDescription(Fields.GetName(m => m.Akt), 2){ LabelText = "Akt" },
                        new ViewItemDescription(Fields.GetName(m => m.Sparte), 3){ LabelText = "Sparte" },
                    }
                },
                new GroupItemDescription("RA", 2)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.RA), 1){ LabelText = "RA" },
                        new ViewItemDescription(Fields.GetName(m => m.RAZeit), 2){ LabelText = "Zeit" },
                        new ViewItemDescription(Fields.GetName(m => m.RAVerdienst), 3){ LabelText = "Verdienst" },
                    }
                },
                new GroupItemDescription("SK", 3)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.SK), 1){ LabelText = "SK" },
                        new ViewItemDescription(Fields.GetName(m => m.SKZeit), 2){ LabelText = "Zeit" },
                        new ViewItemDescription(Fields.GetName(m => m.SKVerdienst), 3){ LabelText = "Verdienst" },
                    }
                }
            };
        }
    }
}

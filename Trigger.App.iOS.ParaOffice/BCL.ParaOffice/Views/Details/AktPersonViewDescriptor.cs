using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class AktPersonViewDescriptor : DetailViewDescriptor<AktPerson>
    {
        public AktPersonViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Aktperson", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Person", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Person), 1){ LabelText = "Person", Required = true },
                                new ViewItemDescription(Fields.GetName(m => m.Vertreter), 1){ LabelText = "Vertreten durch" },
                                new ViewItemDescription(Fields.GetName(m => m.Akt), 2){ LabelText = "Akt", Required = true  },
                            }
                        },
                        new GroupItemDescription("Details", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Partei), 1){ LabelText = "Partei", Required = true  },
                                new ViewItemDescription(Fields.GetName(m => m.Streitgenosse), 2){ LabelText = "Streitgenosse" },
                                new ViewItemDescription(Fields.GetName(m => m.Reihung), 3){ LabelText = "Reihung" },
                            }
                        },
                    }
                }
            };
        }
    }
}

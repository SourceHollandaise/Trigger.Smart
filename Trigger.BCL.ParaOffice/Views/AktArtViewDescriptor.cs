using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{
    public class AktArtViewDescriptor : ViewDescriptor<AktArt>
    {
        public AktArtViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Aktart", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Art", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Art), 1){ LabelText = "Art" },
                                new ViewItemDescription(Fields.GetName(m => m.Bemerkung), 2){ LabelText = "Bemerkung" },
                            }
                        },
                        new GroupItemDescription("Akten", 2)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedAkten), 1){ LabelText = "Akten", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        },
                    }
                }
            };
        }
    }
    
}

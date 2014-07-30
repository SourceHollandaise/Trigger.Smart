using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{

    public class SBViewDescriptor : ViewDescriptor<SB>
    {
        public SBViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Sachbearbeiter", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Art", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.ID), 1){ LabelText = "SB-Kürzel" },
                                new ViewItemDescription(Fields.GetName(m => m.User), 2){ LabelText = "Benutzer" },
                            }
                        },
                        new GroupItemDescription("Termine", 2)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedTermine), 1){ LabelText = "Termine", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        },
                    }
                }
            };
        }
    }
}

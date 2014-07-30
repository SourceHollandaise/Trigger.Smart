using Trigger.XForms;
using System.Collections.Generic;
using System;
using Trigger.BCL.EventTracker.Model;


namespace Trigger.BCL.EventTracker
{
    public class AreaViewDescriptor : ViewDescriptor<Area>
    {
        public AreaViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Area", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Name", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Name), 1){ LabelText = "Name" },
                                new ViewItemDescription(Fields.GetName(m => m.Description), 2){ LabelText = "Description" },
                            }
                        },
                        new GroupItemDescription("Links", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedDocuments), 1){ LabelText = "Documents", ShowLabel = false },
                                new ViewItemDescription(Fields.GetName(m => m.LinkedIssues), 1){ LabelText = "Issues", ShowLabel = false }
                            }
                        }
                    }
                }
            };
        }
    }
}

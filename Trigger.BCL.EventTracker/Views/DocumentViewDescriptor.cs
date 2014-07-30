using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class DocumentViewDescriptor : ViewDescriptor<Document>
    {
        public DocumentViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Document", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Details", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Name" },
                                new ViewItemDescription(Fields.GetName(m => m.Description), 2){ LabelText = "Description" },
                                new ViewItemDescription(Fields.GetName(m => m.User), 3){ LabelText = "From" },
                            }
                        },
                        new GroupItemDescription("Links", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Area), 1){ LabelText = "Area" },
                                new ViewItemDescription(Fields.GetName(m => m.Issue), 2){ LabelText = "Issue" },
                            }
                        }
                    }
                },
                new TabItemDescription("Preview", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.FileName), 1){ LabelText = "Preview file", ShowLabel = false, Fill = true },
                            }
                        }
                    }
                }
            };
        }
    }
}

using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class DocumentViewDescriptor : ViewDescriptor
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
                                new ViewItemDescription("Subject", 1){ LabelText = "Name" },
                                new ViewItemDescription("Description", 2){ LabelText = "Description" },
                                new ViewItemDescription("User", 3){ LabelText = "From" },
                            }
                        },
                        new GroupItemDescription("Links", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("Area", 1){ LabelText = "Area" },
                                new ViewItemDescription("Issue", 2){ LabelText = "Issue" },
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
                                new ViewItemDescription("FileName", 1){ LabelText = "Preview file", ShowLabel = false, Fill = true },
                            }
                        }
                    }
                }
            };
        }
    }
}

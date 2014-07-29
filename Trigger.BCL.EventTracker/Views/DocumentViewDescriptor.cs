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
                                new ViewItemDescription("Subject", 1){ LabelText = "Name", ShowLabel = true },
                                new ViewItemDescription("Description", 2){ LabelText = "Description", ShowLabel = true },
                                new ViewItemDescription("User", 3){ LabelText = "From", ShowLabel = true },
                            }
                        },
                        new GroupItemDescription("Links", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("Area", 1){ LabelText = "Area", ShowLabel = true },
                                new ViewItemDescription("Issue", 2){ LabelText = "Issue", ShowLabel = true },
                            }
                        }
                    }
                },
                new TabItemDescription("Preview", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Preview", 1)
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

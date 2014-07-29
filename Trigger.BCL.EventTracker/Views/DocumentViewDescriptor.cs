using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{

    public class DocumentViewDescriptor : ViewDescriptor
    {
        public DocumentViewDescriptor()
        {
            GroupItems = new List<GroupItem>
            {
                new GroupItem("Details", 1)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("Subject", 1){ LabelText = "Name", ShowLabel = true },
                        new ViewItem("Description", 2){ LabelText = "Middle name", ShowLabel = true },
                        new ViewItem("User", 3){ LabelText = "From", ShowLabel = true },
                    }
                },
                new GroupItem("Links", 2)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("Area", 1){ LabelText = "Area", ShowLabel = true },
                        new ViewItem("Issue", 2){ LabelText = "Issue", ShowLabel = true },
                    }
                },
                new GroupItem("Preview", 2)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("FileName", 1){ LabelText = "Preview file", ShowLabel = false },
                    }
                }
            };
        }
    }
}

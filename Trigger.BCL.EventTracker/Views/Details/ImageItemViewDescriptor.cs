using System.Collections.Generic;
using XForms.Design;
using Trigger.BCL.EventTracker.Model;
using XForms.Commands;

namespace Trigger.BCL.EventTracker
{

    public class ImageItemViewDescriptor : DetailViewDescriptor<ImageItem>
    {
        public ImageItemViewDescriptor()
        {
            RegisterCommands<IAddFileDetailViewCommand>();
 
            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription(null, 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Name", LabelOrientation = LabelOrientation.Top },
                        new ViewItemDescription(Fields.GetName(m => m.Keywords), 1){ LabelText = "Keywords", LabelOrientation = LabelOrientation.Top },
                        new ViewItemDescription(Fields.GetName(m => m.Gallery), 1){ LabelText = "Gallery", LabelOrientation = LabelOrientation.Top }
                    }
                },
                new GroupItemDescription(null, 2)
                {
                    Fill = true,
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.FileName), 1){ LabelText = "Image", ShowLabel = false, Fill = true }
                    }
                },

            };
        }
    }
}

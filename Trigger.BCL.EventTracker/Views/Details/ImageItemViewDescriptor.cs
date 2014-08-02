using System.Collections.Generic;
using Trigger.XForms;
using Trigger.XForms.Commands;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class ImageItemViewDescriptor : DetailViewDescriptor<ImageItem>
    {
        public ImageItemViewDescriptor()
        {
            RegisterCommands<IAddFileCommand>();
 
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Image", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Details", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Name" },
                                new ViewItemDescription(Fields.GetName(m => m.Gallery), 1){ LabelText = "Gallery" }
                            }
                        },
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.FileName), 1){ LabelText = "Image", ShowLabel = false, Fill = true }
                            }
                        },
                    }
                }
            };
        }
    }
}

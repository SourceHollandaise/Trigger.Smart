using System.Collections.Generic;
using XForms.Design;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class ImageGalleryViewDescriptor : DetailViewDescriptor<ImageGallery>
    {
        public ImageGalleryViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Gallery", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Details", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Name), 1){ LabelText = "Name" },
                                new ViewItemDescription(Fields.GetName(m => m.Description), 2){ LabelText = "Description", Fill = true },
                                new ViewItemDescription(Fields.GetName(m => m.Owner), 2){ LabelText = "Owner" },
                            }
                        },
                        new GroupItemDescription("Images", 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedImageItems), 1){ LabelText = "Linked Images", ShowLabel = false, ListMode = ListPropertyMode.List }
                            }
                        },
                    },
                }
            };
        }
    }
}

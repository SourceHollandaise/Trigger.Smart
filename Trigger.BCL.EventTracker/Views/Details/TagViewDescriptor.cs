using System.Collections.Generic;
using XForms.Design;
using XForms.Model;

namespace Trigger.BCL.EventTracker
{
    public class TagViewDescriptor : DetailViewDescriptor<Tag>
    {
        public TagViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Tag", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Name), 1){ LabelText = "Name" },
                                new ViewItemDescription(Fields.GetName(m => m.TagColor), 2){ LabelText = "Color" },
                            }
                        },
                    }
                }
            };
        }
    }
}

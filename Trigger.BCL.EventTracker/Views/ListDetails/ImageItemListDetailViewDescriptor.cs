using System.Collections.Generic;
using XForms.Design;
using Trigger.BCL.EventTracker.Model;
using XForms.Commands;

namespace Trigger.BCL.EventTracker
{

    public class ImageItemListDetailViewDescriptor : DetailViewDescriptor<ImageItem>
    {
        public ImageItemListDetailViewDescriptor()
        {
            Commands.Clear();

            RegisterCommands<IAddFileDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();
            RegisterCommands<IDeleteObjectDetailViewCommand>();

            AutoSave = true;

            MinHeight = 480;

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

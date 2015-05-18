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

            //RegisterCommands<IAddFileDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();
            RegisterCommands<IDeleteObjectDetailViewCommand>();

            AutoSave = true;

            MinHeight = 360;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription(null, 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Name", LabelOrientation = LabelOrientation.Top },
                        new ViewItemDescription(Fields.GetName(m => m.Gallery), 2){ LabelText = "Gallery", LabelOrientation = LabelOrientation.Top },
                        new ViewItemDescription(Fields.GetName(m => m.Keywords), 3){ LabelText = "Tags",  LabelOrientation = LabelOrientation.Top }
                    }
                },
                new GroupItemDescription(null, 2)
                {
                    Fill = true,
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.PreviewFileName), 1){ LabelText = "Preview", ShowLabel = false, LabelOrientation = LabelOrientation.Top },
                    }
                },
            };
        }
    }
}

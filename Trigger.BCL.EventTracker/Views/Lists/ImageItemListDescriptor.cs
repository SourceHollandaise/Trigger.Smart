using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker
{

    public class ImageItemListDescriptor : ListViewDescriptor<ImageItem>
    {
        public ImageItemListDescriptor()
        {
            IsImageList = true;
            ListShowTags = false;

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Thumbnail), 1){ ColumnHeaderText = "Image" },
            };
        }
    }
}

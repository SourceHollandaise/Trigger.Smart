using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Design;

namespace Trigger.BCL.EventTracker
{

    public class ImageItemListDescriptor : ListViewDescriptor<ImageItem>
    {
        public ImageItemListDescriptor()
        {
            IsImageList = true;
            ListShowTags = false;
            RowHeight = 96;

            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Subject);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Thumbnail), 1){ ColumnHeaderText = "Image", AutoSize = false, AllowResize = false },
            };
        }
    }
}

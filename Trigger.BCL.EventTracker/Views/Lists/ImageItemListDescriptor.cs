using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Design;

namespace Trigger.BCL.EventTracker
{

    public class ImageItemListDescriptor : ListViewDescriptor<ImageItem>
    {
        public ImageItemListDescriptor()
        {
            ListShowTags = false;

            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Subject);

            ListDetailView = true;
            ListDetailViewColumns = 3;
            ListDetailViewWithToolbar = true;
            ListDetailViewOrientation = ViewItemOrientation.Vertical;

            DetailView = new ImageItemListDetailViewDescriptor();

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Thumbnail), 1){ ColumnHeaderText = "Image", AutoSize = false, AllowResize = false },
            };
        }
    }
}

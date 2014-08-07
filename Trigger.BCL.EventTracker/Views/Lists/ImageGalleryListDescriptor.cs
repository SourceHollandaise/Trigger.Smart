using XForms.Design;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class ImageGalleryListDescriptor : ListViewDescriptor<ImageGallery>
    {
        public ImageGalleryListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Name);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Name), 1){ ColumnHeaderText = "Name" },
            };
        }
    }
}

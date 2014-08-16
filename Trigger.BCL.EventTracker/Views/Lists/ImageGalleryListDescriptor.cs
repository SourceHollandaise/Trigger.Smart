using System.Collections.Generic;
using System.Linq;
using XForms.Design;
using XForms.Model;
using XForms.Store;
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
                new ColumnDescription(Fields.GetName(m => m.Description), 1){ ColumnHeaderText = "Description", AutoSize = true },
            };
        }
    }
}

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
            ListShowTags = true;

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Subject), 1){ ColumnHeaderText = "Name" },
                new ColumnDescription(Fields.GetName(m => m.GalleryAlias), 2){ ColumnHeaderText = "Gallery" },
            };
        }
    }
}

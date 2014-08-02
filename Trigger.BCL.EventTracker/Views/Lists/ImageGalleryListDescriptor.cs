using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker
{

    public class ImageGalleryListDescriptor : ListViewDescriptor<ImageGallery>
    {
        public ImageGalleryListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Name), 1){ ColumnHeaderText = "Name" },
            };
        }
    }
    
}

using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Design;
using XForms.Commands;
using XForms.Store;
using System.Linq;
using XForms.Model;
using XForms.Dependency;

namespace Trigger.BCL.EventTracker
{

    public class ImageItemListDescriptor : ListViewDescriptor<ImageItem>
    {
        public ImageItemListDescriptor()
        {
            RegisterCommands<IUpdateDocumentStoreListViewCommand>();
            RegisterCommands<ISlideShowListViewCommand>();

            ListShowTags = false;

            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Subject);

            ListDetailView = true;
            ListDetailViewColumns = 4;
            ListDetailViewWithToolbar = true;
            ListDetailViewOrientation = ViewItemOrientation.Vertical;
            ShowListDetailViewForLinkedLists = true;

            DetailView = new ImageItemListDetailViewDescriptor();

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Thumbnail), 1){ ColumnHeaderText = "Image", AutoSize = false, AllowResize = false },
            };
        }
    }
}

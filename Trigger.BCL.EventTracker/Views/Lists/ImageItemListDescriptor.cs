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
         

            ListShowTags = false;
            RowHeight = 64;
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Subject);

            ListDetailView = true;
            ListDetailViewColumns = 4;
            ListDetailViewWithToolbar = true;
            ListDetailViewOrientation = ViewItemOrientation.Vertical;
            ShowListDetailViewForLinkedLists = true;

            DetailView = new ImageItemListDetailViewDescriptor();

            FilePreviewMode = FileDataMode.SlideShow;

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Thumbnail), 1){ ColumnHeaderText = "Image", AutoSize = false, AllowResize = false },
                new ColumnDescription(Fields.GetName(m => m.GalleryAlias), 2){ ColumnHeaderText = "Gallery", AutoSize = false, AllowResize = true },
            };
        }
    }
}

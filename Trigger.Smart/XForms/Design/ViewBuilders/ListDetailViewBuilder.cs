using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;

namespace XForms.Design
{
    public class ListDetailViewBuilder
    {
        protected int? CurrentRowIndex = null;

        readonly IEnumerable<IStorable> dataSet;

        readonly IEnumerable<IStorable> originalDataSet;

        readonly IListViewDescriptor descriptor;

        readonly Type modelType;

        public ListDetailViewBuilder(IListViewDescriptor descriptor, Type modelType, IEnumerable<IStorable> dataSet = null)
        {
            this.descriptor = descriptor;
            this.originalDataSet = dataSet;
            this.dataSet = dataSet;
            this.modelType = modelType;
        }

        public Control GetContent()
        {
            var commandBarLayout = new DynamicLayout();
            commandBarLayout.BeginHorizontal();
            var commandBarBuilder = new ListViewCommandBarBuilder(descriptor, modelType, originalDataSet);
            commandBarLayout.Add(commandBarBuilder.GetContent());
            commandBarLayout.EndHorizontal();

            var contentLayout = new DynamicLayout();

            contentLayout.BeginHorizontal();

            var rawDataSet = new DataStoreProvider(descriptor, modelType).CreateRawDataSet(dataSet);

            if (descriptor.DefaultSorting == ColumnSorting.Ascending)
                rawDataSet = rawDataSet.OrderByProperty(descriptor.DefaultSortProperty);

            if (descriptor.DefaultSorting == ColumnSorting.Descendig)
                rawDataSet = rawDataSet.OrderByPropertyDescending(descriptor.DefaultSortProperty);

            var currentColumnCount = 0;
            var columns = descriptor.ListDetailViewColumns == 0 ? 1 : descriptor.ListDetailViewColumns - 1;
           
            if (descriptor.ListDetailViewOrientation == ViewItemOrientation.Vertical)
            {
                foreach (var current in rawDataSet.ToList())
                {
                    if (currentColumnCount == 0)
                        contentLayout.BeginHorizontal();
 
                    var builder = new ListDetailItemBuilder(descriptor.DetailView, current, current.GetDefaultPropertyValue(), descriptor.ListDetailViewWithToolbar);
                    var content = builder.GetContent();
   
                    var width = ((Application.Instance.MainForm as MainViewTemplate).ContentPanel.Size.Width - 120) / descriptor.ListDetailViewColumns;
                    //var height = (((Application.Instance.MainForm as MainViewTemplate).ContentPanel.Size.Height - 120) / 2) * 1.5;

                    content.Size = new Size(width, -1);

                    contentLayout.Add(content);

                    if (currentColumnCount == columns)
                    {
                        contentLayout.Add(null);
                        currentColumnCount = 0;
                        continue;
                    }

                    currentColumnCount++;
                }

                //contentLayout.Add(null);
            }

            if (descriptor.ListDetailViewOrientation == ViewItemOrientation.Horizontal)
            {
                contentLayout.BeginHorizontal();
                foreach (var current in rawDataSet.ToList())
                {
                    var builder = new ListDetailItemBuilder(descriptor.DetailView, current, current.GetDefaultPropertyValue(), descriptor.ListDetailViewWithToolbar);
                    var content = builder.GetContent();

                    var height = (Application.Instance.MainForm as MainViewTemplate).ContentPanel.Size.Height - 120;

                    var width = Convert.ToInt32(height * 0.75);

                    content.Size = new Size(width, height);

                    contentLayout.Add(content, false, false);
                }

                //contentLayout.Add(null);
            }

            var listDetaillayout = new DynamicLayout();
            listDetaillayout.Add(commandBarLayout);

            var scrollable = new Scrollable()
            {
                Border = BorderType.None,
                Padding = new Padding(-1, -1),
                Content = contentLayout
            };

            listDetaillayout.Add(scrollable);
            //listDetaillayout.EndHorizontal();
            listDetaillayout.Add(null);
            return listDetaillayout;
        }
    }
}

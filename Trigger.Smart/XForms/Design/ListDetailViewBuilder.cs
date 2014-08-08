using System;
using System.Collections.Generic;
using Eto.Forms;
using XForms.Store;
using System.Linq;
using Eto.Drawing;
using XForms.Commands;
using XForms.Dependency;

namespace XForms.Design
{
    public class ListDetailViewBuilder
    {
        protected int? CurrentRowIndex = null;

        IEnumerable<IStorable> dataSet;

        readonly IEnumerable<IStorable> originalDataSet;

        readonly bool isRoot;

        IListViewDescriptor descriptor;

        public Type ModelType
        {
            get;
            private set;
        }

        public ListDetailViewBuilder(IListViewDescriptor descriptor, Type modelType, bool viewIsRoot = true, IEnumerable<IStorable> dataSet = null, bool addCommandBar = false)
        {
            this.descriptor = descriptor;
            this.originalDataSet = dataSet;
            this.dataSet = dataSet;
            this.ModelType = modelType;
            this.isRoot = viewIsRoot;
        }

        public Control GetContent()
        {
            var listDetailLayout = new DynamicLayout();

            listDetailLayout.BeginHorizontal();
            var commandBarBuilder = new ListViewCommandBarBuilder(descriptor, ModelType, isRoot, originalDataSet);
            listDetailLayout.Add(commandBarBuilder.GetContent());
            listDetailLayout.EndHorizontal();
  
            var rawDataSet = new DataStoreProvider(descriptor, ModelType).CreateRawDataSet(dataSet);

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
                        listDetailLayout.BeginHorizontal();
 
                    var builder = new ListDetailItemBuilder(current, current.GetDefaultPropertyValue(), descriptor.ListDetailViewWithToolbar);
                    var content = builder.GetContent();
   
                    listDetailLayout.Add(content);

                    if (currentColumnCount == columns)
                    {
                        listDetailLayout.Add(new DynamicLayout());
                        currentColumnCount = 0;
                        continue;
                    }

                    currentColumnCount++;
                }
            }

            if (descriptor.ListDetailViewOrientation == ViewItemOrientation.Horizontal)
            {
                listDetailLayout.BeginHorizontal();
                foreach (var current in rawDataSet.ToList())
                {
                    var builder = new ListDetailItemBuilder(current, current.GetDefaultPropertyValue(), descriptor.ListDetailViewWithToolbar);
                    var content = builder.GetContent();

                    var width = (Application.Instance.MainForm as MainViewTemplate).ContentPanel.Size.Width / 2;

                    var height = (Application.Instance.MainForm as MainViewTemplate).ContentPanel.Size.Height - 120;

                    content.Size = new Size(width, height);

                    listDetailLayout.Add(content, false, false);
                }

                listDetailLayout.Add(null);
            }

            return listDetailLayout;
        }
    }
}

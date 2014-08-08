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

        bool addCommandBar;

        IListViewDescriptor descriptor;

        public Type ModelType
        {
            get;
            private set;
        }

        public ListDetailViewBuilder(IListViewDescriptor descriptor, Type modelType, bool viewIsRoot = true, IEnumerable<IStorable> dataSet = null, bool addCommandBar = false)
        {
            this.addCommandBar = addCommandBar;
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

            foreach (var current in rawDataSet.ToList())
            {
                listDetailLayout.BeginVertical();

                var builder = new ListDetailItemBuilder(current, current.GetDefaultPropertyValue(), addCommandBar);

                listDetailLayout.Add(builder.GetContent());

                listDetailLayout.EndVertical();
            }

            var scrollable = new Scrollable()
            {
                Border = BorderType.None,
                Size = new Size(-1, -1),
                Content = listDetailLayout
            };

            return scrollable;
        }
    }
}

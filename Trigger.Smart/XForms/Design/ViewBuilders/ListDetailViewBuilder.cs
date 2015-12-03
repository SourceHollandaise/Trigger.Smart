using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using XForms.Store;

namespace XForms.Design
{

    public class ListDetailViewBuilder : IViewBuilder
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
            var commandBarBuilder = new ListViewCommandBarBuilder(descriptor, modelType, originalDataSet, null, descriptor.ShowSearchBox);
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
            var calculated = ScreenResolutionTypeCalculator.CalculatOptimumListDetailColumns(descriptor);

            var columns = descriptor.ListDetailViewColumns == 0 ? 1 : calculated - 1;

            var detailViewDatatSet = rawDataSet.ToList();

            if (descriptor.ListDetailViewOrientation == ViewItemOrientation.Vertical)
            {
                foreach (var current in detailViewDatatSet)
                {
                    if (currentColumnCount == 0)
                        contentLayout.BeginHorizontal();

                    var builder = new ListDetailItemBuilder(descriptor.DetailView, current, current.GetDefaultPropertyValue(), descriptor.ListDetailViewWithToolbar);
                    var content = builder.GetContent();

                    if (Application.Instance.MainForm is MainViewTemplate)
                    {
                        var width = ((Application.Instance.MainForm as MainViewTemplate).ContentPanel.Size.Width - 120) / calculated;

                        content.Size = new Size(width, -1);
                    }


                    if (Application.Instance.MainForm is ReducedMainViewTemplate)
                    {
                        var width = ((Application.Instance.MainForm as ReducedMainViewTemplate).Size.Width - 80) / calculated;

                        content.Size = new Size(width, -1);
                    }

                    contentLayout.Add(content);

                    if (currentColumnCount == columns)
                    {
                        contentLayout.Add(null);
                        currentColumnCount = 0;
                        continue;
                    }

                    currentColumnCount++;
                }

                if (detailViewDatatSet.Count <= columns)
                    contentLayout.Add(null);
            }

            if (descriptor.ListDetailViewOrientation == ViewItemOrientation.Horizontal)
            {
                contentLayout.BeginHorizontal();
                foreach (var current in detailViewDatatSet)
                {
                    var builder = new ListDetailItemBuilder(descriptor.DetailView, current, current.GetDefaultPropertyValue(), descriptor.ListDetailViewWithToolbar);
                    var content = builder.GetContent();

                    if (Application.Instance.MainForm is MainViewTemplate)
                    {
                        var height = (Application.Instance.MainForm as MainViewTemplate).ContentPanel.Size.Height - 120;

                        var width = Convert.ToInt32(height * 0.75);

                        content.Size = new Size(width, height);
                    }

                    if (Application.Instance.MainForm is ReducedMainViewTemplate)
                    {
                        var height = (Application.Instance.MainForm as ReducedMainViewTemplate).Size.Height - 80;

                        var width = Convert.ToInt32(height * 0.75);

                        content.Size = new Size(width, height);
                    }


                    contentLayout.Add(content, false, false);
                }
            }

            var listDetaillayout = new DynamicLayout();
            listDetaillayout.Add(commandBarLayout);

            var scrollable = new Scrollable()
            {
                Border = BorderType.None,
                //Padding = new Padding(0, 0),
                Content = contentLayout
            };

            listDetaillayout.Add(scrollable);
            listDetaillayout.Add(null);
            return listDetaillayout;
        }
    }
}

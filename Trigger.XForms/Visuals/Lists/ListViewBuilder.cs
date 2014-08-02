using System;
using System.Linq;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.Reflection;
using System.Collections.Generic;
using Trigger.XStorable.Dependency;
using Eto.Drawing;
using Trigger.XForms.Commands;

namespace Trigger.XForms.Visuals
{
    public class ListViewBuilder
    {
        readonly ListViewControlFactory factory;

        protected IListViewDescriptor Descriptor
        {
            get;
            set;
        }

        public Type ModelType
        {
            get;
            set;
        }

        protected IEnumerable<IStorable> DataSet
        {
            get;
            set;
        }

        protected IEnumerable<IStorable> OriginalDataSet
        {
            get;
            set;
        }

        public ListViewBuilder(IListViewDescriptor descriptor, Type modelType, IEnumerable<IStorable> dataSet = null)
        {
            this.OriginalDataSet = dataSet;
            this.DataSet = dataSet;
            this.ModelType = modelType;
            this.Descriptor = descriptor;
            factory = new ListViewControlFactory(ModelType);   
        }

        public Control GetContent()
        {
            GridView gridView = null;

            var detailViewLayout = new DynamicLayout();
            detailViewLayout.BeginHorizontal();

            var commandBar = new DynamicLayout();
            commandBar.BeginHorizontal();
            foreach (var command in Descriptor.Commands)
            {
                var button = new Button();
                button.Size = new Size(40, 40);
                button.ID = command.ID;
                button.ToolTip = command.Name;
                button.Image = ImageExtensions.GetImage(command.ImageName, 24);
                button.ImagePosition = ButtonImagePosition.Overlay;
                button.Click += (sender, e) =>
                {
                    command.Execute(new ListViewArguments{ TargetType = ModelType, Grid = gridView, CustomDataSet = OriginalDataSet });
                };
                commandBar.Add(button, false, false);

            }
            commandBar.Add(new DynamicLayout(){ Size = new Size(-1, -1) });
            commandBar.EndHorizontal();

            detailViewLayout.Add(commandBar);

            detailViewLayout.EndHorizontal();
            detailViewLayout.BeginHorizontal();

            gridView = new GridView();
           
            foreach (var columnItem in Descriptor.ColumnDescriptions.OrderBy(p => p.Index).ToList())
            {
                var gridColumn = CreateColumn(columnItem);
                if (gridColumn != null)
                    gridView.Columns.Add(gridColumn);
            }

            if (DataSet == null)
                DataSet = DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll(ModelType).ToList();

            gridView.DataStore = new DataStoreCollection(DataSet);
            gridView.AllowColumnReordering = Descriptor.AllowColumnReorder;
            gridView.AllowMultipleSelection = Descriptor.AllowMultiSelection;
            gridView.ShowCellBorders = false;
            gridView.RowHeight = 32;

            gridView.CellEdited += (sender, e) =>
            {

            };

            gridView.CellFormatting += (object sender, GridCellFormatEventArgs e) =>
            {
                e.Font = new Font(e.Font.Family, 12f);
            };

            gridView.MouseDoubleClick += (sender, e) =>
            {
                if (gridView.SelectedItem != null)
                    (gridView.SelectedItem as IStorable).ShowDetailView();
            };

            detailViewLayout.Add(gridView);

            detailViewLayout.EndHorizontal();
            return detailViewLayout;
        }

        GridColumn CreateColumn(ColumnDescription columnItem)
        {
            var property = ModelType.GetProperty(columnItem.FieldName);
            if (property == null)
                return null;
                
            var cell = factory.CreateDataCell(property);

            if (cell == null)
                return null;

            var gridColumn = new GridColumn();
            gridColumn.DataCell = cell;
            gridColumn.HeaderText = columnItem.ColumnHeaderText;
            gridColumn.Sortable = columnItem.Sorting != ColumnSorting.None;
            gridColumn.Resizable = true;
            gridColumn.AutoSize = true;
           
            return gridColumn;
        }
    }
}

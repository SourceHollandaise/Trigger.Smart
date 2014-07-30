using System;
using System.Linq;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.Reflection;
using System.Collections.Generic;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Visuals
{
    public class ListViewBuilder
    {
        readonly ListPropertyEditorFactory editorFactory;

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

        public ListViewBuilder(IListViewDescriptor descriptor, Type modelType, IEnumerable<IStorable> dataSet = null)
        {
            this.DataSet = dataSet;
            this.ModelType = modelType;
            this.Descriptor = descriptor;
            editorFactory = new ListPropertyEditorFactory(ModelType);   
        }

        public GridView GetContent()
        {
            var gridView = new GridView();
           
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

            gridView.MouseDoubleClick += (sender, e) =>
            {
                if (gridView.SelectedItem != null)
                    WindowManager.ShowDetailView(gridView.SelectedItem as IStorable);
            };

            return gridView;
        }

        GridColumn CreateColumn(ColumnDescription columnItem)
        {
            var property = ModelType.GetProperty(columnItem.FieldName);
            if (property == null)
                return null;
                
            var cell = editorFactory.CreateDataCell(property);

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

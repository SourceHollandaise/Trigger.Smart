using System;
using System.Linq;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.Reflection;
using System.Collections.Generic;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Visuals
{
    public class ListViewGenerator
    {
        readonly IStore store = DependencyMapProvider.Instance.ResolveType<IStore>();
        readonly ListPropertyEditorFactory factory;

        protected Type ModelType
        {
            get;
            set;
        }

        public ListViewGenerator(Type modelType)
        {
            this.ModelType = modelType;
            factory = new ListPropertyEditorFactory(ModelType);
        }

        public GridView GetContent()
        {
            var items = store.LoadAll(ModelType).ToList();

            var gridView = CreateGrid();

            gridView.DataStore = new DataStoreCollection(items);
            gridView.AllowColumnReordering = true;
            gridView.AllowMultipleSelection = true;
            //gridView.ShowCellBorders = true;

            gridView.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Enter)
                {
                    ExecuteListOpenCommand(gridView);
                }
            };

            gridView.MouseDoubleClick += (sender, e) =>
            {
                ExecuteListOpenCommand(gridView);
            };

            return gridView;
        }

        GridView CreateGrid()
        {
            var	gridView = new GridView();

            var config = DependencyMapProvider.Instance.ResolveType<IViewTemplateConfiguration>();

            var compactViewAttribute = ModelType.GetCustomAttributes(typeof(ViewCompactAttribute), true).FirstOrDefault() as ViewCompactAttribute;
            if (config.IsCompactViewMode)
            {
                var property = ModelType.GetProperty(compactViewAttribute.VisualProperty);
                if (property != null)
                {
                    var column = CreateColumn(property);
                    if (column != null)
                        gridView.Columns.Add(column);

                    return gridView;
                }
            }
            else
            {
                foreach (var property in ModelType.GetProperties())
                {
                    var visibilityAttribute = property.GetCustomAttributes(typeof(FieldVisibleAttribute), true).FirstOrDefault() as FieldVisibleAttribute;

                    if (visibilityAttribute != null && (visibilityAttribute.TargetView == TargetView.DetailOnly || visibilityAttribute.TargetView == TargetView.None))
                        continue;

                    var column = CreateColumn(property);
                    if (column != null)
                        gridView.Columns.Add(column);
                }
            }

            return gridView;
        }

        GridColumn CreateColumn(PropertyInfo property)
        {
            var displayNameAttribute = property.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;

            var cell = factory.CreateDataCell(property);

            if (cell == null)
                return null;

            var column = new GridColumn();
            column.DataCell = cell;
            column.HeaderText = displayNameAttribute != null ? displayNameAttribute.DisplayName : property.Name;
            column.Sortable = true;
            column.Resizable = true;
            column.AutoSize = true;
            return column;
        }

        void ExecuteListOpenCommand(GridView gridView)
        {
            if (gridView.SelectedItem != null)
                WindowManager.ShowDetailView(gridView.SelectedItem as IStorable);
        }
    }
}

using System;
using System.Linq;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Reflection;
using System.Collections.Generic;

namespace Trigger.WinForms.Layout
{
	public class ListViewGenerator
	{
		readonly IStore store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();
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

			return gridView;
		}

		GridView CreateGrid()
		{
			var	gridView = new GridView();

			var visualRepresentationAttribute = ModelType.GetCustomAttributes(typeof(CompactViewRepresentationAttribute), true).FirstOrDefault() as CompactViewRepresentationAttribute;
			if (visualRepresentationAttribute != null)
			{
				var property = ModelType.GetProperty(visualRepresentationAttribute.VisualProperty);
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
					var visibilityAttribute = property.GetCustomAttributes(typeof(VisibleOnViewAttribute), true).FirstOrDefault() as VisibleOnViewAttribute;

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
	}
}

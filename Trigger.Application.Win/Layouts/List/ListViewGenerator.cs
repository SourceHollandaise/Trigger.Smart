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

		protected Type ModelType
		{
			get;
			set;
		}

		public ListViewGenerator(Type modelType)
		{
			this.ModelType = modelType;
		}

		public GridView GetContent()
		{
			var items = store.LoadAll(ModelType).ToList();

			var gridView = CreateGrid();

			gridView.DataStore = new DataStoreCollection(items);
			gridView.AllowColumnReordering = true;
			gridView.AllowMultipleSelection = true;
			gridView.ShowCellBorders = true;

			return gridView;
		}

		GridView CreateGrid()
		{
			var factory = new ListPropertyEditorFactory(ModelType);

			var	gridView = new GridView();

			foreach (var property in ModelType.GetProperties())
			{
				var attribute = property.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;

				var cell = factory.CreateDataCell(property);
				if (cell == null)
					continue;

				var column = new GridColumn();

				column.DataCell = cell;

				column.HeaderText = attribute != null ? attribute.DisplayName : property.Name;
				column.Sortable = true;

				gridView.Columns.Add(column);
			}

			return gridView;
		}
	}
}

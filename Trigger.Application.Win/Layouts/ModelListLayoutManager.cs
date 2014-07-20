using System;
using System.Linq;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Reflection;

namespace Trigger.WinForms.Layout
{
	public class ModelListLayoutManager
	{
		readonly IStore store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();

		protected Type ModelType
		{
			get;
			set;
		}

		public ModelListLayoutManager(Type modelType)
		{
			this.ModelType = modelType;
		}

		public GridView GetLayout()
		{
			var items = store.LoadAll(ModelType);
			var gridView = CreateGrid();

			gridView.DataStore = new DataStoreCollection(items);

			return gridView;
		}

		GridView CreateGrid()
		{
			var factory = new ListPropertyEditorFactory(ModelType);
			var gridView = new GridView();
			gridView.ShowCellBorders = true;

			foreach (var property in ModelType.GetProperties())
			{
				var cell = factory.CreateDataCell(property);
				if (cell == null)
					continue;

				var column = new GridColumn();
				column.DataCell = cell;
				column.HeaderText = property.Name;
				column.AutoSize = true;
				column.Editable = false;
				column.Resizable = true;
				column.Sortable = true;
				gridView.Columns.Add(column);
			}

			return gridView;
		}
	}
}

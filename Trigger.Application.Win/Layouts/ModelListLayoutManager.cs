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

		GridView gridView;

		public GridView GetLayout()
		{
			var items = store.LoadAll(ModelType).ToList();

			gridView = CreateGrid();

			gridView.DataStore = new DataStoreCollection(items);
			gridView.AllowColumnReordering = true;
			gridView.AllowMultipleSelection = true;
			gridView.ShowCellBorders = true;

			return gridView;
		}

		GridView CreateGrid()
		{
			var factory = new ListPropertyEditorFactory(ModelType);
			if (gridView == null)
				gridView = new GridView();

			foreach (var property in ModelType.GetProperties())
			{
				var cell = factory.CreateDataCell(property);
				if (cell == null)
					continue;

				var column = new GridColumn();
				column.DataCell = cell;
				column.HeaderText = property.Name;
				gridView.Columns.Add(column);
			}

			return gridView;
		}
	}
}

using System;
using System.Linq;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Data;

namespace Trigger.WinForms.Layout
{
	public class ModelListLayoutManager
	{
		readonly IStore store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();

		public GridView GetLayout(Type modelType)
		{
			var items = store.LoadAll(modelType).ToList();
			var gridView = CreateGrid(modelType);

			gridView.DataStore = new DataStoreCollection(items);

			return gridView;
		}

		public GridView CreateGrid(Type modelType)
		{
			GridView gridView = new GridView();

			foreach (var item in modelType.GetProperties())
			{
				var column = new GridColumn();
				column.AutoSize = true;
				column.DataCell = new TextBoxCell(item.Name);
				column.Editable = false;
				column.HeaderText = item.Name;
				column.Sortable = true;
				gridView.Columns.Add(column);

			}

			return gridView;
		}
	}
}

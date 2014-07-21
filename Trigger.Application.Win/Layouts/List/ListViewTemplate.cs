using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;

namespace Trigger.WinForms.Layout
{
	public class ListViewTemplate : TemplateBase
	{
		public GridView CurrentGrid
		{
			get;
			set;
		}

		public ListViewTemplate(Type modelType, IPersistent currentObject) : base(modelType, currentObject)
		{
			if (CurrentGrid == null)
				CurrentGrid = new ListViewGenerator(ModelType).GetLayout();

			Content = CurrentGrid;

			Size = new Size(1024, 768);
			Title = ModelType.Name + "-List - Items: " + CurrentGrid.DataStore.AsEnumerable().Count();

			Controllers.Add(new ActionNewController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionRefreshListController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionOpenObjectListController(this, ModelType, CurrentObject));

			if (typeof(IFileData).IsAssignableFrom(ModelType))
				Controllers.Add(new ActionFileDataListController(this, ModelType, CurrentObject));
		}
	}
}
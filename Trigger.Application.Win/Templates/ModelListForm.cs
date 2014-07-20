using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;

namespace Trigger.WinForms.Layout
{
	public class ModelListForm : TemplateBase
	{
		public GridView CurrentGrid
		{
			get;
			set;
		}

		public ModelListForm(Type modelType, IPersistentId currentObject) : base(modelType, currentObject)
		{
			if (CurrentGrid == null)
				CurrentGrid = new ModelListLayoutManager(ModelType).GetLayout();

			Content = CurrentGrid;

			Size = new Size(1280, 800);
			Title = ModelType.Name + "-List - Items: " + CurrentGrid.DataStore.AsEnumerable().Count();

			Controllers.Add(new ModelNewObjectActionController(this, ModelType, CurrentObject));
			Controllers.Add(new ModelRefreshListController(this, ModelType, CurrentObject));
			Controllers.Add(new ModelOpenObjectListController(this, ModelType, CurrentObject));

			if (typeof(IFileData).IsAssignableFrom(ModelType))
				Controllers.Add(new ModelFileDataListController(this, ModelType, CurrentObject));
		}
	}
}

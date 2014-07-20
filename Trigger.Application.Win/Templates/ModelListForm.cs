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

			this.ToolBar.Items.AddRange(new ModelNewObjectActionController(this, ModelType, CurrentObject).RegisterActions());
			this.ToolBar.Items.AddRange(new ModelRefreshListController(this, ModelType, CurrentObject).RegisterActions());
			this.ToolBar.Items.AddRange(new ModelOpenObjectListController(this, ModelType, CurrentObject).RegisterActions());
		}
	}
}

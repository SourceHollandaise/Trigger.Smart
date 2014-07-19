using Eto.Drawing;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;

namespace Trigger.WinForms.Layout
{
	public class ModelDetailForm : TemplateBase
	{
		public ModelDetailForm(Type modelType, IPersistentId currentObject) : base(modelType, currentObject)
		{
			Size = new Size(800, 600);
			Title = ModelType.Name + " - " + CurrentObject.GetRepresentation();

			Content = new ModelDetailLayoutManager(CurrentObject).GetLayout();

			//this.ToolBar.Items.AddRange(new ModelNewObjectActionController(this, ModelType, CurrentObject).RegisterActions());
			this.ToolBar.Items.AddRange(new ModelModificationController(this, CurrentObject).RegisterActions());
			this.ToolBar.Items.AddRange(new ModelRefreshDetailController(this, ModelType, CurrentObject).RegisterActions());
		}
	}
}
	


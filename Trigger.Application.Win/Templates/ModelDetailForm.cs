using Eto.Drawing;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;

namespace Trigger.WinForms.Layout
{
	public class ModelDetailForm : Form
	{
		readonly PersistentModelBase model;

	
		public ModelDetailForm(PersistentModelBase model)
		{
			this.model = model;

			Size = new Size(800, 600);
			Title = model.GetType().Name + " - " + model.GetRepresentation();

			Content = new ModelDetailLayoutManager().GetLayout(model);

			if (this.ToolBar == null)
				this.ToolBar = new ToolBar();
				
			this.ToolBar.Items.AddRange(new ModelNewObjectActionController(this, model.GetType()).RegisterActions());
			this.ToolBar.Items.AddRange(new ModelModificationController(this, model).RegisterActions());
		}
	}
}
	


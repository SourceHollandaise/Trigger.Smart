using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.Application.Win.Layouts;
using Trigger.Application.Win.Actions;
using Eto.Drawing;

namespace Trigger.Application.Win.Templates
{
	public class ModelDetailTemplate : Form
	{
		readonly PersistentModelBase model;

		public ModelDetailTemplate(PersistentModelBase model)
		{
			this.model = model;

			Size = new Size(800, 600);
			Title = model.GetType().Name + " - " + model.GetRepresentation();

			Content = new ModelDetailLayoutManager().GetLayout(model);

			RegisterActions();
		}

		void RegisterActions()
		{
			var actions = new GenerateActionArgs(this);
			actions.Actions.Add(new SaveButtonAction(model));
			actions.Actions.Add(new DeleteButtonAction(model));

			var file = actions.Menu.FindAddSubMenu("&File");
			file.Actions.Add(SaveButtonAction.ActionID);
			file.Actions.Add(DeleteButtonAction.ActionID);

			actions.ToolBar.Add(SaveButtonAction.ActionID);
			actions.ToolBar.Add(DeleteButtonAction.ActionID);

			Menu = actions.Menu.GenerateMenuBar();
			ToolBar = actions.ToolBar.GenerateToolBar();
		}
	}
}
	


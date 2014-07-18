using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;

namespace Trigger.CRM.Forms
{
	public class ModelDetailTemplate : Form
	{
		PersistentModelBase model;

		public ModelDetailTemplate(PersistentModelBase model)
		{
			this.model = model;
			Size = new Eto.Drawing.Size(800, 600);
			Title = model.GetType().Name + " - " + model.GetRepresentation();

			Content = new ModelDetailLayoutManager().GetLayout(model);

			RegisterActions();

			model.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
			{
				Content = new ModelDetailLayoutManager().GetLayout(model);
			};
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
	


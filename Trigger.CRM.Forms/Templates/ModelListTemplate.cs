using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.CRM.Forms.Layout;

namespace Trigger.CRM.Forms.Templates
{
	public class ModelListTemplate : Form
	{
		readonly IStore store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();

		readonly Type modelType;

		public ModelListTemplate(Type modelType)
		{
			this.modelType = modelType;
		
			Size = new Eto.Drawing.Size(1280, 800);
			Title = "List of " + modelType.Name;

			ListBox list = new ModelListLayoutManager().GetLayout(modelType);

			Content = list;

			list.KeyDown += (sender, e) =>
			{
				if (e.Key == Keys.Enter)
				{
					var target = store.Load(modelType, list.SelectedKey);

					var detailView = new ModelDetailTemplate(target as PersistentModelBase);

					detailView.Show();
				}
			};

			RegisterActions();
		}

		void RegisterActions()
		{
			var actions = new GenerateActionArgs(this);
			actions.Actions.Add(new NewButtonAction(modelType));
		
			var file = actions.Menu.FindAddSubMenu("&File");
			file.Actions.Add(NewButtonAction.ActionID);

			actions.ToolBar.Add(NewButtonAction.ActionID);


			Menu = actions.Menu.GenerateMenuBar();
			ToolBar = actions.ToolBar.GenerateToolBar();
		}
	}
}

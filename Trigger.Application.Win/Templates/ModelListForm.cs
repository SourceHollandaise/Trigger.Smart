using System;
using Eto.Drawing;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;

namespace Trigger.WinForms.Layout
{
	public class ModelListForm : Form
	{
		readonly IStore store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();

		readonly Type modelType;

		public ModelListForm(Type modelType)
		{
			this.modelType = modelType;
		
			Size = new Size(1280, 800);
			Title = "List of " + modelType.Name;

			ListBox list = new ModelListLayoutManager().GetLayout(modelType);

			Content = list;

			list.KeyDown += (sender, e) =>
			{
				if (e.Key == Keys.Enter)
				{
					var target = store.Load(modelType, list.SelectedKey);

					new ModelDetailForm(target as PersistentModelBase).Show();
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

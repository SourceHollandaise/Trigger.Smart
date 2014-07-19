using System;
using Eto.Drawing;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;

namespace Trigger.WinForms.Layout
{
	public class ModelListForm : TemplateBase
	{
		readonly IStore store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();

		public ListBox CurrentList
		{
			get;
			set;
		}

		public ModelListForm(Type modelType, PersistentModelBase currentObject) : base(modelType, currentObject)
		{

			Size = new Size(1280, 800);
			Title = "List of " + ModelType.Name;

			if (CurrentList == null)
				CurrentList = new ModelListLayoutManager().GetLayout(ModelType);

			Content = CurrentList;

			CurrentList.KeyDown += (sender, e) =>
			{
				if (e.Key == Keys.Enter)
					ShowDetailExecute();
			};

			CurrentList.MouseDoubleClick += (sender, e) =>
			{
				if (CurrentList.SelectedKey != null)
					ShowDetailExecute();
			};

			if (this.ToolBar == null)
				this.ToolBar = new ToolBar();

			this.ToolBar.Items.AddRange(new ModelNewObjectActionController(this, ModelType, CurrentObject).RegisterActions());
			this.ToolBar.Items.AddRange(new ModelRefreshListController(this, ModelType, CurrentObject).RegisterActions());
		}

		protected virtual void ShowDetailExecute()
		{
			CurrentObject = (PersistentModelBase)store.Load(ModelType, CurrentList.SelectedKey);
			if (CurrentObject != null)
				new ModelDetailForm(ModelType, CurrentObject).Show();
		}
	}
}

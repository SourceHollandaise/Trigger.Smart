using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using System.Collections.Generic;
using Eto.Drawing;

namespace Trigger.WinForms.Actions
{
	public class ModelFileDataListController : ActionBaseController
	{
		public ButtonToolItem LoadFilesAction
		{
			get;
			protected set;
		}

		public ModelFileDataListController(Form template, Type modelType, IPersistentId model) : base(template, model)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<ToolItem> ActionItems()
		{
			LoadFilesAction = new ButtonToolItem();
			LoadFilesAction.ID = "LoadFiles_Tool_Action";
			LoadFilesAction.Image = Bitmap.FromResource("File_add32.png");
			LoadFilesAction.Text = "Refresh";

			LoadFilesAction.Click += (sender, e) =>
			{
				LoadFilesExecute();
			};

			yield return LoadFilesAction;
		}

		protected virtual void LoadFilesExecute()
		{
			var service = Dependency.DependencyMapProvider.Instance.ResolveType<IFileDataService>();
			service.LoadFromStore();
		}
	}
}

using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.WinForms.Layout;
using System.Linq;

namespace Trigger.WinForms.Actions
{
	public class ModelFileDataListController : ActionBaseController
	{
		public ButtonToolItem LoadFilesAction
		{
			get;
			protected set;
		}

		public ModelFileDataListController(TemplateBase template, Type modelType, IPersistentId model) : base(template, model)
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
				LoadFilesActionExecute();
			};

			yield return LoadFilesAction;
		}

		public virtual void LoadFilesActionExecute()
		{
			var service = Dependency.DependencyMapProvider.Instance.ResolveType<IFileDataService>();
			service.LoadFromStore();

			var listForm = Template as ListViewTemplate;
			if (listForm != null)
				listForm.ReloadList();				
		}
	}
}

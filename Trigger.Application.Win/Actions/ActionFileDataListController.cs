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
	public class ActionFileDataListController : ActionBaseController
	{
		public Command LoadFilesAction
		{
			get;
			protected set;
		}

		public ActionFileDataListController(TemplateBase template, Type modelType, IPersistent model) : base(template, model)
		{
			Category = "Store";
		}

		public override IEnumerable<Command> Commands()
		{
			LoadFilesAction = new Command();
			LoadFilesAction.ID = "LoadFiles_Tool_Action";
			LoadFilesAction.Image = ImageExtensions.GetImage("File_add32.png", 24);
			LoadFilesAction.MenuText = "Load files";
			LoadFilesAction.ToolBarText = "Load files";
			LoadFilesAction.Executed += (sender, e) =>
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

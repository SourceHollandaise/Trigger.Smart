using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using Trigger.WinForms.Layout;
using Trigger.CRM.Model;

namespace Trigger.CRM.Actions
{
	public class ActionAreaController : ActionBaseController
	{
		public Command ShowLinkedDocumentsAction
		{
			get;
			protected set;
		}

		public ActionAreaController(TemplateBase template, Type modelType, IPersistent currentObject) : base(template, modelType, currentObject)
		{
			Category = "Edit";
			TargetView = ActionControllerTargetView.DetailView;
			TargetModelType = typeof(Area);
		}

		public override IEnumerable<Command> Commands()
		{
			ShowLinkedDocumentsAction = new Command();
			ShowLinkedDocumentsAction.ID = "Linked_documents_Tool_Action";
			ShowLinkedDocumentsAction.Image = ImageExtensions.GetImage("Close32.png", 24);
			ShowLinkedDocumentsAction.MenuText = "Linked documents";
			ShowLinkedDocumentsAction.ToolBarText = "Linked documents";
			ShowLinkedDocumentsAction.Executed += (sender, e) =>
			{
				ShowLinkedDocumentsExecute();
			};

			yield return ShowLinkedDocumentsAction;
		}

		protected virtual void ShowLinkedDocumentsExecute()
		{
			var area = CurrentObject as Area;
			if (area.LinkedDocuments.Any())
			{
				var listTemplate = new ListViewTemplate(typeof(Document), area);
				listTemplate.CurrentGrid.DataStore = new DataStoreCollection(area.LinkedDocuments);
				listTemplate.Show();

			}
		}
	}
}

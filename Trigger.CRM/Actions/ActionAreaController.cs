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

		public Command ShowLinkedIssuesAction
		{
			get;
			protected set;
		}

		public ActionAreaController(TemplateBase template, Type modelType, IPersistent currentObject) : base(template, modelType, currentObject)
		{
			Category = "Links";
			TargetView = ActionControllerTargetView.DetailView;
			TargetModelType = typeof(Area);
			Visiblity = ActionVisibility.Menu;
		}

		public override IEnumerable<Command> Commands()
		{
			ShowLinkedDocumentsAction = new Command();
			ShowLinkedDocumentsAction.ID = "Linked_documents_Tool_Action";
			ShowLinkedDocumentsAction.Image = ImageExtensions.GetImage("Folder_add32.png", 24);
			ShowLinkedDocumentsAction.MenuText = "All area documents";
			ShowLinkedDocumentsAction.ToolBarText = "All area documents";
			ShowLinkedDocumentsAction.Executed += (sender, e) =>
			{
				ShowLinkedDocumentsExecute();
			};

			yield return ShowLinkedDocumentsAction;

			ShowLinkedIssuesAction = new Command();
			ShowLinkedIssuesAction.ID = "Linked_issues_Tool_Action";
			ShowLinkedIssuesAction.Image = ImageExtensions.GetImage("Folder_add32.png", 24);
			ShowLinkedIssuesAction.MenuText = "All ares issues";
			ShowLinkedIssuesAction.ToolBarText = "All ares issues";
			ShowLinkedIssuesAction.Executed += (sender, e) =>
			{
				ShowLinkedIssuesExecute();
			};

			yield return ShowLinkedIssuesAction;
		}

		protected virtual void ShowLinkedDocumentsExecute()
		{
			CreateLinkView((CurrentObject as Area).LinkedDocuments);
		}

		protected virtual void ShowLinkedIssuesExecute()
		{
			CreateLinkView((CurrentObject as Area).LinkedIssues);
		}

		void CreateLinkView(IEnumerable<IPersistent> list)
		{
			var area = CurrentObject as Area;
			if (area != null && list.Any())
			{
				var listTemplate = new ListViewTemplate(typeof(Document), area);
				listTemplate.CurrentGrid.DataStore = new DataStoreCollection(area.LinkedDocuments);
				listTemplate.Show();
			}
		}
	}
}

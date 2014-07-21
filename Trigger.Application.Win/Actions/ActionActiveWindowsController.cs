using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using Trigger.WinForms.Layout;
using System.Collections.Generic;

namespace Trigger.WinForms.Actions
{
	public class ActionActiveWindowsController : ActionBaseController
	{
		public Command ActiveWindowsCommand
		{
			get;
			protected set;
		}

		public ActionActiveWindowsController(TemplateBase template, IPersistent currentObject) : base(template, currentObject)
		{
			Category = "Windows";
		}

		public override IEnumerable<Command> Commands()
		{
			ActiveWindowsCommand = new Command();
			ActiveWindowsCommand.ID = "ActiveWindows_Menu_Action";
			ActiveWindowsCommand.MenuText = "Active windows";
			ActiveWindowsCommand.Executed += (sender, e) =>
			{
				UpdateActiveWindowsActionItems();
			};

			yield return ActiveWindowsCommand;
		}

		public virtual void UpdateActiveWindowsActionItems()
		{
			foreach (var view in WindowManager.ActiveViews)
			{

			}
		}
	}
}

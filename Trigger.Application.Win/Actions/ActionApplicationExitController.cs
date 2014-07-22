using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using Trigger.WinForms.Layout;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.Datastore.Security;

namespace Trigger.WinForms.Actions
{

	public class ActionApplicationExitController : ActionBaseController
	{
		public Command ExitAction
		{
			get;
			protected set;
		}

		public Command LogOffAction
		{
			get;
			protected set;
		}

		public ActionApplicationExitController(TemplateBase template, IPersistent model) : base(template, model)
		{
			Category = "Application";
			TargetView = ActionControllerTargetView.Main;
		}

		public override IEnumerable<Command> Commands()
		{
			ExitAction = new Command();
			ExitAction.ID = "Exit_Tool_Action";
			ExitAction.Image = ImageExtensions.GetImage("Close32.png", 24);
			ExitAction.MenuText = "Exit";
			ExitAction.ToolBarText = "Exit";

			ExitAction.Executed += (sender, e) =>
			{
				ExitActionExecute();
			};

			yield return ExitAction;

			LogOffAction = new Command();
			LogOffAction.ID = "LogOff_Tool_Action";
			LogOffAction.Image = ImageExtensions.GetImage("Login_out32.png", 24);
			LogOffAction.MenuText = "Log off";
			LogOffAction.ToolBarText = "Log off";
			LogOffAction.Executed += (sender, e) =>
			{
				LogOffActionExecute();
			};

			yield return LogOffAction;
		}

		public virtual void ExitActionExecute()
		{
			var result = MessageBox.Show("Close application?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
			if (result == DialogResult.Yes)
				Application.Instance.Quit();
		}

		public virtual void LogOffActionExecute()
		{
			var result = MessageBox.Show("Log off from application?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
			if (result == DialogResult.Yes)
			{
				var logon = new LogonViewTemplate();
				logon.Topmost = true;
				logon.Focus();
				logon.Show();

				logon.Closed += (sender, args) =>
				{
					Template.Title = "User: " + Dependency.DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser.UserName;
				};
			}
		}
	}
}

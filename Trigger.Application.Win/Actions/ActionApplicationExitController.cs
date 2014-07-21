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
		public ButtonToolItem ExitAction
		{
			get;
			protected set;
		}

		public ButtonToolItem LogOffAction
		{
			get;
			protected set;
		}

		public ActionApplicationExitController(TemplateBase template, IPersistent model) : base(template, model)
		{

		}

		public override IEnumerable<ToolItem> ActionItems()
		{
			ExitAction = new ButtonToolItem();
			ExitAction.ID = "Exit_Tool_Action";
			ExitAction.Image = ImageExtensions.GetImage("Close32.png", 24);
			ExitAction.Text = "Exit";

			ExitAction.Click += (sender, e) =>
			{
				ExitActionExecute();
			};

			yield return ExitAction;

			LogOffAction = new ButtonToolItem();
			LogOffAction.ID = "LogOff_Tool_Action";
			LogOffAction.Image = ImageExtensions.GetImage("Login_out32.png", 24);
			LogOffAction.Text = "Log off";

			LogOffAction.Click += (sender, e) =>
			{
				LogOffActionExecute();
			};

			yield return LogOffAction;
		}

		public virtual void ExitActionExecute()
		{
			Template.Close();
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

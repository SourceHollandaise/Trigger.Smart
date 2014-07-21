using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using Trigger.WinForms.Layout;
using System.Collections.Generic;

namespace Trigger.WinForms.Actions
{
	public class ActionActiveWindowsController : ActionBaseController
	{
		public ButtonMenuItem ActiveWindowsAction
		{
			get;
			protected set;
		}

		public ActionActiveWindowsController(TemplateBase template, IPersistent currentObject) : base(template, currentObject)
		{

		}

		public override IEnumerable<MenuItem> MenuItems()
		{
			ActiveWindowsAction = new ButtonMenuItem();
			ActiveWindowsAction.ID = "ActiveWindows_Menu_Action";
			ActiveWindowsAction.Text = "Active windows";
			ActiveWindowsAction.Click += (sender, e) =>
			{
				UpdateActiveWindowsActionItems();
			};

			yield return ActiveWindowsAction;
		}

		public virtual void UpdateActiveWindowsActionItems()
		{
			ActiveWindowsAction.Items.Clear();

			foreach (var view in WindowManager.ActiveViews)
			{
				var item = new CheckMenuItem();
				item.ID = view.ID;
				item.Text = view.Title;
				item.Checked = false;
				ActiveWindowsAction.Items.Add(item);
			}
		}
	}
}

using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using Trigger.WinForms.Layout;
using System.Collections.Generic;
using System;

namespace Trigger.WinForms.Actions
{
	public class ActionActiveWindowsController : ActionBaseController
	{
		public Command ActiveWindowsCommand
		{
			get;
			protected set;
		}

		public ActionActiveWindowsController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
		{
			Category = "Windows";
			Visiblity = ActionVisibility.None;
		}

		public override IEnumerable<Command> Commands()
		{
			ActiveWindowsCommand = new Command();
			ActiveWindowsCommand.ID = "ActiveWindows_Menu_Action";
			ActiveWindowsCommand.MenuText = "Active windows";
			ActiveWindowsCommand.ToolBarText = "Active windows";
			ActiveWindowsCommand.Executed += (sender, e) =>
			{
				UpdateActiveWindowsActionItems();
			};

			yield return ActiveWindowsCommand;
		}

		public virtual void UpdateActiveWindowsActionItems()
		{
			var windowMenu = Template.Menu.Items.GetSubmenu("Window");
			windowMenu.Items.Clear();

			foreach (var view in WindowManager.ActiveViews)
			{
				var item = new CheckCommand();
				item.ID = view.ID;
				item.MenuText = view.Title;
			}

			windowMenu.Items.Trim();
		}
	}
}

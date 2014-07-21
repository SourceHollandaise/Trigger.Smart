using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;
using System.Collections.Generic;
using System.Linq;

namespace Trigger.WinForms.Actions
{

	public class ActionActiveWindowsController : ActionBaseController
	{
		public ButtonMenuItem ActiveWindowsMenuItem
		{
			get;
			protected set;
		}

		public ActionActiveWindowsController(TemplateBase template, IPersistent currentObject) : base(template, currentObject)
		{

		}

		public override IEnumerable<MenuItem> MenuItems()
		{
			ActiveWindowsMenuItem = new ButtonMenuItem();
			ActiveWindowsMenuItem.ID = "ActiveWindows_Menu_Action";
			ActiveWindowsMenuItem.Text = "Active windows";
		

			foreach (var item in TemplateManager.ActiveTemplates)
			{
				var menuItem = new ButtonMenuItem();
				menuItem.ID = item.ID;
				menuItem.Text = item.Title;

				ActiveWindowsMenuItem.Items.Add(menuItem);
			}

			yield return ActiveWindowsMenuItem;
		}
	}
}

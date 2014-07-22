using Eto.Forms;
using System.Collections.Generic;
using Trigger.Datastore.Persistent;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public enum ActionVisibility
	{
		MenuAndToolbar,
		Menu,
		Toolbar,
		None
	}


	public enum ActionControllerTargetView
	{
		Any,
		DetailView,
		ListView,
		Main
	}
}

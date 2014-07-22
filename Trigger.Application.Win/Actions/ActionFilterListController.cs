using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;
using System.Collections.Generic;
using Eto.Drawing;

namespace Trigger.WinForms.Actions
{
	public class ActionFilterListController : ActionBaseController
	{
		public ActionFilterListController(TemplateBase template, Type modelType, IPersistent currentObject) : base(template, modelType, currentObject)
		{
			this.ModelType = modelType;
			Visiblity = ActionVisibility.Toolbar;
			TargetView = ActionControllerTargetView.ListView;
		}
	}
}

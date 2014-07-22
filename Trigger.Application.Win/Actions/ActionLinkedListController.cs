using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;
using System.Collections.Generic;
using Eto.Drawing;
using System.Linq;

namespace Trigger.WinForms.Actions
{
	public class ActionLinkedListController : ActionBaseController
	{
		public ActionLinkedListController(TemplateBase template, Type modelType, IPersistent currentObject) : base(template, modelType, currentObject)
		{
			Category = "Edit";
			this.ModelType = modelType;
			Visiblity = ActionVisibility.Menu;
			TargetView = ActionControllerTargetView.DetailView;
		}
	}
}

using Eto.Forms;
using System.Collections.Generic;
using Trigger.Datastore.Persistent;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public abstract class ActionBaseController
	{
		public ActionVisibility Visiblity
		{
			get;
			set;
		}

		public ActionControllerTargetView TargetView
		{
			get;
			set;
		}

		public string Category
		{
			get;
			set;
		}

		public Type TargetModelType
		{
			get;
			set;
		}

		protected TemplateBase Template
		{
			get;
			set;
		}

		protected Type ModelType
		{
			get;
			set;
		}

		protected IPersistent CurrentObject
		{
			get;
			set;
		}

		protected ActionBaseController(TemplateBase template, Type modelType, IPersistent currentObject)
		{
			Category = "File";
			Visiblity = ActionVisibility.MenuAndToolbar;
			TargetView = ActionControllerTargetView.Any;
			TargetModelType = typeof(IPersistent);

			this.Template = template;
			this.CurrentObject = currentObject;
			if (this.ModelType == null && CurrentObject != null)
				this.ModelType = CurrentObject.GetType();
				
		}

		public virtual IEnumerable<Command> Commands()
		{
			yield break;
		}
	}
}

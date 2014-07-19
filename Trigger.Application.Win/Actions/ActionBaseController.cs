using Eto.Forms;
using System.Collections.Generic;
using Trigger.Datastore.Persistent;
using System;

namespace Trigger.WinForms.Actions
{
	public abstract class ActionBaseController
	{
		protected Form Template
		{
			get;
			set;
		}

		protected Type ModelType
		{
			get;
			set;
		}

		protected PersistentModelBase CurrentObject
		{
			get;
			set;
		}

		protected ActionBaseController(Form template, PersistentModelBase currentObject)
		{
			this.Template = template;
			this.CurrentObject = currentObject;
			if (this.ModelType == null && CurrentObject != null)
				this.ModelType = CurrentObject.GetType();
		}

		public virtual IEnumerable<ToolItem>  RegisterActions()
		{
			yield break;
		}
	}
}

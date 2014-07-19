using Eto.Drawing;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;

namespace Trigger.WinForms.Layout
{

	public abstract class TemplateBase : Form
	{
		protected PersistentModelBase CurrentObject
		{
			get;
			set;
		}

		protected Type ModelType
		{
			get;
			set;

		}

		protected TemplateBase(System.Type type, PersistentModelBase currentObject)
		{
			this.ModelType = type;
			this.CurrentObject = currentObject;

			if (this.ToolBar == null)
				this.ToolBar = new ToolBar();
		}
	}
	
}

using Eto.Forms;
using Trigger.Datastore.Persistent;
using System;

namespace Trigger.WinForms.Layout
{

	public abstract class TemplateBase : Form
	{
		protected IPersistentId CurrentObject
		{
			get;
			set;
		}

		protected Type ModelType
		{
			get;
			set;
		}

		protected TemplateBase(Type type, IPersistentId currentObject)
		{
			this.ModelType = type;
			this.CurrentObject = currentObject;

			if (this.ToolBar == null)
				this.ToolBar = new ToolBar();
				
		}
	}
}

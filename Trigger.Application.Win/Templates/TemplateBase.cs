using Eto.Forms;
using Trigger.Datastore.Persistent;
using System;
using System.Collections.Generic;
using Trigger.WinForms.Actions;

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

		public void LazyRegisterControllers(IEnumerable<ActionBaseController> controllers)
		{
			foreach (var controller in controllers)
				this.ToolBar.Items.AddRange(controller.RegisterActions());
		}
	}
}

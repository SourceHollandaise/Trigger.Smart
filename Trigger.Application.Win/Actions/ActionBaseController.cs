using Eto.Forms;
using System.Collections.Generic;

namespace Trigger.WinForms.Actions
{
	public abstract class ActionBaseController
	{
		protected Form Container
		{
			get;
			set;
		}

		protected ActionBaseController(Form container)
		{
			this.Container = container;	
		}

		public virtual IEnumerable<ToolItem>  RegisterActions()
		{
			yield break;
		}
	}
}

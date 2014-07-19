using Eto.Drawing;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ModelNewObjectActionController : ActionBaseController
	{
		public ButtonToolItem NewAction
		{
			get;
			protected set;
		}

		public Type ModelType
		{
			get;
			set;
		}

		public ModelNewObjectActionController(Form container, Type modelType) : base(container)
		{
			this.ModelType = modelType;
		}

		public override System.Collections.Generic.IEnumerable<ToolItem> RegisterActions()
		{
			NewAction = new ButtonToolItem();
			NewAction.Text = "New" + ModelType.Name;
			NewAction.ID = "New_Tool_Action";
			NewAction.Click += (sender, e) =>
			{
				NewObjectExecute();
			};

			yield return NewAction;
		}

		protected virtual void NewObjectExecute()
		{
			var item = Activator.CreateInstance(ModelType) as PersistentModelBase;

			var detailTemplate = new ModelDetailForm(item);
			detailTemplate.Show();
		}
	}
	
}

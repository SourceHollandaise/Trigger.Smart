using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{

	public class ModelRefreshDetailController : ModelRefreshBaseController
	{
		public ModelRefreshDetailController(TemplateBase template, Type modelType, IPersistentId model) : base(template, modelType, model)
		{
			this.ModelType = modelType;
		}

		public override void RefreshActionExecute()
		{
			var detailForm = Template as DetailViewTemplate;
			if (detailForm != null)
				detailForm.ReloadObject();
		}
	}
}

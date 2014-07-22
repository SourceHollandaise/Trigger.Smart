using System.Collections.Generic;
using Trigger.WinForms.Actions;
using System.Linq;

namespace Trigger.WinForms.Layout
{
	public class ActionControllerManager
	{
		protected TemplateBase Template
		{
			get;
			set;
		}

		public ActionControllerManager(TemplateBase template)
		{
			this.Template = template;	
		}

		public IEnumerable<ActionBaseController> ValidControllers()
		{
			if (Template is DetailViewTemplate)
			{
				foreach (var controller in Template.Controllers.Where(p => p.TargetView == ActionControllerTargetView.DetailView || p.TargetView == ActionControllerTargetView.Any))
				{
					if (controller.TargetModelType.IsAssignableFrom(Template.ModelType))
						yield return controller;
				}
			}

			if (Template is ListViewTemplate)
			{
				foreach (var controller in Template.Controllers.Where(p => p.TargetView == ActionControllerTargetView.ListView || p.TargetView == ActionControllerTargetView.Any))
				{
					if (controller.TargetModelType.IsAssignableFrom(Template.ModelType))
						yield return controller;
				}
			}

			if (Template is MainViewTemplate)
			{
				foreach (var controller in Template.Controllers.Where(p => p.TargetView == ActionControllerTargetView.Main))
				{
					if (controller.TargetModelType.IsAssignableFrom(Template.ModelType))
						yield return controller;
				}
			}
		}

		void CreateControllers()
		{

		}
	}
	
}

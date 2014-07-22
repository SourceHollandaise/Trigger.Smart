using System.Collections.Generic;
using Trigger.WinForms.Actions;
using System.Linq;
using System;

namespace Trigger.WinForms.Layout
{
	public class ActionControllerProvider
	{
		protected TemplateBase Template
		{
			get;
			set;
		}

		public ActionControllerProvider(TemplateBase template)
		{
			this.Template = template;
			CreateControllers();
		}

		public IEnumerable<ActionBaseController> ValidControllers()
		{
			var controllers = CreateControllers().ToList();

			if (Template is DetailViewTemplate)
			{
				foreach (var controller in controllers.Where(p => p.TargetView == ActionControllerTargetView.DetailView || p.TargetView == ActionControllerTargetView.Any))
				{
					if (controller.TargetModelType.IsAssignableFrom(Template.ModelType))
						yield return controller;
				}
			}

			if (Template is ListViewTemplate)
			{
				foreach (var controller in controllers.Where(p => p.TargetView == ActionControllerTargetView.ListView || p.TargetView == ActionControllerTargetView.Any))
				{
					if (controller.TargetModelType.IsAssignableFrom(Template.ModelType))
						yield return controller;
				}
			}

			if (Template is MainViewTemplate)
			{
				foreach (var controller in controllers.Where(p => p.TargetView == ActionControllerTargetView.Main))
				{
					if (controller.TargetModelType.IsAssignableFrom(Template.ModelType))
						yield return controller;
				}
			}
		}

		IEnumerable<ActionBaseController> CreateControllers()
		{
			foreach (var type in ActionControllerDeclaration.DeclaredControllerTypes)
			{
				var controller = Activator.CreateInstance(type, Template, Template.ModelType, Template.CurrentObject);
				yield return controller as ActionBaseController;
			}
		}
	}
}

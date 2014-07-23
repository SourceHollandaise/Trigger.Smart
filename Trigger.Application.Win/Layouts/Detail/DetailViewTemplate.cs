using Eto.Drawing;
using Trigger.Datastore.Persistent;
using System;

namespace Trigger.WinForms.Layout
{
	public class DetailViewTemplate : TemplateBase
	{
		public DetailViewTemplate(Type modelType, IStorable currentObject) : base(modelType, currentObject)
		{
			Content = new DetailViewGenerator(CurrentObject).GetContent();

			Size = new Size(400, 800);
			Title = ModelType.Name + " - " + CurrentObject.GetDefaultPropertyValue();
		}
	}
}
	


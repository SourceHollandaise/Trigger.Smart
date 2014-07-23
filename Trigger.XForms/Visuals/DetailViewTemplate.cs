using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using System;

namespace Trigger.XForms.Visuals
{
	public class DetailViewTemplate : TemplateBase
	{
		public DetailViewTemplate(Type modelType, IStorable currentObject) : base(modelType, currentObject)
		{
			Content = new DetailViewGenerator(CurrentObject).GetContent();
			Content = new Scrollable{ Content = this.Content };

			Size = new Size(400, 768);
			Title = ModelType.Name + " - " + CurrentObject.GetDefaultPropertyValue();
		}
	}
}
	


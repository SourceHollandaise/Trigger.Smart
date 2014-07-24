using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using System;
using System.Linq;

namespace Trigger.XForms.Visuals
{
    public class DetailViewTemplate : TemplateBase
    {
        public DetailViewTemplate(Type modelType, IStorable currentObject) : base(modelType, currentObject)
        {
            Content = new DetailViewGenerator(CurrentObject).GetContent();
            Content = new Scrollable{ Content = this.Content };

            Size = new Size(400, 768);

            var displayNameAttribute = ModelType.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;
            if (displayNameAttribute != null)
                Title = displayNameAttribute.DisplayName + " - " + CurrentObject.GetDefaultPropertyValue();
            else
                Title = ModelType.Name + " - " + CurrentObject.GetDefaultPropertyValue();
        }
    }
}
	


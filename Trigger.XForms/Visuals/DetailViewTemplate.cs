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

            SetContent(currentObject);


            SetSize();

            SetTitle();

        }

        void SetContent(IStorable currentObject)
        {
            var descriptorAttribute = currentObject.GetType().GetCustomAttributes(typeof(ViewDescriptorAttribute), true).FirstOrDefault() as ViewDescriptorAttribute;
            if (descriptorAttribute != null)
            {
                var descriptor = Activator.CreateInstance(descriptorAttribute.DescriptorType) as ViewDescriptor;
                Content = new DetailViewInterpreter(descriptor, currentObject).CreateFromDescriptor();
            }
            else
                Content = new DetailViewGenerator(CurrentObject).GetContent();

            Content = new Scrollable
            {
                Content = this.Content
            };
        }

        void SetSize()
        {
            Size = ViewTemplateConfig.IsCompactViewMode ? ViewTemplateConfig.DetailViewCompactSize : ViewTemplateConfig.DetailViewDefaultSize;
        }

        void SetTitle()
        {
            var displayNameAttribute = ModelType.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;
            if (displayNameAttribute != null)
                Title = displayNameAttribute.DisplayName + " - " + CurrentObject.GetDefaultPropertyValue();
            else
                Title = ModelType.Name + " - " + CurrentObject.GetDefaultPropertyValue();
        }
    }
}
	


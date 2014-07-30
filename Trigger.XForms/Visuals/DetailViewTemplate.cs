using System;
using System.Linq;
using Trigger.XStorable.DataStore;

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

        public void SetContent(IStorable currentObject)
        {
            var descriptorType = ViewDescriptorDeclarator.GetDescriptor(currentObject.GetType());
            if (descriptorType != null)
            {
                var descriptor = Activator.CreateInstance(descriptorType) as IViewDescriptor;
                Content = new ModelToDetailViewInterpreter(descriptor, currentObject).GetContent();
            }
            else
                Content = new DetailViewGenerator(CurrentObject).GetContent();
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
	


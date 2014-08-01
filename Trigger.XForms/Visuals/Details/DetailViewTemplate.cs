using System;
using Trigger.XStorable.DataStore;
using System.ComponentModel;

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
            var descriptorType = DetailViewDescriptorProvider.GetDescriptor(currentObject.GetType());
            if (descriptorType != null)
            {
                var descriptor = Activator.CreateInstance(descriptorType) as IDetailViewDescriptor;
                Content = new DetailViewBuilder(descriptor, currentObject).GetContent();
            }
        }

        void SetSize()
        {
            Size = ViewTemplateConfig.IsCompactViewMode ? ViewTemplateConfig.DetailViewCompactSize : ViewTemplateConfig.DetailViewDefaultSize;
        }

        void SetTitle()
        {
            var displayNameAttribute = ModelType.FindAttribute<DisplayNameAttribute>();
            if (displayNameAttribute != null)
                Title = displayNameAttribute.DisplayName + " - " + CurrentObject.GetDefaultPropertyValue();
            else
                Title = ModelType.Name + " - " + CurrentObject.GetDefaultPropertyValue();
                
        }
    }
}
	


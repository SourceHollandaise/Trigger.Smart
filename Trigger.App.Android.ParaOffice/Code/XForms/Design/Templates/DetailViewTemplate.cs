using System;
using System.ComponentModel;
using Eto.Drawing;
using XForms.Store;

namespace XForms.Design
{

    public class DetailViewTemplate : TemplateBase
    {
        public DetailViewTemplate(Type modelType, IStorable currentObject) : base(modelType, currentObject)
        {
            SetContent(currentObject);

            SetTitle();

            this.BackgroundColor = Colors.WhiteSmoke;
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

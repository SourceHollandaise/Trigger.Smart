using System;
using System.Linq;
using Trigger.XStorable.DataStore;
using Eto.Drawing;

namespace Trigger.XForms.Visuals
{
    public class ListViewTemplate : TemplateBase
    {
        public ListViewTemplate(Type modelType, IStorable currentObject) : base(modelType, currentObject)
        {
            SetContent();

            SetSize();
			
            SetTitle();
        }

        public void SetContent()
        {
            var descriptorType = ListViewDescriptorProvider.GetDescriptor(ModelType);
            if (descriptorType != null)
            {
                var descriptor = Activator.CreateInstance(descriptorType) as IListViewDescriptor;

                Content = new ListViewBuilder(descriptor, ModelType).GetContent();
            }
        }

        void SetSize()
        {
            Size = ViewTemplateConfig.IsCompactViewMode ? ViewTemplateConfig.DetailViewCompactSize : ViewTemplateConfig.DetailViewDefaultSize;
        }

        void SetTitle()
        {
            var displayNameAttribute = ModelType.FindAttribute<System.ComponentModel.DisplayNameAttribute>();
            Title = displayNameAttribute != null ? displayNameAttribute.DisplayName : ModelType.Name;
        }
    }
}

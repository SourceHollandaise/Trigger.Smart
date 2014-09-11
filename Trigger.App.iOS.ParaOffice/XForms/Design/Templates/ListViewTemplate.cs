using System;
using XForms.Store;

namespace XForms.Design
{
    [Obsolete("Use ListViewBuilder and ListViewDescriptor!", true)]
    public class ListViewTemplate : TemplateBase
    {
        public ListViewTemplate(Type modelType, IStorable currentObject) : base(modelType, currentObject)
        {
            SetContent();

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

        void SetTitle()
        {
            var displayNameAttribute = ModelType.FindAttribute<System.ComponentModel.DisplayNameAttribute>();
            Title = displayNameAttribute != null ? displayNameAttribute.DisplayName : ModelType.Name;
        }
    }
}

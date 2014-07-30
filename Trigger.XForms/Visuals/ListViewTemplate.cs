using System;
using System.Linq;
using Eto.Forms;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Visuals
{
    public class ListViewTemplate : TemplateBase
    {
        public GridView CurrentGrid
        {
            get;
            protected set;
        }

        public ListViewTemplate(Type modelType, IStorable currentObject) : base(modelType, currentObject)
        {
            SetContent();

            SetSize();
			
            SetTitle();
        }

        public void SetContent()
        {
            var descriptorType = ListDescriptorProvider.GetDescriptor(ModelType);
            if (descriptorType != null)
            {
                var descriptor = Activator.CreateInstance(descriptorType) as IListDescriptor;
                Content = new ModelToListViewInterpreter(descriptor, ModelType).GetContent();
            }
            else
                Content = new ListViewGenerator(ModelType).GetContent();

            CurrentGrid = Content as GridView;
        }

        void SetSize()
        {
            Size = ViewTemplateConfig.IsCompactViewMode ? ViewTemplateConfig.DetailViewCompactSize : ViewTemplateConfig.DetailViewDefaultSize;
        }

        void SetTitle()
        {
            var displayNameAttribute = ModelType.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;
            if (displayNameAttribute != null)
                Title = displayNameAttribute.DisplayName + " - Items: " + CurrentGrid.DataStore.AsEnumerable().Count();
            else
                Title = ModelType.Name + " - Items: " + CurrentGrid.DataStore.AsEnumerable().Count();
        }
    }
}

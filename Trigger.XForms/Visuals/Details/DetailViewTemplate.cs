using System;
using Trigger.XStorable.DataStore;
using System.ComponentModel;
using Trigger.BCL.Common.Model;
using System.Linq;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Visuals
{
    public class DetailViewTemplate : TemplateBase
    {
        public DetailViewTemplate(Type modelType, IStorable currentObject) : base(modelType, currentObject)
        {
            SetContent(currentObject);

            SetSize();

            SetTitle();

            //SetTagBackColor();
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

        void SetTagBackColor()
        {
            if (CurrentObject.MappingId == null)
                return;

            var store = DependencyMapProvider.Instance.ResolveType<IStore>();
            var tag = store.LoadAll<Tag>().FirstOrDefault(p => p.TargetObjectMappingId.Equals(CurrentObject.MappingId.ToString()));
            if (tag != null)
                BackgroundColor = Eto.Drawing.Color.Parse(tag.TagColor);
        }
    }
}

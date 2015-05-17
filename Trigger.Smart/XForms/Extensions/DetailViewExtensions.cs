using System;
using System.Collections.Generic;
using Eto.Forms;
using XForms.Dependency;
using XForms.Store;

namespace XForms.Design
{
    public static class DetailViewExtensions
    {
        public static void ShowDetailContentEmbedded(this IStorable currentObject, TemplateBase template = null)
        {
            var detailDescriptorType = DetailViewDescriptorProvider.GetDescriptor(currentObject.GetType());
            if (detailDescriptorType != null)
            {
                var detailDescriptor = Activator.CreateInstance(detailDescriptorType) as IDetailViewDescriptor;

                var detailBuilder = new DetailViewBuilder(detailDescriptor, currentObject);
                if (template == null)
                {
                    Application.Instance.MainForm.Title = currentObject.GetDefaultPropertyValue();

                    var content = detailBuilder.GetContent();
                    (Application.Instance.MainForm as IMainViewTemplate).SetContent(content);
                    TemplateNavigator.Add(content);
                }
                else
                {
                    template.Content = detailBuilder.GetContent();
                    TemplateNavigator.Add(template.Content);
                }
            }
        }

        public static void ShowDetailView(this IStorable targetObject)
        {
            if (targetObject != null)
            {
                var template = TryGetDetailView(targetObject);
                if (template != null)
                {
                    var size = MapProvider.Instance.ResolveType<IViewTemplateConfiguration>().DetailViewDefaultSize;
                    template.Content.Size = size;
                    template.Show();
            
                }
            }
        }

        public static void TryCloseDetailView(this IStorable targetObject)
        {

        }

        public static void RemoveDetailView(this IStorable targetObject)
        {

        }

        public static DetailViewTemplate TryGetDetailView(this IStorable targetObject)
        {
            if (targetObject != null)
            {
                return new DetailViewTemplate(targetObject.GetType(), targetObject);
            }

            return null;
        }

        public static void ReloadObject(this IStorable target)
        {
            var store = MapProvider.Instance.ResolveType<IStore>();
            var dataObject = store.Load(target.GetType(), target.MappingId);
            if (dataObject != null)
            {
                var descriptorType = DetailViewDescriptorProvider.GetDescriptor(dataObject.GetType());
                if (descriptorType != null)
                {
                    var descriptor = Activator.CreateInstance(descriptorType) as IDetailViewDescriptor;
                    target.TryGetDetailView().Content = new DetailViewBuilder(descriptor, dataObject).GetContent();
                }
            }
        }
    }
}

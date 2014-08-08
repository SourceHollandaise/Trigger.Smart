using System;
using System.Collections.Generic;
using Eto.Forms;
using XForms.Dependency;
using XForms.Store;

namespace XForms.Design
{

    public static class DetailViewExtensions
    {
        static readonly Dictionary<IStorable, DetailViewTemplate> detailTemplates = new Dictionary<IStorable, DetailViewTemplate>();

        public static void ShowDetailContentEmbedded(this IStorable currentObject, TemplateBase template = null)
        {
            var detailDescriptorType = DetailViewDescriptorProvider.GetDescriptor(currentObject.GetType());
            if (detailDescriptorType != null)
            {
                var detailDescriptor = Activator.CreateInstance(detailDescriptorType) as IDetailViewDescriptor;

                var detailBuilder = new DetailViewBuilder(detailDescriptor, currentObject);
                if (template == null)
                {
                    (Application.Instance.MainForm as MainViewTemplate).Title = currentObject.GetDefaultPropertyValue();

                    (Application.Instance.MainForm as MainViewTemplate).ContentPanel.Content = detailBuilder.GetContent();

                    TemplateNavigator.Add((Application.Instance.MainForm as MainViewTemplate).ContentPanel.Content);
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
                if (detailTemplates.ContainsKey(targetObject))
                    detailTemplates[targetObject].Show();
                else
                {
                    var template = new DetailViewTemplate(targetObject.GetType(), targetObject);
                    if (!detailTemplates.ContainsKey(targetObject))
                        detailTemplates.Add(targetObject, template);

                    template.Show();
                }
            }
        }

        public static void TryCloseDetailView(this IStorable targetObject)
        {
            if (detailTemplates.ContainsKey(targetObject))
            {
                detailTemplates[targetObject].Close();
                RemoveDetailView(targetObject);
            }
        }

        public static void RemoveDetailView(this IStorable targetObject)
        {
            if (detailTemplates.ContainsKey(targetObject))
                detailTemplates.Remove(targetObject);
        }

        public static DetailViewTemplate TryGetDetailView(this IStorable targetObject)
        {
            if (targetObject != null)
            {
                if (detailTemplates.ContainsKey(targetObject))
                    return detailTemplates[targetObject];

                var template = new DetailViewTemplate(targetObject.GetType(), targetObject);
                detailTemplates.Add(targetObject, template);

                return template;
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

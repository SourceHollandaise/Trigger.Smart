using System.Linq;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using System.Collections.Generic;
using System;
using Trigger.XForms.Visuals;

namespace Trigger.XForms
{
    public static class ListViewTemplateExtensions
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
                    (Application.Instance.MainForm as MainViewTemplate).Title = currentObject.GetDefaultPropertyValue();

                    (Application.Instance.MainForm as MainViewTemplate).ContentPanel.Content = detailBuilder.GetContent();
                }
                else
                    template.Content = detailBuilder.GetContent();
            }
        }

        public static void ReloadList(this GridView gridView, Type type, IEnumerable<IStorable> customDataSource = null)
        {
            var descriptorType = ListViewDescriptorProvider.GetDescriptor(type);
            if (descriptorType != null)
            {
                var descriptor = Activator.CreateInstance(descriptorType) as IListViewDescriptor;
                if (descriptor != null)
                {
                    if (customDataSource == null)
                    {
                        var dataSet = descriptor.Repository ?? DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll(type);

                        if (descriptor.Filter != null)
                            gridView.DataStore = new DataStoreCollection(dataSet.Where(descriptor.Filter));
                        else
                            gridView.DataStore = new DataStoreCollection(dataSet);
                    }
                    else
                    {
                        var dataSet = descriptor.Repository ?? customDataSource;

                        if (descriptor.Filter != null)
                            dataSet = customDataSource.Where(descriptor.Filter);
                        gridView.DataStore = new DataStoreCollection(dataSet);
                    }
                }
            }
        }
    }
}

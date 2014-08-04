using System.Linq;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using System.Collections.Generic;
using System;

namespace Trigger.XForms
{
    public static class ListViewTemplateExtensions
    {
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

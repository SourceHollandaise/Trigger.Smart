using System.Linq;
using Eto.Forms;
using System.Collections.Generic;
using System;
using XForms.Store;
using XForms.Dependency;

namespace XForms.Design
{
    public static class ListViewTemplateExtensions
    {
        public static void ReloadList(this GridView gridView, Type type, IEnumerable<IStorable> customDataSource = null)
        {
            if (gridView == null)
                return;

            var descriptorType = ListViewDescriptorProvider.GetDescriptor(type);
            if (descriptorType != null)
            {
                var descriptor = Activator.CreateInstance(descriptorType) as IListViewDescriptor;
                if (descriptor != null)
                {
                    if (customDataSource == null)
                    {
                        var dataSet = descriptor.Repository ?? MapProvider.Instance.ResolveType<IStore>().LoadAll(type);

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

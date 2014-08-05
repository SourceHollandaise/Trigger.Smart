using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Visuals
{
    public class DataStoreProvider
    {
        readonly IListViewDescriptor descriptor;

        readonly Type modelType;

        public DataStoreProvider(IListViewDescriptor descriptor, Type type)
        {
            this.modelType = type;
            this.descriptor = descriptor;
        }

        public IDataStore CreateDataSet(IEnumerable<IStorable> dataSet)
        {
            if (descriptor == null)
                return null;

            if (dataSet == null)
            {
                var tempDataSet = descriptor.Repository ?? DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll(modelType);

                if (descriptor.Filter != null)
                    tempDataSet = tempDataSet.Where(descriptor.Filter);
                dataSet = tempDataSet;
               
            }
           
            return new DataStoreCollection(dataSet);
        }
    }
}

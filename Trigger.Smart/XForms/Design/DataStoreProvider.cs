using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Forms;
using XForms.Store;
using XForms.Dependency;

namespace XForms.Design
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

        public IList<IStorable> CreateDataSet(IEnumerable<IStorable> dataSet)
        {
            if (descriptor == null)
                return null;

            if (dataSet == null)
            {
                var tempDataSet = descriptor.Repository ?? MapProvider.Instance.ResolveType<IStore>().LoadAll(modelType);

                if (descriptor.Filter != null)
                    tempDataSet = tempDataSet.Where(descriptor.Filter);
                dataSet = tempDataSet;
               
            }
           
            return dataSet.ToList();
        }

        public IEnumerable<IStorable> CreateRawDataSet(IEnumerable<IStorable> dataSet)
        {
            if (descriptor == null)
                return null;

            if (dataSet == null)
            {
                var tempDataSet = descriptor.Repository ?? MapProvider.Instance.ResolveType<IStore>().LoadAll(modelType);

                if (descriptor.Filter != null)
                    tempDataSet = tempDataSet.Where(descriptor.Filter);
                dataSet = tempDataSet;

            }

            return dataSet;
        }
    }
}

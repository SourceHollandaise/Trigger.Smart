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
            var store = DependencyMapProvider.Instance.ResolveType<IStore>();
            if (customDataSource == null)
                gridView.DataStore = new DataStoreCollection(store.LoadAll(type));
            else
                gridView.DataStore = new DataStoreCollection(customDataSource);
        }
    }
}

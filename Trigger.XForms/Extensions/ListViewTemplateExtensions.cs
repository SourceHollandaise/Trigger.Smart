using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.Linq;
using Trigger.XForms.Visuals;
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

        public static void ReloadList(this ListViewTemplate listForm, IEnumerable<IStorable> customDataSource = null)
        {
            if (listForm != null)
            {
                var store = DependencyMapProvider.Instance.ResolveType<IStore>();
                if (customDataSource == null)
                    listForm.CurrentGrid.DataStore = new DataStoreCollection(store.LoadAll(listForm.ModelType));
                else
                    listForm.CurrentGrid.DataStore = new DataStoreCollection(customDataSource);

                listForm.Title = listForm.ModelType.Name + "-List - Items: " + listForm.CurrentGrid.DataStore.AsEnumerable().Count();
            }
        }
    }
}

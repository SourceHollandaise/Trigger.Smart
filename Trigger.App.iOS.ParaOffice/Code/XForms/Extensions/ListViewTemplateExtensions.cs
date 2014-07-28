using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Controllers;
using System.Linq;
using System;
using Trigger.XForms.Visuals;
using Trigger.XStorable.Dependency;
using System.Collections.Generic;

namespace Trigger.XForms
{
    public static class ListViewTemplateExtensions
    {
        public static void ReloadList(this ListViewTemplate listForm, IEnumerable<IStorable> customDataSource = null)
        {
            if (listForm != null)
            {
                var store = DependencyMapProvider.Instance.ResolveType<IStore>();

                listForm.CurrentGrid.DataStore = customDataSource == null 
                    ? new DataStoreCollection(store.LoadAll(listForm.ModelType)) 
                    : new DataStoreCollection(customDataSource);

                listForm.Title = listForm.ModelType.Name + "-List - Items: " + listForm.CurrentGrid.DataStore.AsEnumerable().Count();
            }
        }
    }
}

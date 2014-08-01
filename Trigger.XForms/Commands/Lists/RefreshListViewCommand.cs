using System;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Commands
{

    public class RefreshListViewCommand : IRefreshListViewCommand
    {
        public void Execute(Type type)
        {
            var store = DependencyMapProvider.Instance.ResolveType<IStore>();
//            if (customDataSource == null)
//                gridView.DataStore = new DataStoreCollection(store.LoadAll(type));
//            else
//                gridView.DataStore = new DataStoreCollection(customDataSource);
        }

        public string ID
        {
            get
            {
                return "cmd_refresh";
            }
        }

        public string Name
        {
            get
            {
                return "Refresh";
            }
        }

        public string ImageName
        {
            get
            {
                return "Refresh16";
            }
        }
    }
}

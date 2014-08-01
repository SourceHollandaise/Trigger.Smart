using Eto.Forms;
using Trigger.XForms.Controllers;
using System;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{

    public class RefreshDetailViewCommand : IDetailViewCommand
    {
        public void Execute(IStorable current)
        {
            var store = DependencyMapProvider.Instance.ResolveType<IStore>();
            var dataObject = store.Load(current.GetType(), current.MappingId);
            if (dataObject != null)
            {
               
            }
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
    }
    
}

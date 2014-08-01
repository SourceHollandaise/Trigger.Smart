using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Controllers;

namespace Trigger.XForms.Commands
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

        public string ImageName
        {
            get
            {
                return "Refresh16";
            }
        }
    }
}

using Trigger.XForms.Controllers;
using System;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Controllers
{

    public class UpdateDocumentStoreCommand : IUpdateDocumentStoreCommand
    {
        public void Execute(Type type)
        {
            DependencyMapProvider.Instance.ResolveType<IFileDataService>().LoadFromStore();
        }

        public string ID
        {
            get
            {
                return "cmd_update_docstore";
            }
        }

        public string Name
        {
            get
            {
                return "Load data";
            }
        }
    }
}

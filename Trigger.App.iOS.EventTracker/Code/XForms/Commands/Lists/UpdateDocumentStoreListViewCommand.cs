using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Commands
{

    public class UpdateDocumentStoreListViewCommand : IUpdateDocumentStoreListViewCommand
    {
        public void Execute(ListViewArguments args)
        {
            DependencyMapProvider.Instance.ResolveType<IFileDataService>().LoadFromStore();
            args.Grid.ReloadList(args.TargetType, args.CustomDataSet);
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

        public string ImageName
        {
            get
            {
                return "folder_down";
            }
        }

        public bool AllowExecute
        {
            get
            {
                return true;
            }
        }

        public bool Visible
        {
            get
            {
                return true;
            }
        }
    }
}

using XForms.Dependency;
using XForms.Store;
using XForms.Design;

namespace XForms.Commands
{

    public class UpdateDocumentStoreListViewCommand : IUpdateDocumentStoreListViewCommand
    {
        public void Execute(ListViewArguments args)
        {
            MapProvider.Instance.ResolveType<IFileDataService>().LoadFromStore();
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
                return "Update Documents";
            }
        }

        public string ImageName
        {
            get
            {
                return "folder_down";
            }
        }

        public int Width
        {
            get
            {
                return 140;
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

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

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.Image;
            }
        }

        public string Name
        {
            get
            {
                return "Load files";
            }
        }

        public string ImageName
        {
            get
            {
                return "server_to_client";
            }
        }

        public int Width
        {
            get
            {
                return 34;
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

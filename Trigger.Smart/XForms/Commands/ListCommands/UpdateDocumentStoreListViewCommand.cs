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

        public string ID => "cmd_update_docstore";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Load files";

        public string ImageName => "server_to_client";

        public int Width => 34;

        public bool AllowExecute => true;

        public bool Visible => true;
    }
}

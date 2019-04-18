using XForms.Design;

namespace XForms.Commands
{
    public class RefreshDetailViewCommand : IRefreshDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            if (args.CurrentObject != null)
                args.CurrentObject.ReloadObject();
        }

        public string ID => "cmd_refresh";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Refresh";

        public string ImageName => "cloud_computing_refresh";

        public int Width => 34;

        public bool AllowExecute => true;

        public bool Visible => true;
    }
}

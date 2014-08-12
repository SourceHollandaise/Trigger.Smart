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
                return "down";
            }
        }

        public int Width
        {
            get
            {
                return 70;
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

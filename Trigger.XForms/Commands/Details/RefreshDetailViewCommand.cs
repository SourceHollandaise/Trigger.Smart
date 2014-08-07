
namespace Trigger.XForms.Commands
{
    public class RefreshDetailViewCommand : IRefreshDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
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
                return 80;
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

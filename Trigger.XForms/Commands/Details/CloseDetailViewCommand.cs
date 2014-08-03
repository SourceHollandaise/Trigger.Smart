
namespace Trigger.XForms.Commands
{

    public class CloseDetailViewCommand : ICloseDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            if (args.CurrentObject != null)
                args.CurrentObject.TryCloseDetailView();
        }

        public string ID
        {
            get
            {
                return "cmd_close";
            }
        }

        public string Name
        {
            get
            {
                return "Close";
            }
        }

        public string ImageName
        {
            get
            {
                return "window_remove";
            }
        }
    }
}

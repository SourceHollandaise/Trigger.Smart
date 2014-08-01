
namespace Trigger.XForms.Commands
{

    public class CloseWindowCommand : ICloseWindowCommand
    {
        public void Execute(DetailViewArguments args)
        {
            if (args.Template != null)
                args.Template.Close();
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

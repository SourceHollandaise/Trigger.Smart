
namespace Trigger.XForms.Commands
{

    public class SaveObjectDetailViewCommand : ISaveObjectDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            args.CurrentObject.Save();
        }

        public string ID
        {
            get
            {
                return "cmd_save";
            }
        }

        public string Name
        {
            get
            {
                return "Save";
            }
        }

        public string ImageName
        {
            get
            {
                return "accept";
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

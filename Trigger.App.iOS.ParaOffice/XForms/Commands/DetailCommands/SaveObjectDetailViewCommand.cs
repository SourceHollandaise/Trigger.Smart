
namespace XForms.Commands
{

    public class SaveObjectDetailViewCommand : ISaveObjectDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            if (args.CurrentObject != null)
                args.CurrentObject.Save();
        }

        public string ID
        {
            get
            {
                return "cmd_save";
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
                return "Save";
            }
        }

        public string ImageName
        {
            get
            {
                return "floppy_disk";
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

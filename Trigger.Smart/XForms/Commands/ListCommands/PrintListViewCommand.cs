
namespace XForms.Commands
{

    public class PrintListViewCommand : IPrintListViewCommand
    {
        public void Execute(ListViewArguments listParameter)
        {

        }

        public string ID
        {
            get
            {
                return "cmd_print";
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
                return "Print";
            }
        }

        public string ImageName
        {
            get
            {
                return "printer";
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

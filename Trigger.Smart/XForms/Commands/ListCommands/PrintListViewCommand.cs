
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

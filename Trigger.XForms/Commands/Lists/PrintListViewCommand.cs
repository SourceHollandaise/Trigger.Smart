using Eto.Forms;

namespace Trigger.XForms.Commands
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
    }
}

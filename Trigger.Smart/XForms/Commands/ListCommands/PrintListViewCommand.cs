
namespace XForms.Commands
{

    public class PrintListViewCommand : IPrintListViewCommand
    {
        public void Execute(ListViewArguments listParameter)
        {

        }

        public string ID => "cmd_print";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Print";

        public string ImageName => "printer";

        public int Width => 70;

        public bool AllowExecute => true;

        public bool Visible => true;
    }
}

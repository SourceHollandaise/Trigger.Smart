using Eto.Forms;


namespace XForms.Commands
{

    public class NavigateHomeListViewCommand : INavigateHomeListViewCommand
    {

        public void Execute(ListViewArguments listParameter)
        {
            if (Application.Instance.MainForm is XForms.Design.ReducedMainViewTemplate)
            {
                (Application.Instance.MainForm as XForms.Design.ReducedMainViewTemplate).UpdateNavigation();
            }
        }

        public string ID => "cmd_navigate_home";

        public string Name => "Home";

        public string ImageName => "navigate_close";

        public int Width => 34;

        public bool AllowExecute => true;

        public bool Visible => Application.Instance.MainForm is XForms.Design.ReducedMainViewTemplate;

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;
    }
}

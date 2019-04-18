using XForms.Design;

namespace XForms.Commands
{

    public class NavigateBackListViewCommand : INavigateBackListViewCommand
    {

        public void Execute(ListViewArguments listParameter)
        {
            TemplateNavigator.Back();
        }

        public string ID => "cmd_navigate_back";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Back";

        public string ImageName => "clock_history";

        public int Width => 34;

        public bool AllowExecute => TemplateNavigator.BackPossible;

        public bool Visible => true;
    }
}

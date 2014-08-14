using XForms.Design;

namespace XForms.Commands
{

    public class NavigateBackListViewCommand : INavigateBackListViewCommand
    {

        public void Execute(ListViewArguments listParameter)
        {
            TemplateNavigator.Back();
        }

        public string ID
        {
            get
            {
                return "cmd_navigate_back";
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
                return "Back";
            }
        }

        public string ImageName
        {
            get
            {
                return "clock_history";
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
                return TemplateNavigator.BackPossible;
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

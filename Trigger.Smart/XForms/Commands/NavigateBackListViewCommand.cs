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

        public string Name
        {
            get
            {
                return "<";
            }
        }

        public string ImageName
        {
            get
            {
                return "";
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

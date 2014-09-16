using Eto.Forms;


namespace XForms.Commands
{

    public class NavigateHomeDetailViewCommand : INavigateHomeDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            if (Application.Instance.MainForm is XForms.Design.ReducedMainViewTemplate)
            {
                (Application.Instance.MainForm as XForms.Design.ReducedMainViewTemplate).UpdateNavigation();
            }
        }

        public string ID
        {
            get
            {
                return "cmd_navigate_home";
            }
        }

        public string Name
        {
            get
            {
                return "Home";
            }
        }

        public string ImageName
        {
            get
            {
                return "navigate_close";
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
                return Application.Instance.MainForm is XForms.Design.ReducedMainViewTemplate;
            }
        }

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.Image;
            }
        }
    }
}

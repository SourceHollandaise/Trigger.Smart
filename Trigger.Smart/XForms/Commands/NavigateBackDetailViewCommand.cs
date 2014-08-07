using Trigger.XForms.Visuals;

namespace Trigger.XForms.Commands
{

    public class NavigateBackDetailViewCommand : INavigateBackDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
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

using XForms.Design;

namespace XForms.Commands
{
    public class LogonCommand : ILogonCommand
    {
        public void Execute(TemplateBase template)
        {
            new LogonViewTemplate().ShowDialog();
        }

        public string ID
        {
            get
            {
                return "cmd_logoff";
            }
        }

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.ImageAndText;
            }
        }

        public string Name
        {
            get
            {
                return "Login";
            }
        }

        public string ImageName
        {
            get
            {
                return "key2";
            }
        }

        public int Width
        {
            get
            {
                return 100;
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

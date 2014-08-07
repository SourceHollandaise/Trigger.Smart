using XForms.Design;

namespace XForms.Commands
{
    public class LogonCommand : ILogonCommand
    {
        public void Execute(MainViewTemplate template)
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
                return "lock";
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

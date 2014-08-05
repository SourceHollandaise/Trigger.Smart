using Eto.Forms;
using Trigger.XForms.Visuals;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Commands
{
    public class LogOffCommand : ILogOffCommand
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
                return "Log off";
            }
        }

        public string ImageName
        {
            get
            {
                return "lock";
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

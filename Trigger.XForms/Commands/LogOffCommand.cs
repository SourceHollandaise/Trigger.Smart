using Eto.Forms;
using Trigger.XForms.Visuals;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Security;

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
    }
}

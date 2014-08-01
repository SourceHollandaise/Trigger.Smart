using Trigger.XForms.Controllers;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Commands
{

    public class CloseWindowCommand : ICloseWindowCommand
    {
        public void Execute(IStorable current)
        {

        }

        public string ID
        {
            get
            {
                return "cmd_close";
            }
        }

        public string Name
        {
            get
            {
                return "Close";
            }
        }

        public string ImageName
        {
            get
            {
                return "Close16";
            }
        }
    }
}

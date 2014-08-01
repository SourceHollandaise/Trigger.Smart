using Eto.Forms;
using Trigger.XForms.Controllers;
using System;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
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
    }
    
}

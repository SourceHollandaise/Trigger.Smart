using Eto.Forms;
using Trigger.XForms.Controllers;
using System;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{

    public class OpenObjectCommand : IOpenObjectCommand
    {
        public void Execute(Type type)
        {

        }

        public string ID
        {
            get
            {
                return "cmd_open";
            }
        }

        public string Name
        {
            get
            {
                return "Open";
            }
        }
    }
    
}

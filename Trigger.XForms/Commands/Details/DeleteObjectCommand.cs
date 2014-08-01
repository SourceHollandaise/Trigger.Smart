using Eto.Forms;
using Trigger.XForms.Controllers;
using System;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{

    public class DeleteObjectCommand : IDeleteObjectCommand
    {
        public void Execute(IStorable current)
        {
            current.Delete();
        }

        public string ID
        {
            get
            {
                return "cmd_delete";
            }
        }

        public string Name
        {
            get
            {
                return "Delete";
            }
        }
    }
    
}

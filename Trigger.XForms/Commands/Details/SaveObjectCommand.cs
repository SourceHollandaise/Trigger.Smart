using Trigger.XForms.Controllers;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Controllers
{

    public class SaveObjectCommand : ISaveObjectCommand
    {
        public void Execute(IStorable current)
        {
            current.Save();
        }

        public string ID
        {
            get
            {
                return "cmd_save";
            }
        }

        public string Name
        {
            get
            {
                return "Save";
            }
        }
    }
}

using Eto.Forms;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Commands
{

    public class ApplicationExitCommand : IApplicationExitCommand
    {

        public void Execute(MainViewTemplate template)
        {
            template.Close();
            Application.Instance.Quit();
        }

        public string ID
        {
            get
            {
                return "cmd_exit";
            }
        }

        public string Name
        {
            get
            {
                return "Exit";
            }
        }

        public string ImageName
        {
            get
            {
                return "Login_out16";
            }
        }
    }
}

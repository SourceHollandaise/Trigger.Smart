using Eto.Forms;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Commands
{

    public class ApplicationExitCommand : IApplicationExitCommand
    {
        public void Execute(MainViewTemplate template)
        {
            var result = MessageBox.Show("Close application?", "Exit", MessageBoxButtons.OKCancel, MessageBoxType.Warning);
            if (result == DialogResult.Ok)
            {
                template.Close();
                Application.Instance.Quit();
            }
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
                return "application_remove";
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

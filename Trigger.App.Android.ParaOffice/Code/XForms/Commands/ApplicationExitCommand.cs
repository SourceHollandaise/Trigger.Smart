using Eto.Forms;
using XForms.Design;

namespace XForms.Commands
{

    public class ApplicationExitCommand : IApplicationExitCommand
    {
        public void Execute(TemplateBase template)
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

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.ImageAndText;
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
                return "exit";
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

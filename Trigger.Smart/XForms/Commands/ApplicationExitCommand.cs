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

        public string ID => "cmd_exit";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.ImageAndText;

        public string Name => "Exit";

        public string ImageName => "exit";

        public int Width => 100;

        public bool AllowExecute => true;

        public bool Visible => true;
    }
}

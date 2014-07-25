using Eto.Forms;
using Eto.Drawing;

namespace Trigger.XForms.Visuals
{
    public class LogonViewTemplate : Form
    {
        public LogonViewTemplate()
        {
            Size = new Size(240, 200);
            Title = "Logon";
		
            Content = new LogonViewGenerator(this).GetContent();
        }

        protected override void OnLoadComplete(System.EventArgs e)
        {
            base.OnLoadComplete(e);

            BringToFront();
        }
    }
}

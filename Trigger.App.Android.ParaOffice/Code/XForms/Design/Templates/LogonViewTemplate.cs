using Eto.Forms;
using Eto.Drawing;

namespace XForms.Design
{
    public class LogonViewTemplate : Dialog
    {
   
        public LogonViewTemplate()
        {
            Size = new Size(240, 180);
            Title = "Logon";
            Content = new LogonViewGenerator(this).GetContent();
        }

        public override void OnLoadComplete(System.EventArgs e)
        {
            base.OnLoadComplete(e);

            BringToFront();
        }
    }
}

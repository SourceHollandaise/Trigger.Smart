using Eto.Forms;
using Eto.Drawing;

namespace Trigger.XForms.Visuals
{
    public class LogonViewTemplate : Dialog
    {
   
        public LogonViewTemplate()
        {
            Size = new Size(240, 180);
            Title = "Logon";
		
            //this.BackgroundColor = Colors.WhiteSmoke;
            Content = new LogonViewGenerator(this).GetContent();
        }

        public override void OnLoadComplete(System.EventArgs e)
        {
            base.OnLoadComplete(e);

            BringToFront();
        }
    }
}

using Eto.Forms;
using Eto.Drawing;


namespace XForms.Design
{
    public class LogonViewTemplate : Dialog
    {
   
        public LogonViewTemplate()
        {
            Size = new Size(300, 200);
            Title = "Logon";
            Content = new LogonViewGenerator(this).GetContent(this.Width - 8);
           
            //DisplayMode = DialogDisplayMode.Attached;
        }

        protected override void OnLoadComplete(System.EventArgs e)
        {
            base.OnLoadComplete(e);

            BringToFront();
        }
    }
}

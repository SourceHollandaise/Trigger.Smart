using Eto.Forms;
using Eto.Drawing;
using Trigger.Datastore.Security;

namespace Trigger.WinForms.Layout
{
	public class LogonViewTemplate : Form
	{
		public LogonViewTemplate()
		{
			Size = new Size(240, 200);
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

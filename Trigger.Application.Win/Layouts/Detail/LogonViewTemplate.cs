using Eto.Forms;
using Eto.Drawing;
using Trigger.Datastore.Security;

namespace Trigger.WinForms.Layout
{
	public class LogonViewTemplate : Form
	{
		public LogonViewTemplate()
		{
			Size = new Size(340, 200);
			Title = "Logon";
		
			Content = new LogonViewGenerator(this).GetLayout();
		}

		public override void OnLoadComplete(System.EventArgs e)
		{
			base.OnLoadComplete(e);

			BringToFront();
		}
	}
}

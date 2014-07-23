using Eto.Forms;
using Eto.Drawing;
using Trigger.XStore.Security;

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

		public override void OnLoadComplete(System.EventArgs e)
		{
			base.OnLoadComplete(e);

			BringToFront();
		}
	}
}

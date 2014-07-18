using Eto.Forms;
using Trigger.CRM.Forms.Layout;

namespace Trigger.CRM.Forms.Templates
{
	public class LogonTemplate : Form
	{
		public LogonTemplate()
		{
			Size = new Eto.Drawing.Size(360, 240);
			Title = "Logon - Enter username and password";

			Content = new ModelLogonLayoutManager(this).GetLayout();
		}
	}
}

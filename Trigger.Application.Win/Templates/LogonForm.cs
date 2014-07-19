using Eto.Forms;
using Eto.Drawing;

namespace Trigger.WinForms.Layout
{
	public class LogonForm : Form
	{
		public LogonForm()
		{
			Size = new Size(360, 240);
			Title = "Logon - Enter username and password";

			Content = new ModelLogonLayoutManager().GetLayout();
		}
	}
}

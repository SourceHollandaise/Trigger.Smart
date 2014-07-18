using Eto.Forms;
using Trigger.Application.Win.Layouts;
using Eto.Drawing;

namespace Trigger.Application.Win.Templates
{
	public class LogonTemplate : Form
	{
		public LogonTemplate()
		{
			Size = new Size(360, 240);
			Title = "Logon - Enter username and password";

			Content = new ModelLogonLayoutManager(this).GetLayout();
		}
	}
}

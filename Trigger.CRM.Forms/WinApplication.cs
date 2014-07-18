using Eto.Forms;
using Trigger.CRM.Forms.Templates;
using Trigger.Datastore.Security;

namespace Trigger.CRM.Forms
{
	public static class WinApplication
	{
		static void Main()
		{
			new Bootstrapper().StartUpApplication();

			var application = new Application();

			application.Initialized += (sender, e) =>
			{
				application.MainForm = new LogonTemplate();
				application.MainForm.BringToFront();
				application.MainForm.Show();

			};

			application.Run();
		}
	}
}

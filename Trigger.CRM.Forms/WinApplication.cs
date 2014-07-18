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

			new DataStoreAuthenticate().LogOn(new LogonParameters { UserName = "Admin", Password = "admin" });

			new CRM.Services.DocumentUpdateService().LoadFromDocumentStore();

			application.Initialized += (sender, e) =>
			{
				application.MainForm = new MainForm();
				application.MainForm.Show();

			};

			application.Run();
		}
	}
}

using Trigger.CRM.Model;
using Trigger.WinForms.Layout;


namespace Trigger.CRM.Forms
{
	public static class WinApplication
	{
		static void Main()
		{
			new Bootstrapper().StartUpApplication();

			var application = new Eto.Forms.Application();

			var types = new []
			{
				typeof(IssueTracker),
				typeof(Project),
				typeof(Document),
				typeof(TimeTracker),

			};

			application.Initialized += (sender, e) =>
			{
				application.MainForm = new MainForm(types);
				application.MainForm.BringToFront();
				application.MainForm.Show();

			};

			application.Run();
		}
	}
}

using Eto.Forms;
using Trigger.Datastore.Security;

namespace Trigger.WinForms.Layout
{
	public class LogonViewGenerator
	{

		protected LogonViewTemplate LogonViewTemplate
		{
			get;
			set;
		}

		public LogonViewGenerator(LogonViewTemplate logonViewTemplate)
		{
			this.LogonViewTemplate = logonViewTemplate;
		}

		public DynamicLayout GetLayout()
		{
			DynamicLayout layout = new DynamicLayout();

			layout.BeginHorizontal();
			var textBoxUserName = new TextBox
			{
				PlaceholderText = "Set username..."
			};
			textBoxUserName.Size = new Eto.Drawing.Size(-1, -1);
			layout.Add(textBoxUserName, true);
			layout.EndHorizontal();

			layout.BeginHorizontal();
			var textBoxPassword = new TextBox
			{
				PlaceholderText = "Set password...",
			};
			textBoxPassword.Size = new Eto.Drawing.Size(-1, -1);

			layout.Add(textBoxPassword, true);
			layout.EndHorizontal();

			layout.BeginHorizontal();
			var logonButton = new Button()
			{
				Text = "Log on",
				Size = new Eto.Drawing.Size(30, -1)
			};
			layout.Add(logonButton, true);
			logonButton.Click += (sender, e) =>
			{
				var auth = Dependency.DependencyMapProvider.Instance.ResolveType<IAuthenticate>();

				if (auth.LogOn(new LogonParameters { UserName = textBoxUserName.Text, Password = textBoxPassword.Text }))
				{
					LogonViewTemplate.Close();
				}
			};

			layout.EndHorizontal();

			layout.BeginHorizontal();
			layout.EndHorizontal();

			return layout;
		}
	}
}
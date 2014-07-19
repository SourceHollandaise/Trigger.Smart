using Eto.Forms;
using Eto.Drawing;
using Trigger.Datastore.Security;
using Trigger.CRM.Model;

namespace Trigger.WinForms.Layout
{
	public class LogonForm : Form
	{
		public LogonForm()
		{
			Size = new Size(360, 240);
			Title = "Logon - Enter username and password";

			Content = GetLayout();
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
			textBoxPassword.Size = new Size(-1, -1);

			layout.Add(textBoxPassword, true);
			layout.EndHorizontal();

			layout.BeginHorizontal();
			var logonButton = new Button()
			{
				Text = "Log on",
			};
			layout.Add(logonButton, true);
			logonButton.Click += (sender, e) =>
			{
				var auth = Dependency.DependencyMapProvider.Instance.ResolveType<IAuthenticate>();

				if (auth.LogOn(new LogonParameters { UserName = textBoxUserName.Text, Password = textBoxPassword.Text }))
				{
					this.Close();
				}
			};

			layout.EndHorizontal();

			return layout;
		}
	}
}

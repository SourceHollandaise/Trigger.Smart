using Eto.Forms;
using Trigger.Datastore.Security;
using Trigger.CRM.Forms.Templates;

namespace Trigger.CRM.Forms.Layout
{
	public class ModelLogonLayoutManager
	{
		Form form;

		public ModelLogonLayoutManager(Form form)
		{
			this.form = form;
		}

		public DynamicLayout GetLayout()
		{
			DynamicLayout layout = new DynamicLayout();

			layout.BeginHorizontal();
			layout.Add(new Label
			{
				Text = "Username"
			});
			var textBoxUserName = new TextBox
			{
				PlaceholderText = "Set username..."
			};
			textBoxUserName.Size = new Eto.Drawing.Size(-1, -1);
			layout.Add(textBoxUserName, true);
			layout.EndHorizontal();

			layout.BeginHorizontal();
			layout.Add(new Label
			{
				Text = "Password"
			});
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
			};
			layout.Add(logonButton, true);
			logonButton.Click += (sender, e) =>
			{
				var auth = Dependency.DependencyMapProvider.Instance.ResolveType<IAuthenticate>();

				if (auth.LogOn(new LogonParameters { UserName = textBoxUserName.Text, Password = textBoxPassword.Text }))
				{
					var mainForm = new MainTemplate();
					mainForm.Show();
				}
			};

			layout.EndHorizontal();

			return layout;
		}
	}
}
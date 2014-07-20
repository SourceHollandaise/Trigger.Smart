using Eto.Forms;
using Eto.Drawing;
using Trigger.Datastore.Security;

namespace Trigger.WinForms.Layout
{
	public class LogonForm : Form
	{
		public LogonForm()
		{
			Size = new Size(340, 200);
			Title = "Logon - Enter username and password";
			
			Content = GetLayout();
		}

		public DynamicLayout GetLayout()
		{
			DynamicLayout layout = new DynamicLayout();

			layout.BeginHorizontal();
		
			var textBoxUserName = new TextBox
			{
				PlaceholderText = "Username"
			};
			textBoxUserName.Size = new Size(-1, -1);
			layout.Add(textBoxUserName, true);
			layout.EndHorizontal();

			layout.BeginHorizontal();

			var textBoxPassword = new TextBox
			{
				PlaceholderText = "Password",
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
			logonButton.Image = Bitmap.FromResource("Login_in32.png");
			logonButton.ImagePosition = ButtonImagePosition.Left;
			layout.EndHorizontal();

			layout.BeginHorizontal();
			layout.EndHorizontal();

			return layout;
		}
	}
}

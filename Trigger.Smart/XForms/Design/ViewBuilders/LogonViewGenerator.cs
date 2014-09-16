using Eto.Forms;
using Eto.Drawing;
using XForms.Dependency;
using XForms.Security;

namespace XForms.Design
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

        public DynamicLayout GetContent()
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
            var textBoxPassword = new PasswordBox
            {
                PasswordChar = '*'
            };
            textBoxPassword.Size = new Size(-1, -1);

            layout.Add(textBoxPassword, true);
            layout.EndHorizontal();

            layout.BeginHorizontal();
            var logonButton = new Button()
            {
                Text = "Log on",
                Size = new Size(-1, 34),
                Image = ImageExtensions.GetImage("lock_off", 16),
                ImagePosition = ButtonImagePosition.Left,
                BackgroundColor = Colors.WhiteSmoke
            };

            LogonViewTemplate.DefaultButton = logonButton;
        
            layout.Add(logonButton, true);
            logonButton.Click += (sender, e) =>
            {
                LogonViewTemplate.DialogResult = DialogResult.No;
               
                var auth = MapProvider.Instance.ResolveType<IAuthenticate>();

                if (auth.LogOn(new LogonParameters { UserName = textBoxUserName.Text, Password = textBoxPassword.Text }))
                {
                    LogonViewTemplate.DialogResult = DialogResult.Ok;

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
using Eto.Forms;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Security;

namespace Trigger.XForms.Visuals
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
            textBoxUserName.Size = new Eto.Drawing.Size(-1, -1);
            layout.Add(textBoxUserName, true);
            layout.EndHorizontal();

            layout.BeginHorizontal();
            var textBoxPassword = new PasswordBox
            {
                PasswordChar = '*'
            };
            textBoxPassword.Size = new Eto.Drawing.Size(-1, -1);

            layout.Add(textBoxPassword, true);
            layout.EndHorizontal();

            layout.BeginHorizontal();
            var logonButton = new Button()
            {
                Text = "Log on",
                Size = new Eto.Drawing.Size(-1, 40),
                Image = Eto.Drawing.ImageExtensions.GetImage("lock_off", 24),
                ImagePosition = ButtonImagePosition.Left
            };

            LogonViewTemplate.DefaultButton = logonButton;
        
            layout.Add(logonButton, true);
            logonButton.Click += (sender, e) =>
            {
                LogonViewTemplate.DialogResult = DialogResult.No;
               
                var auth = DependencyMapProvider.Instance.ResolveType<IAuthenticate>();

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
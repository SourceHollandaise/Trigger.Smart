using XForms.Design;

namespace XForms.Commands
{
    public class LogonCommand : ILogonCommand
    {
        public void Execute(TemplateBase template)
        {
            new LogonViewTemplate().ShowModal();
        }

        public string ID => "cmd_logoff";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.ImageAndText;

        public string Name => "Login";

        public string ImageName => "key2";

        public int Width => 100;

        public bool AllowExecute => true;

        public bool Visible => true;
    }

}

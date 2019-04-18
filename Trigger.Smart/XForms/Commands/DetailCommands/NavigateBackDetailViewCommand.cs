using XForms.Design;

namespace XForms.Commands
{

    public class NavigateBackDetailViewCommand : INavigateBackDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            if (args.CurrentObject != null)
            {
                if (args.CurrentObject.HasChanged || args.CurrentObject.IsNewObject)
                {
                    if (ConfirmationMessages.SaveObjectShow() == Eto.Forms.DialogResult.Ok)
                        args.CurrentObject.Save();
                }

                TemplateNavigator.Back();
            }
        }

        public string ID => "cmd_navigate_back";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Back";

        public string ImageName => "clock_history";

        public int Width => 34;

        public bool AllowExecute => TemplateNavigator.BackPossible;

        public bool Visible => true;
    }

}

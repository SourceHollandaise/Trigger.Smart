using XForms.Design;

namespace XForms.Commands
{

    public class CloseDetailViewCommand : ICloseDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            if (args.CurrentObject != null)
            {
                if (args.CurrentObject.HasChanged)// || args.CurrentObject.IsNewObject)
                {
                    if (ConfirmationMessages.SaveObjectShow() == Eto.Forms.DialogResult.Ok)
                        args.CurrentObject.Save();
                }
                args.CurrentObject.TryCloseDetailView();
            }
        }

        public string ID => "cmd_close";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Close";

        public string ImageName => "navigate_close";

        public int Width => 34;

        public bool AllowExecute => true;

        public bool Visible => true;
    }
}

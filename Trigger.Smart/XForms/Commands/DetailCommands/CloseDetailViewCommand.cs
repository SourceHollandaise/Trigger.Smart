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

        public string ID
        {
            get
            {
                return "cmd_close";
            }
        }

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.Image;
            }
        }

        public string Name
        {
            get
            {
                return "Close";
            }
        }

        public string ImageName
        {
            get
            {
                return "navigate_close";
            }
        }

        public int Width
        {
            get
            {
                return 34;
            }
        }

        public bool AllowExecute
        {
            get
            {
                return true;
            }
        }

        public bool Visible
        {
            get
            {
                return true;
            }
        }
    }
}

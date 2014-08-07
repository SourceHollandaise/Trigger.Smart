
namespace Trigger.XForms.Commands
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
                return "window_remove";
            }
        }

        public int Width
        {
            get
            {
                return 80;
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

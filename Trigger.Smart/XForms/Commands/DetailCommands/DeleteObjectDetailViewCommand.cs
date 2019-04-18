using Eto.Forms;
using XForms.Design;
using XForms.Model;

namespace XForms.Commands
{
    public class DeleteObjectDetailViewCommand : IDeleteObjectDetailViewCommand
    {
        DetailViewArguments detailViewArgs;

        public void Execute(DetailViewArguments args)
        {
            detailViewArgs = args;

            if (!AllowExecute)
            {
                ConfirmationMessages.NotAllowedShow();

                return;
            }
                
            if (ConfirmationMessages.DeleteObjectShow() == DialogResult.Ok)
            {
                args.CurrentObject.Delete();
                args.CurrentObject.TryCloseDetailView();
            }
        }

        public string ID => "cmd_delete";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Delete";

        public string ImageName => "delete";

        public int Width => 34;

        public bool AllowExecute
        {
            get
            {
                if (detailViewArgs.CurrentObject == null)
                    return false;

                if (detailViewArgs.CurrentObject is User)
                {
                    return ApplicationQuery.CurrentUserIsAdministrator;
                }

                return true;
            }
        }

        public bool Visible => true;
    }
}

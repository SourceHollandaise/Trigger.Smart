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

        public string ID
        {
            get
            {
                return "cmd_delete";
            }
        }

        public string Name
        {
            get
            {
                return "Delete";
            }
        }

        public string ImageName
        {
            get
            {
                return "remove";
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
                if (detailViewArgs.CurrentObject == null)
                    return false;

                if (detailViewArgs.CurrentObject is User)
                {
                    return ApplicationQuery.CurrentUserIsAdministrator;
                }

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

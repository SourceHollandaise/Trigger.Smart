using Eto.Forms;
using Trigger.BCL.Common.Model;

namespace Trigger.XForms.Commands
{
    public class DeleteObjectCommand : IDeleteObjectCommand
    {
        DetailViewArguments detailViewArgs;

        public void Execute(DetailViewArguments args)
        {
            detailViewArgs = args;

            if (!AllowExecute)
            {
                SecurityConfirmationMessages.DeleteObjectShow();
                return;
            }
                
            if (SecurityConfirmationMessages.DeleteObjectShow() == DialogResult.Ok)
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

        public bool AllowExecute
        {
            get
            {
                if (detailViewArgs.CurrentObject == null)
                    return false;

                if (detailViewArgs.CurrentObject is User)
                {
                    return UserQuery.CurrentUserIsAdministrator;
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

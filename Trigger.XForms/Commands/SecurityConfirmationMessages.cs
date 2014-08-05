using Eto.Forms;

namespace Trigger.XForms.Commands
{

    public static class SecurityConfirmationMessages
    {
        static readonly string securityWarningCaption = "Security Warning";

        public static DialogResult OpenObjectShow(string message = null)
        {
            return MessageBox.Show(message ?? "Your are not allowed to see user details! Please contact your administrator!", securityWarningCaption, MessageBoxButtons.OK, MessageBoxType.Warning);
        }

        public static DialogResult CreateObjectShow(string message = null)
        {
            return MessageBox.Show(message ?? "Create an user is not allowed! Please contact your administrator!", securityWarningCaption, MessageBoxButtons.OK, MessageBoxType.Warning);
        }

        public static DialogResult DeleteObjectShow(string message = null)
        {
            return MessageBox.Show(message ?? "Delete an user is not allowed! Please contact your administrator!", securityWarningCaption, MessageBoxButtons.OK, MessageBoxType.Warning);
        }
    }
}

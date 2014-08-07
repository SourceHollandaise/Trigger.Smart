using Eto.Forms;

namespace XForms.Commands
{

    public static class ConfirmationMessages
    {
        static readonly string securityWarningCaption = "Security Warning";


        public static void NotAllowedShow(string message = null)
        {
            MessageBox.Show(message ?? "Your are not allowed! Please contact your administrator!", securityWarningCaption, MessageBoxButtons.OK, MessageBoxType.Warning);
        }

        public static void OpenObjectShow(string message = null)
        {
            MessageBox.Show(message ?? "Your are not allowed to see details! Please contact your administrator!", securityWarningCaption, MessageBoxButtons.OK, MessageBoxType.Warning);
        }

        public static void CreateObjectShow(string message = null)
        {
            MessageBox.Show(message ?? "Create entry is not allowed! Please contact your administrator!", securityWarningCaption, MessageBoxButtons.OK, MessageBoxType.Warning);
        }

        public static DialogResult DeleteObjectShow(string message = null)
        {
            return MessageBox.Show(message ?? "Delete entry?", "Delete", MessageBoxButtons.OKCancel, MessageBoxType.Question);
        }

        public static DialogResult SaveObjectShow(string message = null)
        {
            return MessageBox.Show(message ?? "Save current entry?", "Save", MessageBoxButtons.OKCancel, MessageBoxType.Question);
        }
    }
}

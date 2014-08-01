using Eto.Forms;

namespace Trigger.XForms.Commands
{
    public class DeleteObjectCommand : IDeleteObjectCommand
    {
        public void Execute(DetailViewArguments args)
        {
            var result = MessageBox.Show("Delete current object?", "Delete", MessageBoxButtons.OKCancel, MessageBoxType.Warning);
            if (result == DialogResult.Ok)
                args.CurrentObject.Delete();
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
    }
}

using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Commands
{
    public class DeleteObjectCommand : IDeleteObjectCommand
    {
        public void Execute(DetailViewArguments args)
        {
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
                return "Delete16";
            }
        }
    }
}

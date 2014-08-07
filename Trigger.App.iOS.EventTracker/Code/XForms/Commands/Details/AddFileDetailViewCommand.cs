using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Commands
{
    public class AddFileDetailViewCommand : IAddFileDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            var service = DependencyMapProvider.Instance.ResolveType<IFileDataService>();
            if (args.CurrentObject != null)
            {
                var fileDialog = new OpenFileDialog();
                fileDialog.MultiSelect = false;
                if (fileDialog.ShowDialog(null) == DialogResult.Ok)
                {
                    service.AddFile(args.CurrentObject as IFileData, fileDialog.FileName);

                    args.CurrentObject.Save();
                    args.CurrentObject.ReloadObject();
                }
            }
        }

        public string ID
        {
            get
            {
                return "cmd_add_file";
            }
        }

        public string Name
        {
            get
            {
                return "Attachment";
            }
        }

        public string ImageName
        {
            get
            {
                return "attachment";
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

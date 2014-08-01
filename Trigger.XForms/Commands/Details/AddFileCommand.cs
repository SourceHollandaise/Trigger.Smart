using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Commands
{
    public class AddFileCommand : IAddFileCommand
    {
        public void Execute(IStorable current)
        {
            var service = DependencyMapProvider.Instance.ResolveType<IFileDataService>();
            if (current != null)
            {
                var fileDialog = new OpenFileDialog();
                fileDialog.MultiSelect = false;
                if (fileDialog.ShowDialog(null) == DialogResult.Ok)
                {
                    service.AddFile(current as IFileData, fileDialog.FileName);
                    fileDialog.Dispose();
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
                return "Paperclip16";
            }
        }
    }
}

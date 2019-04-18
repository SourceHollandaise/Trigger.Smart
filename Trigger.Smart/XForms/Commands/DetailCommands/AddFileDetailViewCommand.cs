using Eto.Forms;
using XForms.Dependency;
using XForms.Store;
using XForms.Design;

namespace XForms.Commands
{
    public class AddFileDetailViewCommand : IAddFileDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            var service = MapProvider.Instance.ResolveType<IFileDataService>();
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

        public string ID => "cmd_add_file";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Attachment";

        public string ImageName => "document_attachment";

        public int Width => 34;

        public bool AllowExecute => true;

        public bool Visible => true;
    }
}

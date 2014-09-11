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

        public string ID
        {
            get
            {
                return "cmd_add_file";
            }
        }

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.Image;
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
                return "document_attachment";
            }
        }

        public int Width
        {
            get
            {
                return 34;
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

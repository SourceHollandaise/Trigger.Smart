using System;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.EventTracker.Services;
using XForms.Commands;
using XForms.Design;
using XForms.Dependency;
using XForms.Store;
using Eto.Forms;
using System.IO;
using System.Linq;

namespace Trigger.BCL.EventTracker
{
    public class AddMultipleFilesDetailViewCommand : IAddMultipleFilesDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            var service = MapProvider.Instance.ResolveType<IFileDataService>();

            var fileDialog = new OpenFileDialog();
            fileDialog.MultiSelect = true;

            if (fileDialog.ShowDialog(null) == DialogResult.Ok)
            {
                var store = MapProvider.Instance.ResolveType<IStore>();
 
                foreach (var fileName in fileDialog.Filenames)
                {
                    var fi = new FileInfo(fileName);

                    var item = store.LoadAll<ImageItem>().FirstOrDefault(p => p.FileName.ToLowerInvariant() == fi.Name.ToLowerInvariant());
                    if (item == null)
                        item = new ImageItem();

                    item.Gallery = args.CurrentObject as ImageGallery;
                   
                    service.AddFile(item, fileName);

                    item.Save();
                }
              
                args.CurrentObject.Save();
                args.CurrentObject.ReloadObject();
               
            }
        }

        public string ID
        {
            get
            {
                return "cmd_add_multiple_files";
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
                return "Add files";
            }
        }

        public string ImageName
        {
            get
            {
                return "photos";
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

using XForms.Commands;
using XForms.Store;
using System.Collections.Generic;
using XForms.Design;
using System.Linq;

namespace XForms.Commands
{


    public class FileViewerListViewCommand : IFileViewerListViewCommand
    {
        public void Execute(ListViewArguments listParameter)
        {
            if (listParameter.InputData is IListViewDescriptor)
            {
                var descriptor = listParameter.InputData as IListViewDescriptor;

                if (descriptor.FilePreviewMode == FileDataMode.None)
                    return;

                var storables = new DataStoreProvider(descriptor, listParameter.TargetType).CreateRawDataSet(listParameter.CustomDataSet);
                if (storables.Any())
                {
                    var fileDataItems = storables.Cast<IFileData>();

                    if (descriptor.FilePreviewMode == FileDataMode.SlideShow)
                    {
                        var preview = new ImagePreviewControl(fileDataItems);
                        preview.Show();
                        preview.BringToFront();
                    }

                    if (descriptor.FilePreviewMode == FileDataMode.FilePreview || descriptor.FilePreviewMode == FileDataMode.MixedMode)
                    {
                        var preview = new FilePreviewControl(fileDataItems);
                        preview.Show();
                        preview.BringToFront();
                    }
                }
            }
        }

        public string ID => "cmd_image_slideshow";

        public string Name => "Slideshow";

        public string ImageName => "presentation";

        public int Width => 34;

        public bool AllowExecute => true;

        public bool Visible => true;

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;
    }
}

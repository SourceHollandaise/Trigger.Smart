using XForms.Commands;
using XForms.Store;
using System.Collections.Generic;
using XForms.Design;
using System.Linq;

namespace XForms.Commands
{

    public class SlideShowListViewCommand : ISlideShowListViewCommand
    {

        public void Execute(ListViewArguments listParameter)
        {
            if (listParameter.InputData is IListViewDescriptor)
            {
                var descriptor = listParameter.InputData as IListViewDescriptor;
                var storables = new DataStoreProvider(descriptor, listParameter.TargetType).CreateRawDataSet(listParameter.CustomDataSet) as IEnumerable<IStorable>;
                if (storables.Any())
                {
                    var fileDataItems = storables.Cast<IFileData>();

                    var slider = new SlideShowControl(fileDataItems);
                    slider.Show();
                    slider.BringToFront();
                }
            }
        }

        public string ID
        {
            get
            {
                return "cmd_image_slideshow";
            }
        }

        public string Name
        {
            get
            {
                return "Slideshow";
            }
        }

        public string ImageName
        {
            get
            {
                return "presentation";
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

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.Image;
            }
        }
    }
}

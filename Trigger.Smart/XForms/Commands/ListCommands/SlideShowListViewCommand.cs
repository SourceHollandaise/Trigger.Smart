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
                var dummy = new DataStoreProvider(descriptor, listParameter.TargetType).CreateRawDataSet(listParameter.CustomDataSet) as IEnumerable<IStorable>;
                if (dummy.Any())
                {
                    var fileDataItems = dummy.Cast<IFileData>();

                    var slider = new SlideShowTemplate(fileDataItems);
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

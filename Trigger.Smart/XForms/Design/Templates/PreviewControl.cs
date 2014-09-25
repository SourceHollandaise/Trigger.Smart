using Eto.Drawing;
using Eto.Forms;

namespace XForms.Design
{
    public abstract class PreviewControl : Form
    {
        protected int CurrentIndex;
        protected Label FileLabel;

        protected Size DefaultButtonSize = new Size(48, 48);
        protected Color DefaultButtonBackColor = Colors.Gray;
      
        protected Button PlayButton;
        protected Button StopButton;
        protected Button NextButton;
        protected Button PreviousButton;
        protected Button RandomButton;
        protected Button LoopButton;
        protected Button OpenFileButton;
        protected Button OpenSourceFolderButton;
        protected Button StoreFileDataButton;

        protected PreviewControl()
        {
            CreateControlButtons();
        }

        protected void CreateControlButtons()
        {
            PlayButton = CreateButton("media_play");
            StopButton = CreateButton("media_stop");
            NextButton = CreateButton("media_step_forward");
            PreviousButton = CreateButton("media_step_back");
            RandomButton = CreateButton("photos");
            LoopButton = CreateButton("nav_refresh");
            OpenFileButton = CreateButton("document_attachment");
            OpenSourceFolderButton = CreateButton("folder3_document");
            StoreFileDataButton = CreateButton("floppy_disk");
        }

        protected virtual Button CreateButton(string imageName)
        {       
            return new Button
            { 
                Size = DefaultButtonSize, 
                Image = ImageExtensions.GetImage(imageName),
                BackgroundColor = DefaultButtonBackColor,
                ImagePosition = ButtonImagePosition.Overlay
            };
        }
    }
}
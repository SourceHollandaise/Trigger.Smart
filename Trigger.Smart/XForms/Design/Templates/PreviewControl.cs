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

            AddButtonHandlers();
        }

        protected void CreateControlButtons()
        {
            PlayButton = CreateButton("media_play");
            StopButton = CreateButton("media_stop");
            NextButton = CreateButton("media_step_forward");
            PreviousButton = CreateButton("media_step_back");
            RandomButton = CreateButton("photos");
            LoopButton = CreateButton("nav_refresh");
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

        void AddButtonHandlers()
        {
            PlayButton.Click += (sender, e) => PlayPause();

            StopButton.Click += (sender, e) => Stop();

            NextButton.Click += (sender, e) => Next();

            PreviousButton.Click += (sender, e) => Previous();

            RandomButton.Click += (sender, e) => Random();

            LoopButton.Click += (sender, e) => Loop();

            OpenSourceFolderButton.Click += (sender, e) => AddImageSourceFolder();

            StoreFileDataButton.Click += (sender, e) => StoreImage();
        }

        protected Control GetFilePathContent()
        {
            var layout = new DynamicLayout();

            FileLabel = new Label()
            {
                Text = "",
                HorizontalAlign = HorizontalAlign.Center,
                VerticalAlign = VerticalAlign.Top,
            };

            try
            {
                FileLabel.Font = new Font(FileLabel.Font.Family, FileLabel.Font.Size, FileLabel.Font.FontStyle, FontDecoration.Underline);
            }
            catch
            {

            }

            FileLabel.MouseDoubleClick += (sender, e) => OpenFile();

            layout.BeginVertical();
            layout.Add(FileLabel, true, false);
            layout.EndVertical();

            return layout;
        }

        protected virtual void OpenFile()
        {
          
        }

        protected virtual void PlayPause()
        {

        }

        protected virtual void Stop()
        {

        }

        protected virtual void Previous()
        {

        }

        protected virtual void Next()
        {

        }

        protected virtual void Random()
        {

        }

        protected virtual void Loop()
        {

        }

        protected virtual void AddImageSourceFolder()
        {

        }

        protected virtual void StoreImage()
        {

        }
    }
}
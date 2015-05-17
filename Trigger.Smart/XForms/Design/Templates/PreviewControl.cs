using Eto.Drawing;
using Eto.Forms;

namespace XForms.Design
{
    public abstract class PreviewControl : Form
    {
        protected int CurrentIndex;
        protected Label FileLabel;

        protected Size DefaultButtonSize = new Size(32, 32);
        protected Color DefaultButtonBackColor = Colors.Black;
      
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

            this.BackgroundColor = Colors.Black;
        }

        protected void CreateControlButtons()
        {
            PlayButton = CreateButton("media_play", "Play");
            StopButton = CreateButton("media_stop", "Stop");
            NextButton = CreateButton("media_step_forward", "Next");
            PreviousButton = CreateButton("media_step_back", "Previous");
            RandomButton = CreateButton("photos", "Shuffle");
            LoopButton = CreateButton("nav_refresh", "Replay all");
            OpenSourceFolderButton = CreateButton("folder3_document", "Add folder to preview");
            StoreFileDataButton = CreateButton("floppy_disk", "Save item");
        }

        protected virtual Button CreateButton(string imageName, string toolTip)
        {       
            return new Button
            { 
                Size = DefaultButtonSize, 
                Image = ImageExtensions.GetImage(imageName, 24),
                BackgroundColor = DefaultButtonBackColor,
                ImagePosition = ButtonImagePosition.Overlay,
                ToolTip = toolTip
            };
        }

        void AddButtonHandlers()
        {
            PlayButton.Click += (_, __) => PlayPause();

            StopButton.Click += (_, __) => Stop();

            NextButton.Click += (_, __) => Next();

            PreviousButton.Click += (_, __) => Previous();

            RandomButton.Click += (_, __) => Random();

            LoopButton.Click += (_, __) => Loop();

            OpenSourceFolderButton.Click += (_, __) => AddImageSourceFolder();

            StoreFileDataButton.Click += (_, __) => StoreImage();
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

            FileLabel.MouseDoubleClick += (_, __) => OpenFile();

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
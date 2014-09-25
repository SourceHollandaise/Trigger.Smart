using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;

namespace XForms.Design
{
    public class SlideShowControl : Form
    {
        bool isLoop, isRandom, isPlaying;
        int imageIndex;
        Label imageLabel;
        UITimer slideTimer;
        ImageView imageView;

        static Size defaultButtonSize = new Size(48, 48);
        static Color defaultButtonBackColor = Colors.Gray;

        readonly Dictionary<int, ImageItem> imageCollection;
        readonly IEnumerable<IFileData> files;

        readonly Button playButton = CreateControlButton("media_play");
        readonly Button stopButton = CreateControlButton("media_stop");
        readonly Button nextButton = CreateControlButton("media_step_forward");
        readonly Button previousButton = CreateControlButton("media_step_back");
        readonly Button randomButton = CreateControlButton("photos");
        readonly Button loopButton = CreateControlButton("nav_refresh");
        readonly Button openImageFileButton = CreateControlButton("document_attachment");
        readonly Button addImageSourceButton = CreateControlButton("folder3_document");
        readonly Button storeImageButton = CreateControlButton("floppy_disk");

        protected SlideShowControl()
        {
            //INFO: Should not be called
        }

        public SlideShowControl(IEnumerable<IFileData> files, bool autoStart = false)
        {
            this.WindowState = WindowState.Maximized;
            this.BackgroundColor = Colors.LightGrey;

            this.files = files;
            this.imageCollection = new Dictionary<int, ImageItem>();
            this.slideTimer = new UITimer();
            slideTimer.Interval = 3;
            slideTimer.Elapsed += (sender, e) => Next();

            AddControlButtonHandlers();

            this.Content = GetContent();

            if (autoStart)
                PlayPause();
        }

        public void PlayPause()
        {
            isPlaying = !isPlaying;

            if (isPlaying)
                slideTimer.Start();
            else
                slideTimer.Stop();

            if (isPlaying && (imageCollection == null || imageCollection.Count == 0))
                CreateCollection(isRandom);

            if (imageCollection.ContainsKey(imageIndex))
            {
                var item = imageCollection[imageIndex];
                if (item.Image != null)
                {
                    imageView.Image = item.Image;
                    imageLabel.Text = item.ImageFilePath;
                }
            }

            playButton.Image = isPlaying 
                ? ImageExtensions.GetImage("media_pause") 
                : ImageExtensions.GetImage("media_play");
        }

        public void Stop()
        {
            slideTimer.Stop();
        }

        public void Next()
        {
            if (imageCollection.ContainsKey(imageIndex + 1))
            {
                var item = imageCollection[imageIndex + 1];
                if (item.Image != null)
                {
                    imageView.Image = item.Image;
                    imageIndex = imageIndex + 1;
                    imageLabel.Text = item.ImageFilePath;
                }
            }
            else
            {
                slideTimer.Stop();

                if (isLoop)
                {
                    imageIndex = 0;
                    CreateCollection(isRandom);
                    Next();
                    slideTimer.Start();
                }
            }
        }

        public void Previous()
        {
            if (imageIndex <= 0)
                imageIndex = 0;

            if (imageIndex >= 1)
                imageIndex = imageIndex - 1;

            var item = imageCollection[imageIndex];
            if (item.Image != null)
            {
                imageView.Image = item.Image;
                imageLabel.Text = item.ImageFilePath;
            }
        }

        public void Loop()
        {
            isLoop = !isLoop;

            loopButton.BackgroundColor = isLoop ? Colors.CornflowerBlue : defaultButtonBackColor;

//            loopButton.Image = isLoop 
//                ? ImageExtensions.GetImage("nav_refresh") 
//                : ImageExtensions.GetImage("nav_refresh");
        }

        public void Random()
        {
            isRandom = !isRandom;

            CreateCollection(isRandom);

            randomButton.BackgroundColor = isRandom ? Colors.CornflowerBlue : defaultButtonBackColor;

//            randomButton.Image = isRandom 
//                ? ImageExtensions.GetImage("photo_landscape") 
//                : ImageExtensions.GetImage("photos");
        }

        public void OpenImageFile()
        {
            var currentItem = imageCollection[imageIndex];

            if (File.Exists(currentItem.ImageFilePath))
                System.Diagnostics.Process.Start(currentItem.ImageFilePath);
        }

        public void AddImageSourceFolder()
        {
            var folderBrowser = new SelectFolderDialog();
            folderBrowser.Title = "Select source";
            var result = folderBrowser.ShowDialog(this);
            if (result == DialogResult.Ok)
            {
                var info = new DirectoryInfo(folderBrowser.Directory);
                CreateCollectionFromSystem(isRandom, info.EnumerateFiles("*.*", SearchOption.AllDirectories));
            }
        }

        public void StoreImage()
        {
            var currentItem = imageCollection[imageIndex];
            if (File.Exists(currentItem.ImageFilePath))
            {
                var service = Dependency.MapProvider.Instance.ResolveType<IFileDataService>();
                if (service != null)
                    service.StoreFile(currentItem.ImageFilePath);
            }
        }

        Control GetContent()
        {
            var layout = new DynamicLayout();

            layout.Add(GetButtonsContent(), false, false);

            layout.Add(GetFilePathContent(), false, false);

            layout.BeginVertical();
            layout.EndVertical();

            layout.Add(GetImageViewContent());

            return layout;
        }

        Control GetFilePathContent()
        {
            var layout = new DynamicLayout();

            imageLabel = new Label()
            {
                Text = "",
                HorizontalAlign = HorizontalAlign.Center,
                VerticalAlign = VerticalAlign.Top,
            };

            try
            {
                imageLabel.Font = new Font(imageLabel.Font.Family, imageLabel.Font.Size, imageLabel.Font.FontStyle, FontDecoration.Underline);
            }
            catch
            {

            }

            imageLabel.MouseDoubleClick += (sender, e) => OpenImageFile();

            layout.BeginVertical();
            layout.Add(imageLabel, true, false);
            layout.EndVertical();

            return layout;
        }

        Control GetButtonsContent()
        {
            var layout = new DynamicLayout();

            layout.BeginHorizontal();
            layout.Add(null);
            layout.Add(playButton, false, false);
            layout.Add(previousButton, false, false);
            layout.Add(nextButton, false, false);
            layout.Add(stopButton, false, false);
            layout.Add(loopButton, false, false);
            layout.Add(randomButton, false, false);
            //layout.Add(openImageFileButton, false, false);
            layout.Add(addImageSourceButton, false, false);
            layout.Add(storeImageButton, false, false);
            layout.Add(null);
            layout.EndHorizontal();

            return layout;
        }

        Control GetImageViewContent()
        {
            imageView = new ImageView()
            {
                Size = this.Size
            };

            var layout = new DynamicLayout();

            layout.BeginVertical();
            layout.Add(imageView);
            layout.EndVertical();

            return layout;
        }

        static Button CreateControlButton(string imageName)
        {
            return new Button
            { 
                Size = defaultButtonSize, 
                Image = ImageExtensions.GetImage(imageName),
                BackgroundColor = defaultButtonBackColor,
                ImagePosition = ButtonImagePosition.Overlay
            };
        }

        void AddControlButtonHandlers()
        {
            playButton.Click += (sender, e) => PlayPause();

            stopButton.Click += (sender, e) => Stop();

            nextButton.Click += (sender, e) => Next();

            previousButton.Click += (sender, e) => Previous();

            randomButton.Click += (sender, e) => Random();

            loopButton.Click += (sender, e) => Loop();

            openImageFileButton.Click += (sender, e) => OpenImageFile();

            addImageSourceButton.Click += (sender, e) => AddImageSourceFolder();

            storeImageButton.Click += (sender, e) => StoreImage();
        }

        void CreateCollection(bool random)
        {
            imageCollection.Clear();

            if (random)
            {
                if (slideTimer.Started)
                    slideTimer.Stop();

                var index = GetRandomPosition();

                foreach (var item in files)
                {
                    var image = item.ConvertToImage();
                    if (image != null)
                    {
                        while (imageCollection.ContainsKey(index))
                            index = GetRandomPosition();
                        imageCollection.Add(index, new ImageItem { Image = image, ImageFilePath = item.FileName.GetValidPath() });
                    }
                }

                if (isPlaying)
                    slideTimer.Start();
            }
            else
            {
                var index = 0;
                foreach (var item in files)
                {
                    var image = item.ConvertToImage();
                    if (image != null)
                        imageCollection.Add(index++, new ImageItem { Image = image, ImageFilePath = item.FileName.GetValidPath() });
                }
            }
        }

        void CreateCollectionFromSystem(bool random, IEnumerable<FileInfo> files)
        {
            imageCollection.Clear();

            if (random)
            {
                if (slideTimer.Started)
                    slideTimer.Stop();

                var index = GetRandomPosition();

                foreach (var item in files)
                {
                    if (!item.IsSupportedImage())
                        continue;

                    var image = item.FullName.ConvertToImage();
                    if (image != null)
                    {
                        while (imageCollection.ContainsKey(index))
                            index = GetRandomPosition();

                        imageCollection.Add(index, new ImageItem { Image = image, ImageFilePath = item.FullName.GetValidPath() });
                    }
                }

                if (isPlaying)
                    slideTimer.Start();
            }
            else
            {
                var index = 0;
                foreach (var item in files)
                {
                    if (!item.IsSupportedImage())
                        continue;

                    var image = item.FullName.ConvertToImage();

                    if (image != null)
                        imageCollection.Add(index++, new ImageItem { Image = image, ImageFilePath = item.FullName.GetValidPath() });
                }
            }
        }

        int GetRandomPosition()
        {
            var random = new Random();
            int index = random.Next(0, files.Count());

            return index;
        }
    }
}
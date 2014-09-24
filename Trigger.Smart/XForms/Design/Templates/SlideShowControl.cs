using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;

namespace XForms.Design
{
    internal struct ImageItem
    {
        public Image Image
        {
            get;
            set;
        }

        public string ImageFilePath
        {
            get;
            set;
        }
    }

    public class SlideShowControl : Form
    {
        bool isLoop, isRandom, isPlaying;

        int currentImageIndex;

        UITimer slideTimer = new UITimer();

        ImageView imageView;

        IEnumerable<IFileData> files;

        static Size defaultButtonSize = new Size(48, 48);
        static Color defaultBackColor = Colors.Gray;

        Dictionary<int, ImageItem> imageCollection = new Dictionary<int, ImageItem>();

        Button playButton = CreateControlButton("media_play");
        Button stopButton = CreateControlButton("media_stop");
        Button nextButton = CreateControlButton("media_step_forward");
        Button previousButton = CreateControlButton("media_step_back");
        Button randomButton = CreateControlButton("photos");
        Button loopButton = CreateControlButton("nav_refresh");
        Button openImageFileButton = CreateControlButton("document_attachment");

        public SlideShowControl(IEnumerable<IFileData> files)
        {
            this.files = files;
            this.WindowState = WindowState.Maximized;
            this.BackgroundColor = Colors.LightGrey;

            slideTimer.Interval = 3;
            slideTimer.Elapsed += (sender, e) =>
            {
                Next();
            };

            AddControlButtonHandlers();

            this.Content = GetContent();
        }

        static Button CreateControlButton(string imageName)
        {
            return new Button
            { 
                Size = defaultButtonSize, 
                Image = ImageExtensions.GetImage(imageName),
                BackgroundColor = defaultBackColor,
                ImagePosition = ButtonImagePosition.Overlay
            };
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
            layout.Add(openImageFileButton, false, false);
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

            imageView.MouseUp += (sender, e) =>
            {
                if (e.Buttons == MouseButtons.Alternate)
                {
                    //TODO: Show ContextMenu
                }
            };

            var layout = new DynamicLayout();

            layout.BeginVertical();
            layout.Add(imageView);
            layout.EndVertical();

            return layout;
        }

        public Control GetContent()
        {
            var layout = new DynamicLayout();

            layout.Add(GetButtonsContent(), false, false);

            layout.BeginVertical();
            layout.EndVertical();

            layout.Add(GetImageViewContent());

            return layout;
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

        int GetRandomPosition()
        {
            var random = new Random();
            int index = random.Next(0, files.Count());

            return index;
        }

        void AddControlButtonHandlers()
        {
            playButton.Click += (sender, e) =>
            {
                PlayPause();

                playButton.Image = isPlaying 
                    ? ImageExtensions.GetImage("media_pause") 
                    : ImageExtensions.GetImage("media_play");
            };

            stopButton.Click += (sender, e) =>
            {
                Stop();
            };

            nextButton.Click += (sender, e) =>
            {
                Next();
            };
                
            previousButton.Click += (sender, e) =>
            {
                Previous();
            };
                
            randomButton.Click += (sender, e) =>
            {
                Random();

                randomButton.Image = isRandom 
                    ? ImageExtensions.GetImage("photo_landscape") 
                    : ImageExtensions.GetImage("photos");
            };

            loopButton.Click += (sender, e) =>
            {
                Loop();

                loopButton.Image = isLoop 
                    ? ImageExtensions.GetImage("") 
                    : ImageExtensions.GetImage("nav_refresh");
            };

            openImageFileButton.Click += (sender, e) =>
            {
                var currentItem = imageCollection[currentImageIndex];

                if (File.Exists(currentItem.ImageFilePath))
                    System.Diagnostics.Process.Start(currentItem.ImageFilePath);
            };
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

            if (imageCollection.ContainsKey(currentImageIndex))
            {
                var item = imageCollection[currentImageIndex];
                if (item.Image != null)
                {
                    imageView.Image = item.Image;
                }
            }
        }

        public void Stop()
        {
            slideTimer.Stop();
        }

        public void Next()
        {
            if (imageCollection.ContainsKey(currentImageIndex + 1))
            {
                var item = imageCollection[currentImageIndex + 1];
                if (item.Image != null)
                {
                    imageView.Image = item.Image;
                    currentImageIndex = currentImageIndex + 1;
                }
            }
            else
            {
                slideTimer.Stop();

                if (isLoop)
                {
                    currentImageIndex = 0;
                    CreateCollection(isRandom);
                    Next();
                    slideTimer.Start();
                }
            }
        }

        public void Previous()
        {
            if (currentImageIndex <= 0)
                currentImageIndex = 0;

            if (currentImageIndex >= 1)
                currentImageIndex = currentImageIndex - 1;

            var item = imageCollection[currentImageIndex];
            if (item.Image != null)
            {
                imageView.Image = item.Image;
            }
        }

        public void Loop()
        {
            isLoop = !isLoop;
        }

        public void Random()
        {
            isRandom = !isRandom;

            CreateCollection(isRandom);
        }
    }
}
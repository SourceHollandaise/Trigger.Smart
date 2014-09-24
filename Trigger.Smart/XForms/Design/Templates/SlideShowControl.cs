using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;
using System;

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

        static Size defaultButtonSize = new Size(100, 40);

        Dictionary<int, ImageItem> imageCollection = new Dictionary<int, ImageItem>();

        Button playButton = new Button(){ Text = "PLAY", Size = defaultButtonSize };
        Button stopButton = new Button(){ Text = "STOP", Size = defaultButtonSize };
        Button nextButton = new Button(){ Text = "NEXT", Size = defaultButtonSize };
        Button previousButton = new Button(){ Text = "PREVIOUS", Size = defaultButtonSize };
        Button randomButton = new Button(){ Text = "RANDOM ON", Size = defaultButtonSize };
        Button loopButton = new Button(){ Text = "LOOP ON", Size = defaultButtonSize };

        public SlideShowControl(IEnumerable<IFileData> files)
        {
            this.files = files;
            this.WindowState = WindowState.Maximized;
            this.BackgroundColor = Colors.DarkSlateGray;

            slideTimer.Interval = 3;
            slideTimer.Elapsed += (sender, e) =>
            {
                Next();
            };

            AddControlButtonHandlers();

            this.Content = GetContent();
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
            if (random)
            {
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

            }
            else
            {
                imageCollection.Clear();

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
                if (isPlaying)
                    playButton.Text = "PAUSE";
                else
                    playButton.Text = "PLAY";
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
                if (isRandom)
                    randomButton.Text = "RANDOM OFF";
                else
                    randomButton.Text = "RANDOM ON";
            };

            loopButton.Click += (sender, e) =>
            {
                Loop();

                if (isLoop)
                    loopButton.Text = "LOOP OFF";
                else
                    loopButton.Text = "LOOP ON";
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
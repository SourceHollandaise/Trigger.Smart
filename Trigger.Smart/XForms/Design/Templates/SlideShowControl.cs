using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;
using System;

namespace XForms.Design
{
    public class SlideShowControl : Form
    {
        UITimer slideTimer = new UITimer();

        ImageView imageView;

        IEnumerable<IFileData> files;

        Dictionary<int, Image> imageCollection = new Dictionary<int, Image>();

        Button playButton = new Button(){ Text = "PLAY", Size = new Size(120, 40) };
        Button stopButton = new Button(){ Text = "STOP", Size = new Size(120, 40) };
        Button nextButton = new Button(){ Text = "NEXT", Size = new Size(120, 40) };
        Button previousButton = new Button(){ Text = "PREVIOUS", Size = new Size(120, 40) };
        Button randomButton = new Button(){ Text = "RANDOM ON", Size = new Size(120, 40) };
        Button loopButton = new Button(){ Text = "LOOP ON", Size = new Size(120, 40) };

        bool isLoop, isRandom, isPlaying;

        int currentImageIndex;

        public SlideShowControl(IEnumerable<IFileData> files)
        {
            this.files = files;
            imageView = new ImageView();
            imageView.Size = new Size(-1, 600);

            slideTimer.Interval = 2.5;
            slideTimer.Elapsed += (sender, e) =>
            {
                Next();
            };

            this.LastImageShown += (sender, e) =>
            {
                if (isLoop)
                {
                    CreateCollection(isRandom);
                    Next();
                }
            };

            AddControlButtonHandlers();

            this.Content = GetContent();
        }

        public Control GetContent()
        {
            var layout = new DynamicLayout();
            layout.BeginVertical();
            layout.Add(imageView, false, true);
            layout.EndVertical();

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

        void CreateCollection(bool random)
        {
            if (random)
            {
                var index = GetRandomPosition();

                foreach (var item in files)
                {
                    var image = Convert(item);
                    if (image != null)
                    {
                        while (imageCollection.ContainsKey(index))
                            index = GetRandomPosition();
                        imageCollection.Add(index, image);
                    }
                }

            }
            else
            {
                imageCollection.Clear();

                var index = 0;
                foreach (var item in files)
                {
                    var image = Convert(item);
                    if (image != null)
                        imageCollection.Add(index++, image);
                }
            }
        }

        int GetRandomPosition()
        {
            var random = new System.Random();
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
                var image = imageCollection[currentImageIndex];
                if (image != null)
                {
                    imageView.Image = image;
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
                var image = imageCollection[currentImageIndex + 1];
                if (image != null)
                {
                    imageView.Image = image;
                    currentImageIndex = currentImageIndex + 1;
                }
            }
            else
            {
                slideTimer.Stop();

                OnLastImageShown();
            }

        }

        public event EventHandler LastImageShown;

        protected virtual void OnLastImageShown()
        {
            var handler = LastImageShown;
            if (handler != null)
                handler(this, new EventArgs());
        }

        public void Previous()
        {
            var image = imageCollection[currentImageIndex - 1];
            if (image != null)
            {
                imageView.Image = image;
                currentImageIndex = currentImageIndex - 1;
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

        Image Convert(IFileData fileData)
        {
            var value = fileData.FileName;
            if (!string.IsNullOrWhiteSpace(value))
            {
                var file = value.GetValidPath();
                if (file != null)
                {
                    try
                    {
                        var image = new Bitmap(file);

                        return image;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            return null;
        }
    }
}
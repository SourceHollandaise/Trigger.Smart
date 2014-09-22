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
        UITimer slideTimer = new UITimer();

        ImageView imageView;

        IEnumerable<IFileData> images;

        Dictionary<int, Image> imageCollection = new Dictionary<int, Image>();

        Button playButton = new Button(){ Text = "PLAY/PAUSE", Size = new Size(120, 40) };
        Button stopButton = new Button(){ Text = "STOP", Size = new Size(120, 40) };
        Button nextButton = new Button(){ Text = "NEXT", Size = new Size(120, 40) };
        Button previousButton = new Button(){ Text = "PREVIOUS", Size = new Size(120, 40) };
        Button randomButton = new Button(){ Text = "RANDOM", Size = new Size(120, 40) };
        Button loopButton = new Button(){ Text = "LOOP", Size = new Size(120, 40) };

        bool isLoop, isRandom, isPlaying, isCollectionCreated;

        int currentImageIndex;

        public SlideShowControl(ImageView imageView, IEnumerable<IFileData> images)
        {
            this.images = images;
            this.imageView = imageView;

            slideTimer.Interval = 2.5;
            slideTimer.Elapsed += (sender, e) =>
            {
                Next();
            };


            AddControlButtonHandlers();
        }

        void CreateCollection()
        {
            imageCollection.Clear();

            var index = 0;
            foreach (var item in images)
            {
                var image = Convert(item);
                if (image != null)
                    imageCollection.Add(index++, image);
            }
        }

        void CreateRandomCollection()
        {
            imageCollection.Clear();

            var index = GetRandomPosition();
 
            foreach (var item in images)
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

        int GetRandomPosition()
        {
            var random = new System.Random();
            int index = random.Next(0, images.Count());

            return index;
        }

        void AddControlButtonHandlers()
        {
            playButton.Click += (sender, e) =>
            {
                PlayPause();
            };

            stopButton.Click += (sender, e) =>
            {
                Stop();
            };

            nextButton.Click += (sender, e) =>
            {
                Next();
            };

            previousButton.Size = new Size(60, 60);
            previousButton.Click += (sender, e) =>
            {
                Previous();
            };

            randomButton.Click += (sender, e) =>
            {
                Random();
            };

            loopButton.Click += (sender, e) =>
            {
                Loop();
            };
        }

        public void PlayPause()
        {
            isPlaying = !isPlaying;

            if (isPlaying)
                slideTimer.Start();
            else
                slideTimer.Stop();

            var image = imageCollection[currentImageIndex];
            if (image != null)
            {
                imageView.Image = image;
            }
        }

        public void Stop()
        {
            slideTimer.Stop();
        }

        public void Next()
        {
            var image = imageCollection[currentImageIndex + 1];
            if (image != null)
            {
                imageView.Image = image;
                currentImageIndex = currentImageIndex + 1;
            }
            else
            {
                if (isLoop)
                {
                    //TODO: Add loop
                }
            }
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

            if (isLoop)
            {
                if (currentImageIndex > imageCollection.Count)
                {
                    currentImageIndex = 0;
                    Next();
                }
            }
        }

        public void Random()
        {
            isRandom = !isRandom;

            if (isRandom)
            {
                Next();
            }
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
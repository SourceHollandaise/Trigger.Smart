using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;

namespace XForms.Design
{
    public class SlideShowControl : PreviewControl
    {
        bool isLoop, isRandom, isPlaying;

        UITimer slideTimer;
        ImageView imageView;

        readonly Dictionary<int, ImageItem> imageCollection;
        readonly IEnumerable<IFileData> files;

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

            if (imageCollection.ContainsKey(CurrentIndex))
            {
                var item = imageCollection[CurrentIndex];
                if (item.Image != null)
                {
                    imageView.Image = item.Image;
                    FileLabel.Text = item.ImageFilePath;
                }
            }

            PlayButton.Image = isPlaying 
                ? ImageExtensions.GetImage("media_pause") 
                : ImageExtensions.GetImage("media_play");
        }

        public void Stop()
        {
            slideTimer.Stop();
        }

        public void Next()
        {
            if (imageCollection.ContainsKey(CurrentIndex + 1))
            {
                var item = imageCollection[CurrentIndex + 1];
                if (item.Image != null)
                {
                    imageView.Image = item.Image;
                    CurrentIndex = CurrentIndex + 1;
                    FileLabel.Text = item.ImageFilePath;
                }
            }
            else
            {
                slideTimer.Stop();

                if (isLoop)
                {
                    CurrentIndex = 0;
                    CreateCollection(isRandom);
                    Next();
                    slideTimer.Start();
                }
            }
        }

        public void Previous()
        {
            if (CurrentIndex <= 0)
                CurrentIndex = 0;

            if (CurrentIndex >= 1)
                CurrentIndex = CurrentIndex - 1;

            var item = imageCollection[CurrentIndex];
            if (item.Image != null)
            {
                imageView.Image = item.Image;
                FileLabel.Text = item.ImageFilePath;
            }
        }

        public void Loop()
        {
            isLoop = !isLoop;

            LoopButton.BackgroundColor = isLoop ? Colors.CornflowerBlue : DefaultButtonBackColor;
        }

        public void Random()
        {
            isRandom = !isRandom;

            CreateCollection(isRandom);

            RandomButton.BackgroundColor = isRandom ? Colors.CornflowerBlue : DefaultButtonBackColor;
        }

        public void OpenImageFile()
        {
            var currentItem = imageCollection[CurrentIndex];

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
            var currentItem = imageCollection[CurrentIndex];
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

            FileLabel.MouseDoubleClick += (sender, e) => OpenImageFile();

            layout.BeginVertical();
            layout.Add(FileLabel, true, false);
            layout.EndVertical();

            return layout;
        }

        Control GetButtonsContent()
        {
            var layout = new DynamicLayout();

            layout.BeginHorizontal();
            layout.Add(null);
            layout.Add(PlayButton, false, false);
            layout.Add(PreviousButton, false, false);
            layout.Add(NextButton, false, false);
            layout.Add(StopButton, false, false);
            layout.Add(LoopButton, false, false);
            layout.Add(RandomButton, false, false);
            layout.Add(OpenSourceFolderButton, false, false);
            layout.Add(StoreFileDataButton, false, false);
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

        void AddControlButtonHandlers()
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
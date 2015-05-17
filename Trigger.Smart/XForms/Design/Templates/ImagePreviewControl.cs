using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;

namespace XForms.Design
{
    public class ImagePreviewControl : PreviewControl
    {
        bool isLoop, isRandom, isPlaying;

        UITimer slideTimer;
        ImageView imageView;

        readonly Dictionary<int, ImagePreviewItem> imageCollection;
        readonly IEnumerable<IFileData> fileDataItems;

        protected ImagePreviewControl()
        {
        }

        public ImagePreviewControl(IEnumerable<IFileData> fileDataItems, bool autoStart = false) : base()
        {
            this.WindowState = WindowState.Maximized;

            this.fileDataItems = fileDataItems;
            this.imageCollection = new Dictionary<int, ImagePreviewItem>();
            this.slideTimer = new UITimer();
            slideTimer.Interval = 3;
            slideTimer.Elapsed += (_, __) => Next();

            this.Content = GetContent();

            if (autoStart)
                PlayPause();
        }

        protected override void PlayPause()
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

            PlayButton.Image = isPlaying ? ImageExtensions.GetImage("media_pause") : ImageExtensions.GetImage("media_play");

            PlayButton.ToolTip = isPlaying ? "Pause" : "Play";
        }

        protected override void Stop()
        {
            slideTimer.Stop();
        }

        protected override void Next()
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

        protected override void Previous()
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

        protected override void Loop()
        {
            isLoop = !isLoop;

            LoopButton.BackgroundColor = isLoop ? Colors.CornflowerBlue : DefaultButtonBackColor;
            LoopButton.ToolTip = isLoop ? "End repeat all" : "Repeat all";
        }

        protected override void Random()
        {
            isRandom = !isRandom;

            CreateCollection(isRandom);

            RandomButton.BackgroundColor = isRandom ? Colors.CornflowerBlue : DefaultButtonBackColor;
            RandomButton.ToolTip = isRandom ? "Unshuffle" : "Shuffle";
        }

        protected override void AddImageSourceFolder()
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

        protected override void StoreImage()
        {
            var currentItem = imageCollection[CurrentIndex];
            if (File.Exists(currentItem.ImageFilePath))
            {
                var service = Dependency.MapProvider.Instance.ResolveType<IFileDataService>();
                if (service != null)
                    service.StoreFile(currentItem.ImageFilePath);
            }
        }

        protected override void OpenFile()
        {
            var currentItem = imageCollection[CurrentIndex];

            if (File.Exists(currentItem.ImageFilePath))
                System.Diagnostics.Process.Start(currentItem.ImageFilePath);
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

        void CreateCollection(bool random)
        {
            imageCollection.Clear();

            if (random)
            {
                if (slideTimer.Started)
                    slideTimer.Stop();

                var index = GetRandomPosition();

                foreach (var item in fileDataItems)
                {
                    var image = item.ConvertToImage();
                    if (image != null)
                    {
                        while (imageCollection.ContainsKey(index))
                            index = GetRandomPosition();
                        imageCollection.Add(index, new ImagePreviewItem { Image = image, ImageFilePath = item.FileName.GetValidPath() });
                    }
                }

                if (isPlaying)
                    slideTimer.Start();
            }
            else
            {
                var index = 0;
                foreach (var item in fileDataItems)
                {
                    var image = item.ConvertToImage();
                    if (image != null)
                        imageCollection.Add(index++, new ImagePreviewItem { Image = image, ImageFilePath = item.FileName.GetValidPath() });
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

                        imageCollection.Add(index, new ImagePreviewItem { Image = image, ImageFilePath = item.FullName.GetValidPath() });
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
                        imageCollection.Add(index++, new ImagePreviewItem { Image = image, ImageFilePath = item.FullName.GetValidPath() });
                }
            }
        }

        int GetRandomPosition()
        {
            var random = new Random();
            int index = random.Next(0, fileDataItems.Count());

            return index;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;

namespace XForms.Design
{
    public class FilePreviewControl : PreviewControl
    {
        WebView fileView;

        readonly Dictionary<int, FileItem> fileCollection;
        readonly IEnumerable<IFileData> files;

        protected FilePreviewControl()
        {
            //INFO: Should not be called
        }

        public FilePreviewControl(IEnumerable<IFileData> files)
        {
            this.WindowState = WindowState.Maximized;
            this.BackgroundColor = Colors.LightGrey;

            this.files = files;
            this.fileCollection = new Dictionary<int, FileItem>();

            AddControlButtonHandlers();

            this.Content = GetContent();

            CreateCollection();

        }

        public void Next()
        {
            if (fileCollection.ContainsKey(CurrentIndex + 1))
            {
                var item = fileCollection[CurrentIndex + 1];
                if (item.FilePath != null)
                {
                    fileView.Url = new Uri(item.FilePath);
                    CurrentIndex = CurrentIndex + 1;
                    FileLabel.Text = item.FilePath;
                }
            }
        }

        public void Previous()
        {
            if (CurrentIndex <= 0)
                CurrentIndex = 0;

            if (CurrentIndex >= 1)
                CurrentIndex = CurrentIndex - 1;

            var item = fileCollection[CurrentIndex];
            if (item.FilePath != null)
            {
                fileView.Url = new Uri(item.FilePath);
                FileLabel.Text = item.FilePath;
            }
        }

        public void OpenFile()
        {
            var currentItem = fileCollection[CurrentIndex];

            if (File.Exists(currentItem.FilePath))
                System.Diagnostics.Process.Start(currentItem.FilePath);
        }

        public void AddImageSourceFolder()
        {
            var folderBrowser = new SelectFolderDialog();
            folderBrowser.Title = "Select source";
            var result = folderBrowser.ShowDialog(this);
            if (result == DialogResult.Ok)
            {
                var info = new DirectoryInfo(folderBrowser.Directory);
                CreateCollectionFromSystem(info.EnumerateFiles("*.*", SearchOption.AllDirectories));
            }
        }

        public void StoreImage()
        {
            var currentItem = fileCollection[CurrentIndex];
            if (File.Exists(currentItem.FilePath))
            {
                var service = Dependency.MapProvider.Instance.ResolveType<IFileDataService>();
                if (service != null)
                    service.StoreFile(currentItem.FilePath);
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

            FileLabel.MouseDoubleClick += (sender, e) => OpenFile();

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
            layout.Add(PreviousButton, false, false);
            layout.Add(NextButton, false, false);
            //layout.Add(openImageFileButton, false, false);
            layout.Add(OpenSourceFolderButton, false, false);
            layout.Add(StoreFileDataButton, false, false);
            layout.Add(null);
            layout.EndHorizontal();

            return layout;
        }

        Control GetImageViewContent()
        {
            fileView = new WebView()
            {
                Size = this.Size
            };

            var layout = new DynamicLayout();

            layout.BeginVertical();
            layout.Add(fileView);
            layout.EndVertical();

            return layout;
        }

        void AddControlButtonHandlers()
        {

            NextButton.Click += (sender, e) => Next();

            PreviousButton.Click += (sender, e) => Previous();

            OpenSourceFolderButton.Click += (sender, e) => AddImageSourceFolder();

            StoreFileDataButton.Click += (sender, e) => StoreImage();
        }

        void CreateCollection()
        {
            fileCollection.Clear();
          
            var index = 0;
            foreach (var item in files)
            {
                var info = new FileInfo(item.FileName);

                if (!info.IsSupportedDocument())
                    continue;

                fileCollection.Add(index++, new FileItem { FilePath = item.FileName.GetValidPath(), Info = info });
            }

            StartUp();
        }

        void CreateCollectionFromSystem(IEnumerable<FileInfo> files)
        {
            fileCollection.Clear();

            var index = 0;
            foreach (var item in files)
            {
                if (!item.IsSupportedDocument())
                    continue;
                    
                fileCollection.Add(index++, new FileItem { FilePath = item.FullName.GetValidPath(), Info = item });
            }

            StartUp();
        }

        void StartUp()
        {
            if (fileCollection != null && fileCollection.Count > 0)
            {
                CurrentIndex = 0;
                var item = fileCollection[0];

                fileView.Url = new Uri(item.FilePath);
                FileLabel.Text = item.FilePath;
            }
        }
    }
}
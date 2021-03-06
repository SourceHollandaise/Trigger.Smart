using System.Collections.Generic;
using System.IO;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;
using System;

namespace XForms.Design
{
    [Obsolete("Use SlideShowControl instead!", false)]
    public class SlideShowTemplate : Form
    {
        bool loop;

        readonly IEnumerable<IFileData> fileDataItems;

        public readonly ImageView ImageView;

        Stack<IFileData> stack = new Stack<IFileData>();

        UITimer slideTimer = new UITimer();

        public SlideShowTemplate(IEnumerable<IFileData> fileDataItems, double interval = 3F, bool loop = true)
        {
            this.loop = loop;
            this.fileDataItems = fileDataItems;
            this.BackgroundColor = Colors.Black;
            this.WindowState = Eto.Forms.WindowState.Maximized;

            ImageView = new ImageView();
            ImageView.Size = new Size(-1, -1);

            this.Content = ImageView;

            AddImagesToStack();

            slideTimer = new UITimer
            {
                Interval = interval
            };

            slideTimer.Start();

            slideTimer.Elapsed += (sender, e) => ShowContentFromStack();

            ImageView.MouseDoubleClick += (sender, e) => ShowContentFromStack();

            ShowContentFromStack();
        }

        void AddImagesToStack()
        {
            if (fileDataItems == null)
                this.Close();

            foreach (var item in fileDataItems)
            {
                stack.Push(item);
            }
        }

        void ShowContentFromStack()
        {
            if (stack.Count == 0)
            {
                if (loop)
                {
                    AddImagesToStack();
                }
                else
                {
                    slideTimer.Stop();
                    this.Close();
                }
            }

            if (stack.Count > 0)
                ChangeContent(stack.Pop());
        }

        void ChangeContent(IFileData fileData)
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

                        ImageView.Image = image;
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
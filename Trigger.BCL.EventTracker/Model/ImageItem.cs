
using System.IO;
using XForms.Model;
using XForms.Store;
using Eto.Drawing;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Name")]
    [System.ComponentModel.DisplayName("Image")]
    [ImageName("photo_landscape")]
    public class ImageItem : StorableBase, IFileData, IThumbnailPreviewable, ITaggable
    {
        public override string GetSearchString()
        {
            var search = Subject + ";" + FileName + ";" + Gallery != null ? Gallery.Name : "";

            if (Keywords != null)
            {
                var splitted = Keywords.Split(new char[ ]{ ' ', ';', ',' }, System.StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in splitted)
                {
                    search += item + ";";   
                }
            }

            return search;
        }

        public override string GetRepresentation
        {
            get
            {
                return Subject;
            }
        }

        string keywords;

        public string Keywords
        {
            get
            {
                return keywords;
            }
            set
            {
                if (Equals(keywords, value))
                    return;
                keywords = value;

                OnPropertyChanged();
            }
        }

        string subject;

        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                if (Equals(subject, value))
                    return;
                subject = value;

                OnPropertyChanged();
            }
        }

        string fileName;

        [FieldImageData]
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                if (Equals(fileName, value))
                    return;
                fileName = value;

                OnPropertyChanged();
            }
        }

        [FieldImageData(true, 296, 296)]
        public string PreviewFileName
        {
            get
            {
                return FileName;
            }
        }

        Image thumbnail;

        [System.Runtime.Serialization.IgnoreDataMember]
        public Image Thumbnail
        {
            get
            {
                if (thumbnail == null)
                {
                    var fileName = FileName.GetValidPath();
                    if (File.Exists(fileName))
                        thumbnail = new Bitmap(FileName.GetValidPath());
                }
                    
                return thumbnail;
            }
        }


        public string GalleryAlias
        {
            get
            {
                return Gallery != null ? Gallery.Name : null;
            }
        }

        ImageGallery gallery;

        [LinkedObject]
        public ImageGallery Gallery
        {
            get
            {
                return gallery;
            }
            set
            {
                if (Equals(gallery, value))
                    return;
                gallery = value;

                OnPropertyChanged();
               
            }
        }
    }
}

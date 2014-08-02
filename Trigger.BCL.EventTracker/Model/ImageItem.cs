using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Datastore;
using Trigger.XForms;
using Eto.Drawing;

namespace Trigger.BCL.EventTracker.Model
{

    [System.ComponentModel.DefaultProperty("Name")]
    [System.ComponentModel.DisplayName("Gallery")]
    public class ImageItem : StorableBase, IFileData
    {

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

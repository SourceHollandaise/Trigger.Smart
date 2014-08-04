using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Datastore;
using Trigger.XForms;
using System.Collections.Generic;
using System.Linq;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Name")]
    [System.ComponentModel.DisplayName("Gallery")]
    public class ImageGallery : StorableBase
    {
        string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Equals(name, value))
                    return;
                name = value;

                OnPropertyChanged();
            }
        }

        string description;

        [FieldTextArea]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (Equals(description, value))
                    return;
                description = value;

                OnPropertyChanged();
            }
        }

        bool privateGallery;

        public bool PrivateGallery
        {
            get
            {
                return privateGallery;
            }
            set
            {
                if (Equals(privateGallery, value))
                    return;
                privateGallery = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Linked images")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(ImageItem))]
        public IEnumerable<ImageItem> LinkedImageItems
        {
            get
            {
                return Store.LoadAll<ImageItem>().Where(p => p.Gallery != null && p.Gallery.MappingId.Equals(MappingId));
            }
        }
    }
}

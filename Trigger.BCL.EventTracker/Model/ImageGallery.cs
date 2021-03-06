using System.Collections.Generic;
using System.Linq;
using XForms.Model;
using XForms.Store;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Name")]
    [System.ComponentModel.DisplayName("Gallery")]
    [ImageName("photos")]
    public class ImageGallery : StorableBase
    {
        public override string GetRepresentation
        {
            get
            {
                return Name;
            }
        }

        public override string GetSearchString()
        {
            return Name + Description;
        }

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

        ApplicationUser owner;

        [LinkedObject]
        public ApplicationUser Owner
        {
            get
            {
                return owner;
            }
            set
            {
                if (Equals(owner, value))
                    return;
                owner = value;

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

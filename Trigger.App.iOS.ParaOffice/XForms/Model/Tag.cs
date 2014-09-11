
namespace XForms.Model
{
    [System.ComponentModel.DefaultProperty("Name")]
    [System.ComponentModel.DisplayName("Tag")]
    public class Tag : StorableBase
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

        string targetObjectMappingId;

        public string TargetObjectMappingId
        {
            get
            {
                return targetObjectMappingId;
            }
            set
            {
                if (Equals(targetObjectMappingId, value))
                    return;
                targetObjectMappingId = value;

                OnPropertyChanged();
            }
        }

        string tagColor;

        public string TagColor
        {
            get
            {
                return tagColor;
            }
            set
            {
                if (Equals(tagColor, value))
                    return;
                tagColor = value;

                OnPropertyChanged();
            }
        }
    }
    
}

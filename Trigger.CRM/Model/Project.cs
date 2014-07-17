
namespace Trigger.CRM.Model
{
    public class Project : ModelBase
    {
        public override string GetRepresentation()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format("{0}", Name));
            sb.AppendLine(string.Format("{0}", Description));
            sb.AppendLine(string.Format("ID: {0}", MappingId));
            return sb.ToString();
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
    }
}

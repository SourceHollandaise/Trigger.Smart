using System.Collections.Generic;
using System.Linq;
using XForms.Model;
using XForms.Store;

namespace Trigger.BCL.EventTracker.Model
{

    [System.ComponentModel.DefaultProperty("Name")]
    [System.ComponentModel.DisplayName("Area")]
    [ImageName("application")]
    public class Area : StorableBase
    {
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

        [System.ComponentModel.DisplayName("Linked documents")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Document))]
        public IEnumerable<Document> LinkedDocuments
        {
            get
            {
                return Store.LoadAll<Document>().Where(p => p.Area != null && p.Area.MappingId.Equals(MappingId));
            }
        }

        [System.ComponentModel.DisplayName("Linked issues")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(IssueTracker))]
        public IEnumerable<IssueTracker> LinkedIssues
        {
            get
            {
                return Store.LoadAll<IssueTracker>().Where(p => p.Area != null && p.Area.MappingId.Equals(MappingId));
            }
        }

        [System.ComponentModel.DisplayName("Linked users")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(ApplicationUser))]
        public IEnumerable<ApplicationUser> LinkedAreaUsers
        {
            get
            {
                var areaUsers = Store.LoadAll<AreaUser>().Where(p => p.Area != null && p.User != null && p.Area.MappingId.Equals(MappingId));
                foreach (var item in  areaUsers)
                {
                    yield return item.User;
                }
            }
        }
    }
}

using Trigger.BCL.Common.Datastore;
using Trigger.XForms;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Area")]
    [System.ComponentModel.DisplayName("Area")]
    public class AreaUser : StorableBase
    {
        public string AreaAlias
        {
            get
            {
                return Area != null ? Area.Name : null;
            }
        }

        Area area;

        [LinkedObject]
        public Area Area
        {
            get
            {
                return area;
            }
            set
            {
                if (Equals(area, value))
                    return;
                area = value;

                OnPropertyChanged();
            }
        }

        public string UserAlias
        {
            get
            {
                return User != null ? User.UserName : null;
            }
        }

        ApplicationUser user;

        [System.ComponentModel.DisplayName("User")]
        [LinkedObject]
        public ApplicationUser User
        {
            get
            {
                return user;
            }
            set
            {
                if (Equals(user, value))
                    return;
                user = value;

                OnPropertyChanged();
            }
        }
    }
}

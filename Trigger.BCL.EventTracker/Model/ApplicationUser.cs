using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker.Model
{

    [System.ComponentModel.DefaultProperty("UserName")]
    [System.ComponentModel.DisplayName("User")]
    public class ApplicationUser : User
    {
        [System.ComponentModel.DisplayName("Linked areas")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Area))]
        public IEnumerable<Area> LinkedAreas
        {
            get
            {
                var areaUsers = Store.LoadAll<AreaUser>().Where(p => p.Area != null && p.User != null && p.User.MappingId.Equals(MappingId));
                foreach (var item in areaUsers)
                {
                    yield return item.Area;
                }
            }
        }
    }
}

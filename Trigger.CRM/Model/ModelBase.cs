using System.Linq;
using System.ComponentModel;
using Trigger.Dependency;
using Trigger.CRM.Persistent;

namespace Trigger.CRM.Model
{
    public abstract class ModelBase : INotifyPropertyChanged, IPersistentId
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public object Id
        {
            get;
            set;
        }

        protected IDependencyMap Map
        {
            get
            {
                return DependencyMapProvider.Instance;
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }
    }
}

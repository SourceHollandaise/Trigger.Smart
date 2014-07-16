using System.ComponentModel;
using Trigger.Dependency;
using Trigger.CRM.Persistent;
using System.Runtime.CompilerServices;
using System;

namespace Trigger.CRM.Model
{
    public abstract class ModelBase : INotifyPropertyChanged, IStorable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public object MappingId
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System.ComponentModel;
using Trigger.Dependency;
using Trigger.CRM.Persistent;
using System.Runtime.CompilerServices;
using System.Text;

namespace Trigger.CRM.Model
{
    public interface IStringRepresentation
    {
        string GetRepresentation();
    }

    public abstract class ModelBase : INotifyPropertyChanged, IStorable, IStringRepresentation
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public object MappingId
        {
            get;
            set;
        }

        public void Save()
        {
            Store.Save(GetType(), this);
        }

        public void Delete()
        {
            Store.Delete(GetType(), this);
        }

        public virtual string GetRepresentation()
        {
            return MappingId.ToString();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected IDependencyMap Map
        {
            get
            {
                return DependencyMapProvider.Instance;
            }
        }

        protected IStore Store
        {
            get
            {
                return Map.ResolveType<IStore>();
            }
        }
    }
}

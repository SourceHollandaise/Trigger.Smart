using Trigger.XStorable.Dependency;
using Trigger.XStorable.DataStore;

namespace Trigger.XStorable.Model
{
    public abstract class StorableBase : NotifyPropertyChangedBase, IStorable
    {
        [VisibleOnView(TargetView.None)]
        public virtual string GetRepresentation
        {
            get
            {
                return MappingId != null ? MappingId.ToString() : string.Empty;
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.DisplayName("ID")]
        [VisibleOnView(TargetView.None)]
        public object MappingId
        {
            get;
            set;
        }

        public virtual void Initialize()
        {
          
        }

        public virtual void Save()
        {
            UpdatePersistentReferences();
            Store.Save(GetType(), this);
        }

        public virtual void Delete()
        {
            Store.Delete(GetType(), this);
        }

        protected virtual void UpdatePersistentReferences()
        {
            LinkedObjectHelper.UpdatePersistentReferences(this);
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

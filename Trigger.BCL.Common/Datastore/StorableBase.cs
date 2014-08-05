using Trigger.XStorable.Dependency;
using Trigger.XStorable.DataStore;
using System;

namespace Trigger.BCL.Common.Datastore
{
    public abstract class StorableBase : NotifyPropertyChangedBase, IStorable
    {
        public virtual string GetRepresentation
        {
            get
            {
                var displayAttribute = GetType().FindAttribute<System.ComponentModel.DisplayNameAttribute>();
                if (displayAttribute != null)
                    return displayAttribute.DisplayName;

                return MappingId != null ? MappingId.ToString() : string.Empty;
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        [System.ComponentModel.DisplayName("ID")]
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
            LinkedObjectHelper.UpdateStoredReferences(this);
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var storable = obj as StorableBase;

            return storable != null && Equals(storable);
        }

        public override int GetHashCode()
        {
            if (MappingId == null)
                return base.GetHashCode();
            return MappingId.GetHashCode();
        }

        public int CompareTo(IStorable storable)
        {
            if (storable == null)
                return 1;

            var attrThis = GetType().FindAttribute<System.ComponentModel.DefaultPropertyAttribute>();
            var attrOther = storable.GetType().FindAttribute<System.ComponentModel.DefaultPropertyAttribute>();

            if (attrThis != null && attrOther != null)
                return string.Compare(attrThis.Name, attrOther.Name, StringComparison.Ordinal);

            return -1;
        }

        public bool Equals(IStorable other)
        {
            return MappingId.Equals(other.MappingId);
        }

    }
}

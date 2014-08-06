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

        public bool HasChanged { get; protected set; }

        bool isNewObject;

        public bool IsNewObject
        { 
            get
            { 
                isNewObject = MappingId == null;
                return isNewObject; 
            }
            protected set
            {
                isNewObject = value;
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

      
        public virtual void OnLoaded()
        {
            HasChanged = false;

            PropertyChanged += ObjectHasChanged;
        }

        public virtual void Save()
        {
            UpdatePersistentReferences();
            Store.Save(GetType(), this);

            PropertyChanged -= ObjectHasChanged;
            HasChanged = false;
            IsNewObject = false;
            PropertyChanged += ObjectHasChanged;
        }

        public virtual void Delete()
        {
            Store.Delete(GetType(), this);
        }

        protected virtual void ObjectHasChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HasChanged = true;
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

            var attributeThis = GetType().FindAttribute<System.ComponentModel.DefaultPropertyAttribute>();
            var attributeOther = storable.GetType().FindAttribute<System.ComponentModel.DefaultPropertyAttribute>();

            if (attributeThis != null && attributeOther != null)
                return string.Compare(attributeThis.Name, attributeOther.Name, StringComparison.Ordinal);

            return -1;
        }

        public bool Equals(IStorable other)
        {
            if (MappingId == null)
                return false;

            if (other == null)
                return false;

            if (other.MappingId == null)
                return false;

            return MappingId.Equals(other.MappingId);
        }
    }
}

using System;
using System.Linq;
using Trigger.CRM.Model;
using Trigger.CRM.Persistent;
using Trigger.CRM.Security;
using Trigger.Dependency;

namespace Trigger.CRM.Persistent
{

    public abstract class PersistentModelBase : ModelBase, IPersistentId
    {
        public object MappingId
        {
            get;
            set;
        }

        DateTime? created;

        public DateTime? Created
        {
            get
            {
                return created;
            }
            private set
            {
                if (Equals(created, value))
                    return;
                created = value;

                OnPropertyChanged();
            }
        }

        User createdBy;

        public User CreatedBy
        {
            get
            {
                return createdBy;
            }
            private set
            {
                if (Equals(createdBy, value))
                    return;
                createdBy = value;

                OnPropertyChanged();
            }
        }

        public virtual void Initialize()
        {
            created = DateTime.Now;
            createdBy = Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
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

        public virtual string GetRepresentation()
        {
            return MappingId.ToString();
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

        void UpdatePersistentReferences()
        {
            var properties = GetType().GetProperties().AsEnumerable()
                .Where(p => p.GetCustomAttributes(typeof(PersistentReferenceAttribute), true).Length > 0).ToList();

            foreach (var property in properties)
            {
                var value = property.GetValue(this, null);

                if (value != null)
                {
                    var persistent = value as IPersistentId;
                    if (persistent != null)
                    {
                        var loaded = Store.Load(persistent.GetType(), persistent.MappingId);

                        if (loaded == null)
                        {
                            property.SetValue(this, null, null);
                        }
                    }
                }
            }
        }
    }
}

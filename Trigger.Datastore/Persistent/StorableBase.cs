using System;
using Trigger.Dependency;
using Trigger.Datastore.Security;
using System.Linq;
using System.Collections.Generic;

namespace Trigger.Datastore.Persistent
{
	public abstract class StorableBase : NotifyPropertyChangedBase, IStorable
	{
		[System.ComponentModel.ReadOnly(true)]
		[System.ComponentModel.DisplayName("ID")]
		public object MappingId
		{
			get;
			set;
		}

		DateTime? created;

		[System.ComponentModel.ReadOnly(true)]
		[VisibleOnView(TargetView.None)]
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

		[System.ComponentModel.DisplayName("Created by")]
		[System.Runtime.Serialization.IgnoreDataMember]
		[VisibleOnView(TargetView.None)]
		public string CreatedByAlias
		{
			get
			{
				return CreatedBy != null ? CreatedBy.UserName : null;
			}
		}

		User createdBy;

		[System.ComponentModel.ReadOnly(true)]
		[System.ComponentModel.DisplayName("Created by")]
		[LinkedObject]
		[VisibleOnView(TargetView.None)]
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
			Created = DateTime.Now;
			CreatedBy = Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
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

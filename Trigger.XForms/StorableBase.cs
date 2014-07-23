using System;
using System.Linq;
using System.Collections.Generic;
using Trigger.XStore.Security;
using Trigger.XStorable.Dependency;

namespace Trigger.XStorable.DataStore
{
	public abstract class StorableBase : NotifyPropertyChangedBase, IStorable
	{
		[System.ComponentModel.DisplayName("Mapping")]
		public virtual string GetRepresentation
		{
			get
			{
				return MappingId.ToString();
			}
		}

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

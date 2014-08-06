using System.ComponentModel;
using System;

namespace Trigger.XStorable.DataStore
{
    public interface IStorable : INotifyPropertyChanged, IEquatable<IStorable> , IComparable<IStorable>
    {
        object MappingId { get; set; }

        bool HasChanged { get; }

        string GetRepresentation { get; }

        void Save();

        void Delete();

        void Initialize();
    }
}

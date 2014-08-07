using System.ComponentModel;
using System;

namespace XForms.Store
{
    public interface IStorable : INotifyPropertyChanged, IEquatable<IStorable> , IComparable<IStorable>
    {
        object MappingId { get; set; }

        bool HasChanged { get; }

        bool IsNewObject { get; }

        string GetRepresentation { get; }

        void OnLoaded();

        void Save();

        void Delete();

        void Initialize();
    }
}

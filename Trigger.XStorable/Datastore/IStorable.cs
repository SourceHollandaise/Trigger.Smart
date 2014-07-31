using System.ComponentModel;

namespace Trigger.XStorable.DataStore
{
    public interface IStorable : INotifyPropertyChanged
    {
        object MappingId { get; set; }

        string GetRepresentation { get; }

        void Save();

        void Delete();

        void Initialize();
    }
}

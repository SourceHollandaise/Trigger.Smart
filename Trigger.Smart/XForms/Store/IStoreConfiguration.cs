
namespace XForms.Store
{
    public interface IStoreConfiguration
    {
        string DataStoreLocation { get; }

        string DocumentStoreLocation { get; }

        void InitStore();
    }
    
}

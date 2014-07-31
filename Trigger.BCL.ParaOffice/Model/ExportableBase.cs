using Trigger.BCL.Common.Datastore;

namespace Trigger.BCL.ParaOffice
{
    public abstract class ExportableBase : StorableBase
    {
        public object SourceId
        {
            get;
            set;
        }

        public string SourceTypeName
        {
            get;
            set;
        }
    }
}


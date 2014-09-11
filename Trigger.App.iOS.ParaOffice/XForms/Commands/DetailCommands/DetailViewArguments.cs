using XForms.Store;

namespace XForms.Commands
{
    public class DetailViewArguments
    {
        public IStorable CurrentObject
        {
            get;
            set;
        }

        public object InputData
        {
            get;
            set;
        }
    }
}

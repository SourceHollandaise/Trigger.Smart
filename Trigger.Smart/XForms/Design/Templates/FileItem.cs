using System.IO;

namespace XForms.Design
{
    internal struct FileItem
    {
        public FileInfo Info
        {
            get;
            set;
        }

        public string FilePath
        {
            get;
            set;
        }
    }
}
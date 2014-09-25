using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;

namespace XForms.Design
{
    internal struct ImageItem
    {
        public Image Image
        {
            get;
            set;
        }

        public string ImageFilePath
        {
            get;
            set;
        }
    }
    
}
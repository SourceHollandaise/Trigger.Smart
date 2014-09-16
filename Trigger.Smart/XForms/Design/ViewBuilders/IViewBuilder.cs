using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;

namespace XForms.Design
{
    public interface IViewBuilder
    {
        Control GetContent();
    }
    
}

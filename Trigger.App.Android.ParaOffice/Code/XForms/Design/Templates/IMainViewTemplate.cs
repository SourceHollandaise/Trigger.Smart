using Eto.Forms;
using System;

namespace XForms.Design
{
    public interface IMainViewTemplate
    {
        void SetContent(Control control);

        Type CurrentActiveType { get; set; }
    }
}

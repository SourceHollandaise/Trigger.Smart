using Eto.Forms;
using Trigger.XStorable.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Drawing;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{

    public class Link
    {
        public Type LinkType
        {
            get;
            set;
        }

        public PropertyInfo LinkProperty
        {
            get;
            set;
        }

        public IStorable SourceObject
        {
            get;
            set;
        }
    }
}

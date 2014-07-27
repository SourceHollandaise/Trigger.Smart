using System;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Eto.Drawing;

namespace Trigger.XForms.Visuals
{
    public class CreatableItem
    {
        public PropertyInfo Property
        {
            get;
            set;
        }

        public Control Control
        {
            get;
            set;
        }

        public string Group
        {
            get;
            set;
        }

        public int GroupIndex
        {
            get;
            set;
        }

        public int PropertyIndex
        {
            get;
            set;
        }
    }
}
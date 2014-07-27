using Eto.Forms;
using System.Reflection;

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
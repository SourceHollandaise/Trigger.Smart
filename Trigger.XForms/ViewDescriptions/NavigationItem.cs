using System.Collections.Generic;
using System;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Commands;

namespace Trigger.XForms
{

    public class NavigationItem
    {
        public Type ModelType { get; set; }

        public int Index { get; set; }

        public string NavigationItemText { get; set; }

        public string ImageName { get; set; }

        public NavigationItem(Type modelType, string navigationItemText, int index)
        {
            ModelType = modelType;
            NavigationItemText = navigationItemText;
            Index = index;
        }
    }
    
}

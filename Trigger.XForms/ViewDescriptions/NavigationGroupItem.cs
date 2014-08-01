using System.Collections.Generic;
using System;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Commands;

namespace Trigger.XForms
{

    public class NavigationGroupItem
    {
        public string NavigationGroupText { get; set; }

        public int Index { get; set; }

        public IList<NavigationItem> NavigationItems { get; set; }

        public NavigationGroupItem(string navigationGrouptext, int index)
        {
            NavigationGroupText = navigationGrouptext;
            Index = index;
        }
    }
}

using System.Collections.Generic;

namespace XForms.Design
{

    public class NavigationGroupItem
    {
        public string NavigationGroupText { get; set; }

        public int Index { get; set; }

        public IList<NavigationItem> NavigationItems { get; set; }

        public bool Visible { get; set; }

        public NavigationGroupItem(string navigationGrouptext, int index)
        {
            NavigationGroupText = navigationGrouptext;
            Index = index;
            Visible = true;
        }
    }
}

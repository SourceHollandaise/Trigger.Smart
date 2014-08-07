using Eto.Forms;
using System.Collections.Generic;

namespace Trigger.XForms.Visuals
{
    public static class TemplateNavigator
    {
        static Stack<Control> stack = new Stack<Control>();

        public static void Add(Control content)
        {
            stack.Push(content);
        }

        public static void Clear()
        {
            stack.Clear();
        }

        public static void Back()
        {
            if (stack.Count == 0)
                return;

            var content = stack.Pop();
            if (content != null)
            {
                if (content.Equals(MainTemplate.ContentPanel.Content))
                {
                    content = stack.Pop();
                }
                if (content != null)
                {
                    MainTemplate.ContentPanel.Content = content;
                }
            }
        }

        public static void Forward()
        {

        }

        static MainViewTemplate MainTemplate
        {
            get
            {
                return Application.Instance.MainForm as MainViewTemplate;
            }
        }
    }
}

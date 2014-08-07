using Eto.Forms;
using System.Collections.Generic;

namespace XForms.Design
{
    public static class TemplateNavigator
    {
        static Stack<Control> stack = new Stack<Control>();

        public static bool BackPossible
        {
            get
            {
                return stack.Count > 0;
            }
        }

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
    
            if (content.Equals(MainTemplate.ContentPanel.Content))
            {
                if (stack.Count > 0)
                    content = stack.Pop();
                else
                    return;
            }
          
            MainTemplate.ContentPanel.Content = content;

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

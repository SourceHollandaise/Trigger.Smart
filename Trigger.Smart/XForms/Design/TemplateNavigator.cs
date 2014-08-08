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
                
            stack.Pop();
            if (stack.Count > 0)
            {
                var content = stack.Peek();

                MainTemplate.ContentPanel.Content = content;
            }
        }

        public static void Forward()
        {
            throw new System.NotSupportedException("Forward ist currently not supported!");
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

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
            if (BackPossible)
            {
                var content = stack.Peek();

                if (content != null)
                {
                    if (Application.Instance.MainForm is MainViewTemplate)
                        (Application.Instance.MainForm as MainViewTemplate).ContentPanel.Content = content;

                    if (Application.Instance.MainForm is ReducedMainViewTemplate)
                        (Application.Instance.MainForm as ReducedMainViewTemplate).Content = content;

                }
            }
        }

        public static void Forward()
        {
            throw new System.NotSupportedException("Forward is currently not supported!");
        }
    }
}

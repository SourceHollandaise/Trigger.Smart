using System.Collections.Generic;
using System.Linq;
using System;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;
using XForms.Commands;

namespace XForms.Design
{

    public class ListDetailViewToolBarBuilder
    {
        public void Create(IEnumerable<IListViewCommand> commands, Type type, IEnumerable<IStorable> dataSet = null)
        {
            var toolBar = new ToolBar();
            toolBar.Dock = ToolBarDock.Top;

            foreach (var command in commands)
            {
                var item = new ButtonToolItem();
                item.Text = command.Name;
                item.ID = command.ID;
                item.Tag = command;

                if (!string.IsNullOrWhiteSpace(command.ImageName))
                {
                    item.Image = ImageExtensions.GetImage(command.ImageName, 24);
                }

                item.Click += (sender, e) =>
                {
                    var args = new ListViewArguments();
                    args.Grid = null;
                    args.TargetType = type;
                    args.CustomDataSet = dataSet;

                    (item.Tag as IListViewCommand).Execute(args);
                };

                toolBar.Items.Add(item);
            }

            Application.Instance.MainForm.ToolBar = toolBar;
        }
    }
}
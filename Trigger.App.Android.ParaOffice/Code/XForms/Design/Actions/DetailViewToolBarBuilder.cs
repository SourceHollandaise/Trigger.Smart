using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;
using XForms.Commands;

namespace XForms.Design
{

    public class DetailViewToolBarBuilder
    {
        public void Create(IEnumerable<IDetailViewCommand> commands, IStorable currentObject)
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
                    var args = new DetailViewArguments();
                    args.CurrentObject = currentObject;

                    (item.Tag as IDetailViewCommand).Execute(args);
                };

                toolBar.Items.Add(item);
            }

            Application.Instance.MainForm.ToolBar = toolBar;
        }
    }
}
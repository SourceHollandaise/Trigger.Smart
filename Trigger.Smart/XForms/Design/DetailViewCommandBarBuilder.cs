using System.Collections.Generic;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;
using XForms.Commands;

namespace XForms.Design
{

    public class DetailViewCommandBarBuilder
    {
        readonly IStorable currentObject;

        readonly IEnumerable<IDetailViewCommand> commands;

        readonly bool addTagCommands;

        public DetailViewCommandBarBuilder(IStorable currentObject, IEnumerable<IDetailViewCommand> commands, bool addTagCommands = false)
        {
            this.commands = commands;
            this.currentObject = currentObject;
            this.addTagCommands = addTagCommands;
        }

        public DynamicLayout GetContent()
        {
            var commandBarLayout = new DynamicLayout();
            commandBarLayout.BeginHorizontal();
            foreach (var command in commands.ToList())
            {
                var button = new Button();
                button.Size = new Size(command.Width, 34);
                button.ID = command.ID;
                button.ToolTip = command.Name;
                button.Text = command.Name;
                button.Click += (sender, e) =>
                {
                    command.Execute(new DetailViewArguments
                    {
                        CurrentObject = currentObject
                    });
                };
                commandBarLayout.Add(button, false, false);
            }

            commandBarLayout.Add(new DynamicLayout()
            {
                Size = new Size(-1, -1)
            });
           
            if (addTagCommands)
                new TagButtonBuilder(currentObject).AddTagButtonsContent(commandBarLayout);
             
            commandBarLayout.EndHorizontal();
            return commandBarLayout;
        }
    }
}
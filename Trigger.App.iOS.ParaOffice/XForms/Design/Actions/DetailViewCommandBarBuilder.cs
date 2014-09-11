using System.Collections.Generic;
using System.Linq;
using System;
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

        readonly bool rightToLeft;

        public DetailViewCommandBarBuilder(IStorable currentObject, IEnumerable<IDetailViewCommand> commands, bool addTagCommands = false, bool rightToLeft = false)
        {
            this.rightToLeft = rightToLeft;
            this.commands = commands;
            this.currentObject = currentObject;
            this.addTagCommands = addTagCommands;
        }

        public DynamicLayout GetContent()
        {
            var commandBarLayout = new DynamicLayout();
            commandBarLayout.BeginHorizontal();
            if (rightToLeft)
                commandBarLayout.Add(null);
            foreach (var command in commands.ToList())
            {
                var button = new Button();
                button.Size = new Size(command.Width, 34);
                button.ID = command.ID;
                button.ToolTip = command.Name;
                button.BackgroundColor = Colors.Gray;

                switch (command.DisplayStyle)
                {
                    case ButtonDisplayStyle.Image:
                        button.Image = ImageExtensions.GetImage(command.ImageName, 16);
                        button.ImagePosition = ButtonImagePosition.Above;
                        break;
                    case ButtonDisplayStyle.Text:
                        button.Text = command.Name;
            
                        break;
                    case ButtonDisplayStyle.ImageAndText:
                        button.Text = command.Name;
                        button.Image = ImageExtensions.GetImage(command.ImageName, 16);
                        button.ImagePosition = ButtonImagePosition.Left;
                        break;
                }

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
                new TagButtonBuilder(currentObject, rightToLeft).AddTagButtonsContent(commandBarLayout);
             
            commandBarLayout.EndHorizontal();
            return commandBarLayout;
        }
    }
}
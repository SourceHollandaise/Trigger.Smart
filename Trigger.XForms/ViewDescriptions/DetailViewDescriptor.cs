using System.Collections.Generic;
using System;
using Eto.Forms;
using Trigger.XForms.Controllers;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms
{
    public abstract class DetailViewDescriptor<TModel> : IDetailViewDescriptor
    {
        protected DetailViewDescriptor()
        {
            RegisterCommands<ISaveObjectCommand>();
            RegisterCommands<IDeleteObjectCommand>();
            RegisterCommands<ICloseWindowCommand>();
        }

        public IList<TabItemDescription> TabItemDescriptions { get; set; }

        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        public IList<IDetailViewCommand> Commands { get; set; }

        public void RegisterCommands<TCommand>() where  TCommand: IDetailViewCommand
        {
            if (Commands == null)
                Commands = new List<IDetailViewCommand>();

            var command = DependencyMapProvider.Instance.ResolveType<TCommand>();

            Commands.Add(command);
        }

        protected FieldNames<TModel> Fields = new FieldNames<TModel>();

        protected const string EmptySpaceFieldName = "EmptySpace";
    }
}


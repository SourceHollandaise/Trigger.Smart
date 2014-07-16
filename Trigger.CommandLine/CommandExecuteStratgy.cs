
using System;
using System.Linq;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;
using Trigger.CRM.Security;
using Trigger.Dependency;
using Trigger.CRM.Commands;

namespace Trigger.CommandLine
{

    public class CommandExecuteStratgy : ConsoleCommand
    {
        public static  void ExecuteCommand(string command)
        {
            if (command.ToLower().StartsWith(Commands.ADD.ToLower()))
            {
                ConsoleInsertUpdateCommand.InsertUpdateItems(command.ToLower().Replace(Commands.ADD.ToLower(), ""));
            }
            else if (command.ToLower().StartsWith(Commands.LST.ToLower()))
            {
                ConsoleSelectCommand.ListItems(command.ToLower().Replace(Commands.LST.ToLower(), ""));
            }
            else if (command.ToLower().Equals(Commands.EXIT.ToLower()))
            {
                Environment.Exit(0);
            }
            else if (command.ToLower().StartsWith(Commands.DEL.ToLower()))
            {
                var tmp = command.Replace(Commands.DEL.ToLower(), "");
                var splitted = tmp.Split(new []{ '>' }, StringSplitOptions.RemoveEmptyEntries);
                ConsoleDeleteCommand.DeleteItem(splitted[0], splitted[1]);
            }
        }
    }
}
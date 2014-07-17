
using System;
using Trigger.CommandLine.Commands;

namespace Trigger.CommandLine
{

    public class CommandExecuteStratgy : ConsoleBaseCommand
    {
        public static  void ExecuteCommand(string command)
        {
            if (command.ToLower().StartsWith(CommandKey.ADD.ToLower()))
            {
                ConsoleInsertUpdateCommand.InsertUpdateItems(command.ToLower().Replace(CommandKey.ADD.ToLower(), ""));
            }
            else if (command.ToLower().StartsWith(CommandKey.LST.ToLower()))
            {
                ConsoleSelectCommand.ListItems(command.ToLower().Replace(CommandKey.LST.ToLower(), ""));
            }
            else if (command.ToLower().Equals(CommandKey.EXIT.ToLower()))
            {
                Environment.Exit(0);
            }
            else if (command.ToLower().StartsWith(CommandKey.DEL.ToLower()))
            {
                var tmp = command.Replace(CommandKey.DEL.ToLower(), "");
                var splitted = tmp.Split(new []{ '>' }, StringSplitOptions.RemoveEmptyEntries);
                if (splitted.Length == 2)
                    ConsoleDeleteCommand.DeleteItem(splitted[0], splitted[1]);
            }
        }
    }
}
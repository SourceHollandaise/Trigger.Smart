
using System;
using System.Linq;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;
using Trigger.CRM.Security;
using Trigger.Dependency;
using Trigger.CRM.Commands;

namespace Trigger.CommandLine
{
    class CommandLineApplication
    {
        static IDependencyMap Map
        {
            get{ return DependencyMapProvider.Instance; }
        }

        public static void Main(string[] args)
        {
           
            new Bootstrapper().StartUpApplication();

            Console.WriteLine("CIT - COMMMANDLINE ISSUE TRACKER 0.1");
            Console.WriteLine("Trigger Smart Solutions");
            Console.WriteLine();

            ConsoleLogonCommand.LogonUser();

            Console.WriteLine("Cleanup datastore? Type <Enter> to clean up or any other key to continue!");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.WriteLine("Start cleaning...");
                XmlStoreUtils.RestoreTypeMap();
                Console.WriteLine("Finished cleaning...");
            }
            Console.WriteLine();
            Console.WriteLine("Adding new documents to store...");
            Console.WriteLine();
            var count = XmlStoreUtils.UpdateTypeMapForDocuments();
            Console.WriteLine(string.Format("{0} documents added!", count));
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine(string.Format("This is an overview for you {0}! Loading current open issues...", Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser.UserName));
            Console.WriteLine();
            foreach (var item in new IssueTrackerCommand().GetObjects(new Func<IssueTracker, bool>(p => !p.IsDone)).OrderBy(p => p.Created))
                Console.WriteLine(new IssueTrackerCommand().GetRepresentation(item));
            Console.WriteLine();
            Console.WriteLine("Press <Enter> to continue or <ESC> to exit!");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Type command...");
                CommandExecuteStratgy.ExecuteCommand(Console.ReadLine());
                Console.WriteLine("Press <Enter> to continue or <ESC> to exit!");
            }

            Environment.Exit(0);
        }



        static ConsoleColor GetColorOnIssueSate(IssueState state)
        {
            switch (state)
            {
                case IssueState.Open:
                    return ConsoleColor.Red;
                case IssueState.Accepted:
                case IssueState.InProgress:
                    return ConsoleColor.DarkYellow;               
                case IssueState.Done:
                    return ConsoleColor.Green;
                case IssueState.Rejected:
                    return ConsoleColor.Gray;
            }

            return ConsoleColor.White;
        }
    }

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
                var splitted = tmp.Split('-');
                ConsoleDeleteCommand.DeleteItem(splitted[0], splitted[1]);
            }
            else
                Console.WriteLine("Command not valid!");
        }
    }
}
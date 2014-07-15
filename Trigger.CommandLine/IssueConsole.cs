
using System;
using System.Linq;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;
using Trigger.CRM.Security;
using Trigger.Dependency;
using Trigger.CRM.Commands;

namespace Trigger.CommandLine
{
    class IssueConsole
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

            var currentUser = LogonUser();

            if (currentUser == null)
            {
                Console.WriteLine("Nice try! User does not exists!");
                Console.ReadKey();
                Environment.Exit(0);
            }

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
                ExecuteCommand();

                Console.WriteLine("Press <Enter> to continue or <ESC> to exit!");
            }

            Environment.Exit(0);
        }

        static User LogonUser()
        {
            User currentUser = null;
            var logon = new LogonParameters();
            var auth = Map.ResolveType<IAuthenticate>();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Username: ");
                logon.UserName = Console.ReadLine();
                Console.WriteLine("Password: ");
                logon.Password = Console.ReadLine();
                Console.WriteLine();

                if (auth.LogOn(logon))
                {
                    currentUser = Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
                    Console.WriteLine(string.Format("Hello {0}! You're successfully logged on!", currentUser.UserName));
                    break;
                }
                else
                {
                    Console.WriteLine("Username or password wrong! Try again!");
                }
            }

            return currentUser;
        }

        static void ExecuteCommand()
        {
            var user = Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser;

            Console.WriteLine("Type command...");
            var command = Console.ReadLine();
            {
                if (command.ToLower().StartsWith(Commands.ADD.ToLower()))
                {
                    ConsoleInsertUpdateCommands.InsertUpdateItems(command.ToLower().Replace(Commands.ADD.ToLower(), ""));
                }
                else if (command.ToLower().StartsWith(Commands.LST.ToLower()))
                {
                    ConsoleSelectCommands.ListItems(command.ToLower().Replace(Commands.LST.ToLower(), ""));
                }
                else if (command.ToLower().Equals(Commands.EXIT.ToLower()))
                {
                    Environment.Exit(0);
                }
                else if (command.ToLower().StartsWith(Commands.DEL.ToLower()))
                {
                    var tmp = command.Replace(Commands.DEL.ToLower(), "");
                    var splitted = tmp.Split('-');
                    ConsoleDeleteCommands.DeleteItem(splitted[0], splitted[1]);
                }
                else
                    Console.WriteLine("Command not valid!");

            }
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
}
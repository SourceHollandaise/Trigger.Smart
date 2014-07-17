
using System;
using Trigger.Datastore.Security;

namespace Trigger.CommandLine.Commands
{

    public class ConsoleLogonCommand : ConsoleBaseCommand
    {
        public static void LogonUser()
        {
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

                    Console.WriteLine(string.Format("Hello {0}! You're successfully logged on!", CurrentUser.UserName));
                    break;
                }
                else
                {
                    Console.WriteLine("Username or password wrong! Try again!");
                }
            }

            if (CurrentUser == null)
            {
                Console.WriteLine("Nice try! User does not exists! Application exit!");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
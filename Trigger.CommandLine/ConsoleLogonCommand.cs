
using Trigger.CRM.Security;
using Trigger.CRM.Model;
using System;

namespace Trigger.CommandLine
{

    public class ConsoleLogonCommand : ConsoleCommand
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
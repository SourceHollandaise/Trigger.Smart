
using System;
using System.Linq;
using Trigger.Datastore.Persistent;
using Trigger.CRM.Model;
using Trigger.Dependency;
using Trigger.CommandLine.Commands;
using Trigger.Datastore.Security;

namespace Trigger.CommandLine
{
	class CommandLineApplication
	{
		static IDependencyMap Map
		{
			get { return DependencyMapProvider.Instance; }
		}

		public static void Main(string[] args)
		{
			var bs = new Bootstrapper();
			bs.StartUpApplication();

			Console.WriteLine("CIT - COMMMANDLINE ISSUE TRACKER 0.1");
			Console.WriteLine("Trigger Smart Solutions");
			Console.WriteLine();

			ConsoleLogonCommand.LogonUser();

			Console.WriteLine();
			Console.WriteLine("Search for new documents...");
			Console.WriteLine();
			var count = new Trigger.CRM.Services.DocumentUpdateService().LoadFromDocumentStore();
			if (count == 0)
			{
				Console.WriteLine("No new documents found!");
			}
			else
			{
				Console.WriteLine("{0} documents added!", count);
				Console.WriteLine();
				Console.WriteLine("This is an overview for you {0}! Loading current open issues...", Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser.UserName);
				Console.WriteLine();
				foreach (var item in Map.ResolveType<IStore>().LoadAll<IssueTracker>().Where(p => !p.IsDone).OrderBy(p => p.Created))
					Console.WriteLine(item.GetRepresentation());

			}
				
			Console.WriteLine();
			Console.WriteLine("Press <Enter> to continue or <ESC> to exit!");

			while (Console.ReadKey().Key != ConsoleKey.Escape)
			{
				Console.WriteLine("Type command...");
				CommandExecuteStrategy.ExecuteCommand(Console.ReadLine());
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
}
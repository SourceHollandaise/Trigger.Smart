
using System;
using System.Linq;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;
using Trigger.CRM.Security;
using Trigger.Dependency;

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
                XmlPersistentStoreUtils.RestoreTypeMap();
                Console.WriteLine("Finished cleaning...");
            }
            Console.WriteLine();
            Console.WriteLine("Adding new documents to store...");
            var count = XmlPersistentStoreUtils.UpdateTypeMapForDocuments();
            Console.WriteLine(string.Format("{0} documents added!", count));
            Console.WriteLine();

            var openIssues = Map.ResolveType<IPersistentStore<IssueTracker>>().LoadAll().Where(p => !p.IsDone).OrderBy(p => p.Created);

            Console.WriteLine();
            Console.WriteLine(string.Format("This is an overview for you {0}! Loading current open issues...", Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser.UserName));
            Console.WriteLine();
            foreach (var item in openIssues)
            {
                WriteIssue(item);
            }

            Console.WriteLine("Add or update issue? Press <Enter> to continue or <ESC> to exit!");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                ExecuteCommand();

                Console.WriteLine("Add or update issue? Press <Enter> to continue or <ESC> to exit!");
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

            Console.WriteLine("Set Command: ");
            var command = Console.ReadLine();
            {
                if (command.ToLower().Equals(Commands.ADD.ToLower()))
                    AddOrUpdateIssue();
                else if (command.ToLower().StartsWith(Commands.LST.ToLower()))
                    ListItems(command.ToLower().Replace(Commands.LST.ToLower(), ""));
                else if (command.ToLower().Equals(Commands.EXIT.ToLower()))
                    Environment.Exit(0);
                else if (command.ToLower().StartsWith(Commands.DEL.ToLower()))
                    DeleteIssue(command.Replace(Commands.DEL.ToLower(), ""));
                else
                    Console.WriteLine("Command not valid!");

            }
        }

        static void WriteIssue(IssueTracker item)
        {
            Console.WriteLine(string.Format("{0} - {1} - {2}", item.Subject, item.Issue.ToString().ToUpper(), item.Project != null ? item.Project.Name : "Project not set!"));
            Console.WriteLine(string.Format("      {0} / {1}", item.CreatedBy.UserName, item.Created));
            Console.WriteLine(string.Format("      {0}", item.Description));
            //Console.ForegroundColor = GetColorOnIssueSate(item.State);
            Console.WriteLine(string.Format("      State: {0}", item.State.ToString().ToUpper()));
            //Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
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

        static Document AddDocument()
        {
            var user = Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
            var store = Map.ResolveType<IPersistentStore<Document>>();
            Console.WriteLine("Add Subject for document:");
            var subject = Console.ReadLine();
            Console.WriteLine("Filename:");
            var fileName = Console.ReadLine();

            var doc = store.LoadAll().FirstOrDefault(p => p.FileName == fileName);
            if (doc == null)
            {
                doc = new Document();
                doc.Subject = subject;
                doc.User = user;
                doc.AddFile(fileName);
            }
            else
            {
                Console.WriteLine("Document exists! Overwrite? Press <Enter> for override or any key to continue!");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                    doc.AddFile(fileName);
            }

            return doc;
        }

        static void AddOrUpdateIssue()
        {
            var user = Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
            var store = Map.ResolveType<IPersistentStore<IssueTracker>>();
            Console.WriteLine("Set Subject: ");
            var subject = Console.ReadLine();
            var issue = store.LoadAll().FirstOrDefault(p => p.Subject == subject);
            if (issue == null)
            {
                Console.WriteLine("Issue does not existing! Press <Enter> to create new or any other key to continue!");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {

                    issue = new IssueTracker
                    {
                        Subject = subject,
                        CreatedBy = user,
                        Created = DateTime.Now,
                        Issue = IssueType.Bug,
                        State = IssueState.Open
                    };
                }
                else
                    return;
            }
                
            Console.WriteLine("Set Description: ");
            var description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
                issue.Description = description;
            Console.WriteLine("Set Project: ");
            var projectName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(projectName))
            {
                var project = Map.ResolveType<IPersistentStore<Project>>().LoadAll().FirstOrDefault(p => p.Name == projectName);
                if (project == null)
                {
                    project = new Project
                    {
                        Name = projectName
                    };
                    Map.ResolveType<IPersistentStore<Project>>().Save(project);
                }
                issue.Project = project;
            }
            Console.WriteLine("Set Issue: Bug = 1, Incident = 2, Request = 11, ChangeRequest = 12, EnhancementRequest = 13, Information = 21");
            var type = Console.ReadLine();
            issue.Issue = (IssueType)Convert.ToInt32(type);
            Console.WriteLine("Set State: Open = 1, Accepted = 2, InProgress = 3, Done = 4, Rejected = 10");
            var state = Console.ReadLine();
            issue.State = (IssueState)Convert.ToInt32(state);
            store.Save(issue);
            Console.WriteLine("Add document? Press <Enter> to add or any key to continue!");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                var doc = AddDocument();
                doc.Project = issue.Project;
                doc.Issue = issue;
                Map.ResolveType<IPersistentStore<Document>>().Save(doc);
            }
                
            System.Threading.Thread.Sleep(500);
            var item = store.Load(issue.Id);
            if (item != null)
            {
                Console.WriteLine();
                WriteIssue(item);
            }
        }

        static void DeleteIssue(string subject)
        {
            var store = Map.ResolveType<IPersistentStore<IssueTracker>>();
            var issue = store.LoadAll().FirstOrDefault(p => p.Subject == subject);
            if (issue != null)
            {
                store.Delete(issue.Id);
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("Issue deleted!");
                Console.WriteLine();
            }
        }

        static void ListItems(string target)
        {
            if (target.ToLower().Equals("issue"))
            {
                var store = Map.ResolveType<IPersistentStore<IssueTracker>>();
                Console.WriteLine("Load exisiting issues...");
                Console.WriteLine();
                foreach (var item in store.LoadAll().OrderBy( p => p.Created))
                    WriteIssue(item);
            }

            if (target.ToLower().Equals("project"))
            {
                var store = Map.ResolveType<IPersistentStore<Project>>();
                Console.WriteLine("Load exisiting projects...");
                foreach (var item in store.LoadAll().OrderBy(p => p.Name))
                    Console.WriteLine(item.Name);
            }

            if (target.ToLower().Equals("document"))
            {
                Console.WriteLine();
                Console.WriteLine("Adding new documents to store...");
                var count = XmlPersistentStoreUtils.UpdateTypeMapForDocuments();
                Console.WriteLine(string.Format("{0} documents added!", count));
                Console.WriteLine();

                var store = Map.ResolveType<IPersistentStore<Document>>();
                Console.WriteLine("Load exisiting documents...");
                foreach (var item in store.LoadAll().OrderBy(p => p.Subject))
                {
                    Console.WriteLine(string.Format("{0} - {1}", item.Subject ?? "???", item.Project != null ? item.Project.Name : "Project no set!"));
                    Console.WriteLine(string.Format("      {0}", item.FileName));
                    Console.WriteLine(string.Format("      {0}", item.User.UserName));
                    Console.WriteLine(string.Format("      {0}", item.Description));
                    Console.WriteLine();

                }
            }

        }
    }
}


using System;
using System.Linq;
using Trigger.CRM.Model;
using Trigger.CRM.Commands;

namespace Trigger.CommandLine
{
    public class ConsoleInsertUpdateCommand : ConsoleCommand
    {
        public static void InsertUpdateItems(string target)
        {
            if (target.ToLower().Equals("user"))
            {
                InsertUpdateUser();
            }

            if (target.ToLower().Equals("issue"))
            {
                InsertUpdateIssue();
            }

            if (target.ToLower().Equals("project"))
            {
                InsertUpdateProject();
            }

            if (target.ToLower().Equals("times"))
            {
                //var cmd = new TimeTrackerCommand();
            }

            if (target.ToLower().Equals("document"))
            {
                InsertUpdateDocument();
            }
        }

        static User InsertUpdateUser()
        {
            var cmd = new UserCommand();
            Console.WriteLine("Username:");
            var userName = Console.ReadLine();
            Console.WriteLine("E-Mail:");
            var email = Console.ReadLine();
            Console.WriteLine("Password:");
            var password = Console.ReadLine();
            Console.WriteLine("Retype password:");
            var passwordToCompare = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(passwordToCompare))
            {
                if (!password.Equals(passwordToCompare))
                {
                    Console.WriteLine("Passwords are not equal!");
                    return null;
                }
                var user = cmd.GetObjects(new Func<User, bool>(p => p.UserName == userName && p.Password == password)).FirstOrDefault();

                if (user == null)
                {
                    user = new User();
                    user.UserName = userName;
                }
                if (!string.IsNullOrWhiteSpace(email))
                    user.EMail = email;
                user.Password = password;
                cmd.Save(user);
                return user;
            }

            return null;
        }

        static Project InsertUpdateProject()
        {
            var cmd = new ProjectCommand();
            Console.WriteLine("Projectname:");
            var projectName = Console.ReadLine();
            Console.WriteLine("Add some informations:");
            var description = Console.ReadLine();
            var project = cmd.GetObjects(new Func<Project, bool>(p => p.Name == projectName)).FirstOrDefault();
            if (project == null)
            {
                project = new Project();
                project.Name = projectName;
            }
     
            if (!string.IsNullOrWhiteSpace(description))
                project.Description = description;

            cmd.Save(project);

            return project;
        }

        static Document InsertUpdateDocument()
        {
            var cmd = new DocumentCommand();
            Console.WriteLine("Add Subject for document:");
            var subject = Console.ReadLine();
            Console.WriteLine("Filename:");
            var fileName = Console.ReadLine();
            var document = cmd.GetObjects(new Func<Document, bool>(p => p.Subject == subject && p.FileName == fileName)).FirstOrDefault();
            if (document == null)
            {
                document = new Document();
                document.Subject = subject;
                document.User = CurrentUser;
                document.AddFile(fileName);
            }

            Console.WriteLine("Document exists! Overwrite? Press <Enter> for override or any key to continue!");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
                document.AddFile(fileName);

            document.Project = InsertUpdateProject();
            cmd.Save(document);

            return  document;
        }

        static IssueTracker InsertUpdateIssue()
        {
            var cmd = new IssueTrackerCommand();
            Console.WriteLine("Set Subject: ");
            var subject = Console.ReadLine();
            var issue = cmd.GetObjects().FirstOrDefault(p => p.Subject == subject);
            if (issue == null)
            {
                Console.WriteLine("Issue does not existing! Press <Enter> to create new or any other key to continue!");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    issue = new IssueTracker
                    {
                        Subject = subject,
                        CreatedBy = CurrentUser,
                        Created = DateTime.Now,
                        Issue = IssueType.Incident,
                        State = IssueState.Open
                    };
                }
                else
                    return null;
            }
            Console.WriteLine("Set Description: ");
            var description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
                issue.Description = description;
            Console.WriteLine("Set Issue: Bug = 1, Incident = 2, Request = 11, ChangeRequest = 12, EnhancementRequest = 13, Information = 21");
            var type = Console.ReadLine();
            issue.Issue = (IssueType)Convert.ToInt32(type);
            Console.WriteLine("Set State: Open = 1, Accepted = 2, InProgress = 3, Done = 4, Rejected = 10");
            var state = Console.ReadLine();
            issue.State = (IssueState)Convert.ToInt32(state);
            issue.Project = InsertUpdateProject();

            cmd.Save(issue);

            return issue;
        }
    }
}
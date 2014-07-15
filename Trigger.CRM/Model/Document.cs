using System.ComponentModel;
using System.IO;
using Trigger.CRM.Persistent;
using System;

namespace Trigger.CRM.Model
{
    public class Document : ModelBase
    {
        public void AddFile(string sourcePath, bool copy = true)
        {
            if (File.Exists(sourcePath))
            {
                var file = new FileInfo(sourcePath);

                var targetPath = Path.Combine(PersistentStoreInitialzer.PersistentDocumentStoreLocation, file.Name);

                if (!sourcePath.Equals(targetPath))
                {
                    if (copy)
                        File.Copy(sourcePath, targetPath);
                    else
                        File.Move(sourcePath, targetPath);
                }

                FileName = new FileInfo(targetPath).Name;
            }
        }

        string subject;

        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                if (Equals(subject, value))
                    return;
                subject = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Subject"));
            }
        }

        string description;

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (Equals(description, value))
                    return;
                description = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Description"));
            }
        }

        Project project;

        public Project Project
        {
            get
            {
                return project;
            }
            set
            {
                if (Equals(project, value))
                    return;
                project = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Project"));
            }
        }

        IssueTracker issue;

        public IssueTracker Issue
        {
            get
            {
                return issue;
            }
            set
            {
                if (Equals(issue, value))
                    return;
                issue = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Issue"));
            }
        }

        User user;

        public User User
        {
            get
            {
                return user;
            }
            set
            {
                if (Equals(user, value))
                    return;
                user = value;

                OnPropertyChanged(new PropertyChangedEventArgs("User"));
            }
        }

        string fileName;

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                if (Equals(fileName, value))
                    return;
                fileName = value;

                OnPropertyChanged(new PropertyChangedEventArgs("FileName"));
            }
        }
    }
}

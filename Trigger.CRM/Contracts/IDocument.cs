using System;
using Trigger.CRM.Persistent;

namespace Trigger.CRM.Model
{
    public interface IDocument : IStorable
    {
        string Subject
        {
            get;
            set;
        }

        string Description
        {
            get;
            set;
        }

        string FileName
        {
            get;
            set;
        }

        DateTime? Created
        {
            get;
            set;
        }

        Project Project
        {
            get;
            set;
        }

        IssueTracker Issue
        {
            get;
            set;
        }

        User User
        {
            get;
            set;
        }
    }
    
}

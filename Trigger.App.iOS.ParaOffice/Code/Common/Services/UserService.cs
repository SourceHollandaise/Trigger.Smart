using System;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.Common.Services
{
    [Obsolete("Do not use!", true)]
    public class UserService
    {
        public User CreateUser(string userName, string password, string passwordToCompare)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(passwordToCompare))
            {
                if (!password.Equals(passwordToCompare))
                    throw new ArgumentException("Passwords are not equal!");

                var user = DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<User>().FirstOrDefault(p => p.UserName == userName && p.Password == password);
                if (user != null)
                    throw new ArgumentException("User exists!");
                else
                {
                    user = new User();
                    user.UserName = userName;
                    user.SetPassword(password);
                    user.Save();
                    return user;
                }
            }

            return null;
        }
    }
}

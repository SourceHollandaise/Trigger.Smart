using System;
using System.Linq;
using Trigger.Dependency;
using Trigger.CRM.Persistent;

namespace Trigger.CRM.Model
{

    public class UserFactory
    {
        public User CreateUser(string userName, string password, string comparePassword)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(comparePassword))
            {
                if (!password.Equals(comparePassword))
                    throw new ArgumentException("Passwords are not equal!");

                var user = DependencyMapProvider.Instance.ResolveType<IPersistentStore<User>>().LoadAll().FirstOrDefault(p => p.UserName == userName && p.Password == password);
                if (user != null)
                    throw new ArgumentException("User exists!");
                else
                {
                    user = new User{ UserName = userName, Password = password };
                    DependencyMapProvider.Instance.ResolveType<IPersistentStore<User>>().Save(user);
                    return user;
                }
            }

            return null;
        }
    }
}

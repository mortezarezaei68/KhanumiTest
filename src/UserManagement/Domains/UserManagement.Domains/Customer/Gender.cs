using System.Collections.Generic;
using System.Linq;
using Framework.Domain.Core;
using UserManagement.Domains.IPersistGrants;

namespace UserManagement.Domains.Customer
{
    public class Gender:Entity<int>
    {
        public Gender(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
        private readonly List<User> _users = new();
        public IReadOnlyCollection<User> Users => _users;

        public void Update(string name)
        {
            Name = name;
        }
    }
}
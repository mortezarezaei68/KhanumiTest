using System;
using Microsoft.AspNetCore.Identity;

namespace UserManagement.Domains
{
    public class Role:IdentityRole<int>
    {
        public bool IsDeleted { get; set; }
        public void Update(string name)
        {
            Name = name;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
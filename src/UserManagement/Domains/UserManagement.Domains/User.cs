using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using UserManagement.Domains.Customer;
using UserManagement.Domains.IPersistGrants;

namespace UserManagement.Domains
{
    public class User:IdentityUser<int>
    {
        public Guid SubjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        
        public bool IsDeleted { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get;  set; }
        public UserType UserType { get; set; }
        

        public void Update(DateTime birthday,string firstName,string lastName,string userName,string phoneNumber,string email)
        {
            Birthday = birthday;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            EmailConfirmed = false;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = false;
        }
        
        public void UpdateCustomer(DateTime birthday,string firstName,string lastName,string userName,string phoneNumber,string email,int genderId)
        {
            Birthday = birthday;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            GenderId = genderId;
        }

        public void UpdatePersistGrants()
        {
            var lastGrants = _grantsList.Where(a=>!a.IsRevoke);
            foreach (var items in lastGrants)
            {
                items.Update();
            }
        }

        public void Delete()
        {
            IsDeleted = true;
            var persistGrant=PersistGrants.Where(a => !a.IsRevoke).ToList();
            foreach (var items in persistGrant)
            {
                items.Update();
            }

        }
        public void Add(string subjectId,string refreshToken, string ipAddress, DateTime expiredTime,UserType userType)
        {
            var persistGrants = new PersistGrants(subjectId, refreshToken, ipAddress, expiredTime,userType);
            _grantsList.Add(persistGrants);
        }
        private readonly List<PersistGrants> _grantsList = new();
        public IReadOnlyCollection<PersistGrants> PersistGrants => _grantsList;

    }
}
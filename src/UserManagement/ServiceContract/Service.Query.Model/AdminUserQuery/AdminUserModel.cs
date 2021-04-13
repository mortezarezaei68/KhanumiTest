using System;
using System.Collections.Generic;
using Framework.Queries;
using Service.Query.Model.AdminRoleQuery;
using UserManagement.Domains;

namespace Service.Query.Model.AdminUserQuery
{
    public class AdminUserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string GenderName { get; set; }
        public string PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public List<RoleModel> UserRoles { get; set; }
    }
}
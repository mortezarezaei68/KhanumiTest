using System;

namespace TicketManagement.Query.Models
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
        public string UserType { get; set; }
    }
}
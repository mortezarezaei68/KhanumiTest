using System;

namespace Service.Query.Model.CustomerUserQuery
{
    public class CustomerUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string GenderName { get; set; }
        public int GenderId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
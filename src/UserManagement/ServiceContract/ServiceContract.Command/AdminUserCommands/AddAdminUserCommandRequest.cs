using System;
using MediatR;

namespace ServiceContract.Command.AdminUserCommands
{
    public class AddAdminUserCommandRequest:IRequest<AddAdminUserCommandResponse>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int GenderId => 1;
    }
}
using System;
using MediatR;

namespace ServiceContract.Command.AdminUserCommands
{
    public class EditAdminUserCommandRequest:IRequest<EditAdminUserCommandResponse>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

    }
}
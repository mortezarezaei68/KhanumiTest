using System;
using MediatR;

namespace ServiceContract.Command.CustomerUserCommands
{
    public class AddCustomerCommandRequest:IRequest<AddCustomerCommandResponse>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber{ get; set; }
        public int GenderId { get; set; }
    }
}
using System;
using MediatR;

namespace ServiceContract.Command.CustomerUserCommands
{
    public class EditCustomerCommandRequest:IRequest<EditCustomerCommandResponse>
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }

        public DateTime Birthday { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string UserName { get; set; }
        
        public int GenderId { get; set; }

    }
}
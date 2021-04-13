using MediatR;

namespace ServiceContract.Command.CustomerUserCommands
{
    public class LoginCustomerUserCommandRequest:IRequest<LoginCustomerUserCommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
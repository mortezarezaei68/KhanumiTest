using MediatR;

namespace ServiceContract.Command.CustomerUserCommands
{
    public class UpdateCustomerPasswordCommandRequest:IRequest<UpdateCustomerPasswordCommandResponse>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ServiceContract.Command.CustomerUserCommands
{
    public class LoginCustomerUserCommandResponse:ResponseCommand
    {
        public LoginCustomerUserCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
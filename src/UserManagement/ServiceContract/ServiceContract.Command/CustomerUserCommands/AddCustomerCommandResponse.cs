using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ServiceContract.Command.CustomerUserCommands
{
    public class AddCustomerCommandResponse:ResponseCommand
    {
        public AddCustomerCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
using Common;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ServiceContract.Command.AdminUserCommands
{
    public class AddAdminUserCommandResponse:ResponseCommand
    {
        public AddAdminUserCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ServiceContract.Command.AdminUserCommands
{
    public class LoginAdminUserCommandResponse:ResponseCommand
    {
        public LoginAdminUserCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
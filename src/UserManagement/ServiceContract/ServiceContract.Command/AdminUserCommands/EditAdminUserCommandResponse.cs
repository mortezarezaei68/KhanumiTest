using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ServiceContract.Command.AdminUserCommands
{
    public class EditAdminUserCommandResponse:ResponseCommand
    {
        public EditAdminUserCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
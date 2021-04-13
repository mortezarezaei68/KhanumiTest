using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ServiceContract.Command.UserRoleCommands
{
    public class AddRoleToUserCommandResponse:ResponseCommand
    {
        public AddRoleToUserCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
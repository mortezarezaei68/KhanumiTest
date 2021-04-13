using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;

namespace ServiceContract.Command.AdminUserCommands
{
    public class SignOutAdminUserCommandRequest:IRequest<SignOutAdminUserCommandResponse>
    {
        
    }

    public class SignOutAdminUserCommandResponse:ResponseCommand
    {
        public SignOutAdminUserCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
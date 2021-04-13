using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;

namespace ServiceContract.Command.UserTokenCommands
{
    public class InvokePersistGrantsCommandRequest:IRequest<InvokePersistGrantsCommandResponse>
    {
        public string UserId { get; set; }
    }

    public class InvokePersistGrantsCommandResponse:ResponseCommand
    {
        public InvokePersistGrantsCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
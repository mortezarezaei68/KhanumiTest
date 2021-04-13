using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ServiceContract.Command.UserTokenCommands
{
    public class RefreshTokenCommandResponse:ResponseCommand<RefreshTokenViewModel>
    {
        public RefreshTokenCommandResponse(bool isSuccess, ResultCode resultCode, RefreshTokenViewModel data, string message = null) : base(isSuccess, resultCode, data, message)
        {
        }
    }
}
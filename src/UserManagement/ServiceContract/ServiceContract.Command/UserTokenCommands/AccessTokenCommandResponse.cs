using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ServiceContract.Command.UserTokenCommands
{
    public class AccessTokenCommandResponse:ResponseCommand<TokenViewModel>
    {
        public AccessTokenCommandResponse(bool isSuccess, ResultCode resultCode, TokenViewModel data, string message = null) : base(isSuccess, resultCode, data, message)
        {
        }
    }
}
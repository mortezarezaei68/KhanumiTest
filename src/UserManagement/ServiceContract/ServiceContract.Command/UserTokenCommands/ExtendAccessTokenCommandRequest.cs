using MediatR;

namespace ServiceContract.Command.UserTokenCommands
{
    public class ExtendAccessTokenCommandRequest:IRequest<ExtendAccessTokenCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
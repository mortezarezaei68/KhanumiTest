using MediatR;
using UserManagement.Domains;

namespace ServiceContract.Command.UserTokenCommands
{
    public class RefreshTokenCommandRequest:IRequest<RefreshTokenCommandResponse>
    {
        public UserType UserType { get; set; }
        public string SubjectId { get; set; }
        public int UserId { get; set; }
    }
}
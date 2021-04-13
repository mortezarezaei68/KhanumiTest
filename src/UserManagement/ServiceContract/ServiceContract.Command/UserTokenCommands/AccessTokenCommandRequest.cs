using System.Collections.Generic;
using MediatR;
using UserManagement.Domains;

namespace ServiceContract.Command.UserTokenCommands
{
    public class AccessTokenCommandRequest:IRequest<AccessTokenCommandResponse>
    {
        public string SubjectId { get; set; }
        public List<string> Roles { get; set; }
        public UserType UserType { get; set; }
        public int UserId { get; set; }
    }
}
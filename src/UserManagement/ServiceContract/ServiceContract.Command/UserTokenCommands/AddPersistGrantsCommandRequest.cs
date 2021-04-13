using System;
using MediatR;
using UserManagement.Domains;

namespace ServiceContract.Command.UserTokenCommands
{
    public class AddPersistGrantsCommandRequest:IRequest<AddPersistGrantsCommandResponse>
    {
        public int UserId { get; set; }
        public string SubjectId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string IpAddress { get; set; }
        public UserType UserType { get; set; }
    }
}
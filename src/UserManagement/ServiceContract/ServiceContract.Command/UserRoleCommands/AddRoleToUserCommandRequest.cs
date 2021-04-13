using MediatR;

namespace ServiceContract.Command.UserRoleCommands
{
    public class AddRoleToUserCommandRequest:IRequest<AddRoleToUserCommandResponse>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
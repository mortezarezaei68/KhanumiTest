using MediatR;

namespace ServiceContract.Command.UserRoleCommands
{
    public class DeleteRoleCommandRequest:IRequest<DeleteRoleCommandResponse>
    {
        public string Id { get; set; }
    }
}
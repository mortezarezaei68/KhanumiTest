using MediatR;

namespace ServiceContract.Command.UserRoleCommands
{
    public class EditRoleCommandRequest:IRequest<EditRoleCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
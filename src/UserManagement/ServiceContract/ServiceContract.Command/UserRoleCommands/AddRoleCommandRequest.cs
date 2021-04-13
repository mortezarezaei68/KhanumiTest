using MediatR;

namespace ServiceContract.Command.UserRoleCommands
{
    public class AddRoleCommandRequest:IRequest<AddRoleCommandResponse>
    {
        public string Name { get; set; }
    }
}
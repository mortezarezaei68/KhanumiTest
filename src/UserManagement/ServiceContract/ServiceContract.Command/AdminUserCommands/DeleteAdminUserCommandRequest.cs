using MediatR;

namespace ServiceContract.Command.AdminUserCommands
{
    public class DeleteAdminUserCommandRequest:IRequest<DeleteAdminUserCommandResponse>
    {
        public string Id { get; set; }
    }
}
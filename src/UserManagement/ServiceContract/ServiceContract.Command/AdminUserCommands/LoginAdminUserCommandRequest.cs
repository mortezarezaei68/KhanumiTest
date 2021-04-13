using MediatR;

namespace ServiceContract.Command.AdminUserCommands
{
    public class LoginAdminUserCommandRequest:IRequest<LoginAdminUserCommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
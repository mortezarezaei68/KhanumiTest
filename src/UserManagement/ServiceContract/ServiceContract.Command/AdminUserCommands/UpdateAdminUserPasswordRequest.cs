using MediatR;

namespace ServiceContract.Command.AdminUserCommands
{
    public class UpdateAdminUserPasswordRequest:IRequest<UpdateAdminUserPasswordResponse>
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        
        public string ConfirmPassword { get; set; }
    }
}
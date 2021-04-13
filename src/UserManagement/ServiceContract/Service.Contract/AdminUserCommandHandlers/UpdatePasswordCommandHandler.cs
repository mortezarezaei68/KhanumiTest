using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceContract.Command.AdminUserCommands;
using UserManagement.Domains;

namespace Service.Contract.AdminUserCommandHandlers
{
    public class UpdatePasswordCommandHandler:ITransactionalCommandHandlerMediatR<UpdateAdminUserPasswordRequest,UpdateAdminUserPasswordResponse>
    {
        private readonly UserManager<User> _userManager;

        public UpdatePasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UpdateAdminUserPasswordResponse> Handle(UpdateAdminUserPasswordRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<UpdateAdminUserPasswordResponse> next)
        {
            var user=await _userManager.FindByIdAsync(request.Id);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "user not found");
            var checkPassword = await _userManager.CheckPasswordAsync(user, request.OldPassword);
            if (!checkPassword)
                throw new AppException(ResultCode.BadRequest, "your old password is not correct");

            if (request.NewPassword is not null && request.OldPassword!=request.NewPassword)
                await _userManager.ChangePasswordAsync(user,  request.OldPassword, request.NewPassword);
            return new UpdateAdminUserPasswordResponse(true, ResultCode.Success);
        }
    }
}
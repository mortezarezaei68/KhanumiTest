using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceContract.Command.AdminUserCommands;
using UserManagement.Domains;

namespace Service.Contract
{
    public class EditAdminUserCommandHandler:ITransactionalCommandHandlerMediatR<EditAdminUserCommandRequest,EditAdminUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;

        public EditAdminUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<EditAdminUserCommandResponse> Handle(EditAdminUserCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<EditAdminUserCommandResponse> next)
        {
            var user=await _userManager.FindByIdAsync(request.Id);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "user not found");
            user.Update(request.Birthday, request.FirstName, request.LastName,
                request.UserName, request.PhoneNumber, request.EmailAddress);
            
            await _userManager.UpdateAsync(user);
            

            return new EditAdminUserCommandResponse(true, ResultCode.Success);

        }
    }
}
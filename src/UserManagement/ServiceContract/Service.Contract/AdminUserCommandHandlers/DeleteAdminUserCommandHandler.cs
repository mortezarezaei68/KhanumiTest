using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceContract.Command.AdminUserCommands;
using UserManagement.Domains;

namespace Service.Contract.AdminUserCommandHandlers
{
    public class DeleteAdminUserCommandHandler:ITransactionalCommandHandlerMediatR<DeleteAdminUserCommandRequest,DeleteAdminUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;

        public DeleteAdminUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DeleteAdminUserCommandResponse> Handle(DeleteAdminUserCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<DeleteAdminUserCommandResponse> next)
        {
            var user=await _userManager.Users.Include(a=>a.PersistGrants).FirstOrDefaultAsync(a=>a.Id==int.Parse(request.Id));
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "not found user");
            
            user.Delete();
            await _userManager.UpdateAsync(user);
            return new DeleteAdminUserCommandResponse(true, ResultCode.Success);

        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceContract.Command.UserRoleCommands;
using UserManagement.Domains;

namespace Service.Contract.RoleCommandHandlers
{
    public class AddRoleToUserCommandHandler:ITransactionalCommandHandlerMediatR<AddRoleToUserCommandRequest,AddRoleToUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AddRoleToUserCommandHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AddRoleToUserCommandResponse> Handle(AddRoleToUserCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AddRoleToUserCommandResponse> next)
        {
            var roleResult =await _roleManager.FindByIdAsync(request.RoleId);
            if (roleResult is null)
                throw new AppException(ResultCode.BadRequest, "role is not exist");
            var userResult = await _userManager.FindByIdAsync(request.UserId);
            
            if (userResult.UserType == UserType.customer)
                throw new AppException(ResultCode.BadRequest, "user is not admin");
            
            if (userResult is null || userResult.IsDeleted)
                throw new AppException(ResultCode.BadRequest, "user is not exist");
            
            var addToRole=await _userManager.AddToRoleAsync(userResult, roleResult.Name);
            if (addToRole.Succeeded)
                return new AddRoleToUserCommandResponse(true, ResultCode.Success);

            throw new AppException(ResultCode.BadRequest, "your role not assign to user");


        }
    }
}
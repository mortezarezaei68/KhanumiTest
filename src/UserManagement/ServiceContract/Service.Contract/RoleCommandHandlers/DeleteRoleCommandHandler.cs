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
    public class DeleteRoleCommandHandler:ITransactionalCommandHandlerMediatR<DeleteRoleCommandRequest,DeleteRoleCommandResponse>
    {
        private readonly RoleManager<Role> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<DeleteRoleCommandResponse> next)
        {
            var role=await _roleManager.FindByIdAsync(request.Id);
            if (role is null)
                throw new AppException(ResultCode.BadRequest, "can not found role");
            
            role.Delete();
            await _roleManager.UpdateAsync(role);

            return new DeleteRoleCommandResponse(true, ResultCode.Success);
        }
    }
}
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
    public class EditRoleCommandHandler:ITransactionalCommandHandlerMediatR<EditRoleCommandRequest,EditRoleCommandResponse>
    {
        private readonly RoleManager<Role> _roleManager;

        public EditRoleCommandHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<EditRoleCommandResponse> Handle(EditRoleCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<EditRoleCommandResponse> next)
        {
            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role is null)
                throw new AppException(ResultCode.BadRequest, "role not found");
            
            role.Update(request.Name);
            await _roleManager.UpdateAsync(role);
            return new EditRoleCommandResponse(true, ResultCode.Success);
        }
    }
}
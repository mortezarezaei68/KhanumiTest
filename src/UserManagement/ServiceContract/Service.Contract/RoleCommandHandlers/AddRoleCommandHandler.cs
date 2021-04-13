using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceContract.Command.UserRoleCommands;
using UserManagement.Domains;

namespace Service.Contract.RoleCommandHandlers
{
    public class AddRoleCommandHandler:ITransactionalCommandHandlerMediatR<AddRoleCommandRequest,AddRoleCommandResponse>
    {
        private readonly RoleManager<Role> _roleManager;

        public AddRoleCommandHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<AddRoleCommandResponse> Handle(AddRoleCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AddRoleCommandResponse> next)
        {

            var existRole = await _roleManager.FindByNameAsync(request.Name);
            if (existRole is not null)
                throw new AppException(ResultCode.BadRequest, "role is existed");
            
            await _roleManager.CreateAsync(new Role {Name = request.Name});
            
            return new AddRoleCommandResponse(true, ResultCode.Success);
        }
    }
}
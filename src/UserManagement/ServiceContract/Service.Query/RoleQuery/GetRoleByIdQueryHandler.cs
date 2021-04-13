using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Exception.Exceptions.Enum;
using Framework.Queries;
using Microsoft.AspNetCore.Identity;
using Service.Query.Model.AdminRoleQuery;
using UserManagement.Domains;

namespace Service.Query.RoleQuery
{
    public class GetRoleByIdQueryHandler:IQueryHandlerMediatR<GetAdminRoleByIdQueryRequest,GetAdminRoleByIdQueryResponse>
    {
        private readonly RoleManager<Role> _roleManager;

        public GetRoleByIdQueryHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GetAdminRoleByIdQueryResponse> Handle(GetAdminRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var role=await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role is null)
                throw new AppException(ResultCode.BadRequest, "role not found");
                
            
            var roleModel = new RoleModel
            {
                Id = role.Id.ToString(),
                Name = role.Name
            };
            return new GetAdminRoleByIdQueryResponse(true, roleModel);
        }
    }
}
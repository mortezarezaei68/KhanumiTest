using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.AdminRoleQuery;
using UserManagement.Domains;

namespace Service.Query.RoleQuery
{
    public class GetAllRolesQueryHandler:IQueryHandlerMediatR<GetAllAdminRolesQueryRequest,GetAllAdminRolesQueryResponse>
    {
        private readonly RoleManager<Role> _roleManager;

        public GetAllRolesQueryHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GetAllAdminRolesQueryResponse> Handle(GetAllAdminRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles.Select(a => new RoleModel()
            {
                Id = a.Id.ToString(),
                Name = a.Name
            }).ToListAsync(cancellationToken: cancellationToken);
            return new GetAllAdminRolesQueryResponse(true,roles,roles.Count);
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.AdminUserQuery;
using UserManagement.Domains;

namespace Service.Query.AdminUserQuery
{
    public class GetAllAdminUsersQueryHandler:IQueryHandlerMediatR<GetAllAdminUserQueryRequest,GetAllAdminUserQueryResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public GetAllAdminUsersQueryHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<GetAllAdminUserQueryResponse> Handle(GetAllAdminUserQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Include(a => a.Gender).Select(
                a => new AdminUserModel
                {
                    Birthday = a.Birthday,
                    Email = a.Email,
                    Id = a.Id.ToString(),
                    FirstName = a.FirstName,
                    GenderName = a.Gender.Name,
                    LastName = a.LastName,
                    UserName = a.UserName,
                    PhoneNumber = a.PhoneNumber
                }).ToListAsync(cancellationToken: cancellationToken);
            return new GetAllAdminUserQueryResponse(true, users,users.Count);
        }
    }
}
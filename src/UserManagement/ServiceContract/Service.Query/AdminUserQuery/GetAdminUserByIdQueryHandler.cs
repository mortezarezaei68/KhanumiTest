using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Common;
using Framework.Exception.Exceptions.Enum;
using Framework.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.AdminRoleQuery;
using Service.Query.Model.AdminUserQuery;
using UserManagement.Domains;

namespace Service.Query.AdminUserQuery
{
    public class GetAdminUserByIdQueryHandler:IQueryHandlerMediatR<GetAdminUserQueryRequest,GetAdminUserQueryResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ICurrentUser _currentUser;

        public GetAdminUserByIdQueryHandler(UserManager<User> userManager, RoleManager<Role> roleManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _currentUser = currentUser;
        }

        public async Task<GetAdminUserQueryResponse> Handle(GetAdminUserQueryRequest request, CancellationToken cancellationToken)
        {
            var user=await _userManager.Users.Include(a => a.Gender).FirstOrDefaultAsync(a=>a.Id==request.UserId);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "user not found");
                
            
            var roles = await _userManager.GetRolesAsync(user);
            var adminUser =new AdminUserModel
            {
                Birthday = user.Birthday,
                Email = user.Email,
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                GenderName = user.Gender.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType,
                UserRoles = _roleManager.Roles.Where(a => roles.Any(b => b == a.Name)).Select(a =>
                    new RoleModel()
                    {
                        Name = a.Name,
                        Id = a.Id.ToString()
                    }).ToList()
            };
            return new GetAdminUserQueryResponse(true, adminUser);
        }
    }
}
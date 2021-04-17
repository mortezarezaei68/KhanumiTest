using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.AdminUserQuery;
using UserManagement.Domains;

namespace Service.Query.AdminUserQuery
{
    public class GetAllUserConsumer: IConsumer<GetAllUserRequest>
    {
        private readonly UserManager<User> _userManager;
        public async Task Consume(ConsumeContext<GetAllUserRequest> context)
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
                }).ToListAsync();
            await context.RespondAsync(new GetAllAdminUserQueryResponse(true, users,users.Count));
        }
    }

    public class GetAllUserRequest
    {
    }
}
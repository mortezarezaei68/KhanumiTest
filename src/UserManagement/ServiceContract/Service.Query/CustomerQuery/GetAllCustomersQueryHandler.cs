using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Common;
using Framework.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.CustomerUserQuery;
using UserManagement.Domains;

namespace Service.Query.CustomerQuery
{
    public class GetAllCustomersQueryHandler:IQueryHandlerMediatR<GetAllCustomerQueryRequest,GetAllCustomerQueryResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;

        public GetAllCustomersQueryHandler(UserManager<User> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<GetAllCustomerQueryResponse> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Include(a => a.Gender).Where(a => a.UserType == UserType.customer).Select(
                a => new CustomerUserModel
                {
                    Birthday = a.Birthday,
                    Email = a.Email,
                    FirstName = a.FirstName,
                    GenderName = a.Gender.Name,
                    GenderId = a.GenderId,
                    LastName = a.LastName,
                    UserName = a.UserName,
                    PhoneNumber = a.PhoneNumber
                }).ToListAsync(cancellationToken: cancellationToken);
            return new GetAllCustomerQueryResponse(true, users,users.Count);
        }
    }
}
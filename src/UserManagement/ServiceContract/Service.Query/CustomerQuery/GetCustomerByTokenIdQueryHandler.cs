using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Common;
using Framework.Exception.Exceptions.Enum;
using Framework.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.CustomerUserQuery;
using UserManagement.Domains;

namespace Service.Query.CustomerQuery
{
    public class GetCustomerByTokenIdQueryHandler:IQueryHandlerMediatR<CustomerByTokenIdQueryRequest,CustomerByTokenIdQueryResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;

        public GetCustomerByTokenIdQueryHandler(UserManager<User> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<CustomerByTokenIdQueryResponse> Handle(CustomerByTokenIdQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetUserId();
            var user = await _userManager.Users.Include(a => a.Gender)
                .FirstOrDefaultAsync(a => a.UserType == UserType.customer && a.SubjectId.ToString()==userId, cancellationToken: cancellationToken);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "user not found");

            var customer=new CustomerUserModel
            {
                Birthday = user.Birthday,
                Email = user.Email,
                FirstName = user.FirstName,
                GenderName = user.Gender.Name,
                GenderId = user.GenderId,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
            return new CustomerByTokenIdQueryResponse(true, customer);
        }
    }
}
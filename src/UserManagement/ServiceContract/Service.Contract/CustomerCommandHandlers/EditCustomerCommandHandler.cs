using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using FluentValidation;
using Framework.Commands.CommandHandlers;
using Framework.Common;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceContract.Command.CustomerUserCommands;
using UserManagement.Domains;

namespace Service.Contract.CustomerCommandHandlers
{
    public class EditCustomerCommandHandler:ITransactionalCommandHandlerMediatR<EditCustomerCommandRequest,EditCustomerCommandResponse>
    {
        private readonly ICurrentUser _currentUser;
        private readonly UserManager<User> _userManager;

        public EditCustomerCommandHandler(ICurrentUser currentUser, UserManager<User> userManager)
        {
            _currentUser = currentUser;
            _userManager = userManager;
        }

        public async Task<EditCustomerCommandResponse> Handle(EditCustomerCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<EditCustomerCommandResponse> next)
        {
            var userId = _currentUser.GetUserId();
            var customer =await _userManager.Users.FirstOrDefaultAsync(a => a.SubjectId.ToString() == userId);
            if (customer is null)
                throw new AppException(ResultCode.BadRequest, "customer not exist");
            
            customer.UpdateCustomer(request.Birthday,request.FirstName,request.LastName,request.UserName,request.PhoneNumber,request.Email,request.GenderId);
            await _userManager.UpdateAsync(customer);
            return new EditCustomerCommandResponse(true, ResultCode.Success);
        }
    }
    public class EditCustomerCommandRequestValidator : AbstractValidator<EditCustomerCommandRequest>
    {
        public EditCustomerCommandRequestValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().NotNull();
            RuleFor(p => p.LastName).NotEmpty().NotNull();
            RuleFor(p => p.UserName).NotEmpty().NotNull();
            RuleFor(p=>p.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(p => p.PhoneNumber).NotEmpty().NotNull().Matches(@"^\+(?:[0-9] ?){6,14}[0-9]$");
            RuleFor(p => p.GenderId).NotEqual(0);
        }
    }
}
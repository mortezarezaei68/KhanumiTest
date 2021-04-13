using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using FluentValidation;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceContract.Command.AdminUserCommands;
using UserManagement.Domains;

namespace Service.Contract.AdminUserCommandHandlers
{
    public class AddAdminUserCommandHandler:ITransactionalCommandHandlerMediatR<AddAdminUserCommandRequest,AddAdminUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;

        public AddAdminUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AddAdminUserCommandResponse> Handle(AddAdminUserCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AddAdminUserCommandResponse> next)
        {
            var user=await _userManager.FindByNameAsync(request.UserName);
            if (user is not null && user.IsDeleted)
                throw new AppException(ResultCode.BadRequest, "user is exist but deleted");
            var result = await _userManager.CreateAsync(new User()
            {
                Birthday = request.Birthday,
                Email = request.EmailAddress,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                SubjectId = Guid.NewGuid(),
                UserType = UserType.admin,
                GenderId = request.GenderId
            }, request.Password);
            
            if (result.Succeeded)
                return new AddAdminUserCommandResponse(true, ResultCode.Success);


            throw new AppException(ResultCode.BadRequest, "user can not sign up");
        }
    }
    public class AddAdminUserCommandRequestValidator : AbstractValidator<AddAdminUserCommandRequest>
    {
        public AddAdminUserCommandRequestValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().NotNull();
            RuleFor(p => p.LastName).NotEmpty().NotNull();
            RuleFor(p => p.UserName).NotEmpty().NotNull();
            RuleFor(p=>p.EmailAddress).NotEmpty().NotNull().EmailAddress();
            RuleFor(p => p.PhoneNumber).NotEmpty().NotNull().Matches(@"^\+(?:[0-9] ?){6,14}[0-9]$");
            RuleFor(p => p.GenderId).NotEqual(0);
            RuleFor(p => p.Password).Equal(p=>p.ConfirmPassword);
        }
    }
}
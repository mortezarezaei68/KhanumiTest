using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using FluentValidation;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceContract.Command.CustomerUserCommands;
using UserManagement.Domains;

namespace Service.Contract.CustomerCommandHandlers
{
    public class AddCustomerCommandHandler:ITransactionalCommandHandlerMediatR<AddCustomerCommandRequest,AddCustomerCommandResponse>
    {
        private readonly UserManager<User> _userManager;

        public AddCustomerCommandHandler( UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AddCustomerCommandResponse> Handle(AddCustomerCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AddCustomerCommandResponse> next)
        {
            if (request.Password != request.ConfirmPassword)
                throw new AppException(ResultCode.BadRequest, "confirm password not the same");
            var result=await _userManager.CreateAsync(new User()
            {

                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                Email = request.Email,
                LastName = request.LastName,
                SubjectId = Guid.NewGuid(),
                UserType = UserType.customer,
                GenderId = request.GenderId
            }, request.Password);
            if (result.Succeeded)
            {
                return new AddCustomerCommandResponse(true, ResultCode.Success);
            }


            throw new AppException(ResultCode.BadRequest, "you have a problem in signup");
            
        }
    }
    public class AddCustomerCommandRequestValidator : AbstractValidator<AddCustomerCommandRequest>
    {
        public AddCustomerCommandRequestValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().NotNull();
            RuleFor(p => p.LastName).NotEmpty().NotNull();
            RuleFor(p => p.UserName).NotEmpty().NotNull();
            RuleFor(p=>p.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(p => p.PhoneNumber).NotEmpty().NotNull().Matches(@"^\+(?:[0-9] ?){6,14}[0-9]$");
            RuleFor(p => p.GenderId).NotEqual(0);
            RuleFor(p => p.Password).Equal(p=>p.ConfirmPassword);
        }
    }
}
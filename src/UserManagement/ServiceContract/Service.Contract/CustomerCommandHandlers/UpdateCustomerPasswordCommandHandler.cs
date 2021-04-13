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
    public class UpdateCustomerPasswordCommandHandler:ITransactionalCommandHandlerMediatR<UpdateCustomerPasswordCommandRequest,UpdateCustomerPasswordCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;

            public UpdateCustomerPasswordCommandHandler(ICurrentUser currentUser, UserManager<User> userManager)
            {
                _currentUser = currentUser;
                _userManager = userManager;
            }

        public async Task<UpdateCustomerPasswordCommandResponse> Handle(UpdateCustomerPasswordCommandRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<UpdateCustomerPasswordCommandResponse> next)
        {
            var userId=_currentUser.GetUserId();
            var customer=await _userManager.Users.FirstOrDefaultAsync(a=>a.SubjectId.ToString()==userId);
            if (customer is null)
                throw new AppException(ResultCode.BadRequest, "Customer not exist");

            var oldPassword = await _userManager.CheckPasswordAsync(customer, request.OldPassword);
            if (!oldPassword)
                throw new AppException(ResultCode.BadRequest, "Your Old Password is Not Correct");
            
            if (request.ConfirmPassword != request.NewPassword)
                throw new AppException(ResultCode.BadRequest,
                    "confirm password not same");
            
            if (request.NewPassword == request.OldPassword)
                throw new AppException(ResultCode.BadRequest,
                    "you select same with old password");
            
            await _userManager.ChangePasswordAsync(customer, request.OldPassword, request.NewPassword);
            
            return new UpdateCustomerPasswordCommandResponse(true, ResultCode.Success);
        }
    }
    public class UpdateCustomerPasswordCommandRequestValidator : AbstractValidator<UpdateCustomerPasswordCommandRequest>
    {
        public UpdateCustomerPasswordCommandRequestValidator()
        {
            RuleFor(p => p.ConfirmPassword).NotEmpty().NotNull();
            RuleFor(p => p.NewPassword).NotEmpty().NotNull();
            RuleFor(p => p.OldPassword).NotEmpty().NotNull();
        }
    }
}
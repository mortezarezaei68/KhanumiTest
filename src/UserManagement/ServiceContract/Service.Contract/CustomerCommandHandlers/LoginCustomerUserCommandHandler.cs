using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Buses;
using Framework.Commands.CommandHandlers;
using Framework.Common;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceContract.Command.AdminUserCommands;
using ServiceContract.Command.CustomerUserCommands;
using ServiceContract.Command.UserTokenCommands;
using UserManagement.Domains;

namespace Service.Contract.CustomerCommandHandlers
{
    public class LoginCustomerUserCommandHandler:ITransactionalCommandHandlerMediatR<LoginCustomerUserCommandRequest,LoginCustomerUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEventBus _eventBus;
        private readonly ICurrentUser _currentUser;
        private readonly IConfiguration _configuration;

        public LoginCustomerUserCommandHandler(UserManager<User> userManager, IEventBus eventBus, ICurrentUser currentUser, IConfiguration configuration)
        {
            _userManager = userManager;
            _eventBus = eventBus;
            _currentUser = currentUser;
            _configuration = configuration;
        }
        

        public async Task<LoginCustomerUserCommandResponse> Handle(LoginCustomerUserCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<LoginCustomerUserCommandResponse> next)
        {
            var adminUser = await _userManager.FindByNameAsync(request.UserName);
            if (adminUser is null || adminUser.IsDeleted) 
                throw new AppException(ResultCode.BadRequest, "user password or username is not correct");
            
            var passwordChecker = await _userManager.CheckPasswordAsync(adminUser, request.Password);
            if (!passwordChecker)
                throw new AppException(ResultCode.BadRequest, "user password or username is not correct");
            
            var command = new AccessTokenCommandRequest
            {
                SubjectId = adminUser.SubjectId.ToString(),
                UserType = UserType.customer,
                UserId = adminUser.Id
            };
            var userToken=await _eventBus.Issue(command, cancellationToken);
            _currentUser.SetHttpOnlyUserCookie("X-Access-Token", userToken.Data.AccessToken,
                DateTimeOffset.Now.AddSeconds(int.Parse(_configuration["JwtToken:AccessTokenExpiredTime"])),
                _configuration["JwtToken:DomainUrl"]);
            
            _currentUser.SetHttpOnlyUserCookie("X-Refresh-Token", userToken.Data.RefreshToken,
                DateTimeOffset.Now.AddDays(int.Parse(_configuration["JwtToken:ExpirationDays"])), _configuration["JwtToken:DomainUrl"]);
            
            return new LoginCustomerUserCommandResponse(true, ResultCode.Success);
        }
    }
}
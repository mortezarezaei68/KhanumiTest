using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Buses;
using Framework.Commands.CommandHandlers;
using Framework.Common;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using ServiceContract.Command.AdminUserCommands;
using ServiceContract.Command.UserTokenCommands;

namespace Service.Contract.AdminUserCommandHandlers
{
    public class SignOutCommandHandler:ITransactionalCommandHandlerMediatR<SignOutAdminUserCommandRequest,SignOutAdminUserCommandResponse>
    {
        private readonly IEventBus _eventBus;
        private readonly ICurrentUser _currentUser;

        public SignOutCommandHandler(IEventBus eventBus, ICurrentUser currentUser)
        {
            _eventBus = eventBus;
            _currentUser = currentUser;
        }

        public async Task<SignOutAdminUserCommandResponse> Handle(SignOutAdminUserCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<SignOutAdminUserCommandResponse> next)
        {

            var userId = _currentUser.GetUserId();
            if (userId is null)
                return new SignOutAdminUserCommandResponse(false, ResultCode.UnAuthorized);
                
            
            var persistGrant = new InvokePersistGrantsCommandRequest
            {
                UserId = userId
            };
            await _eventBus.Issue(persistGrant, cancellationToken);
            _currentUser.CleanSecurityCookie("X-Access-Token");
            _currentUser.CleanSecurityCookie("X-Refresh-Token");
            return new SignOutAdminUserCommandResponse(true, ResultCode.Success);
        }
    }
}
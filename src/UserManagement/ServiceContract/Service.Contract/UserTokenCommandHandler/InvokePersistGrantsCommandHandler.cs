using System.Threading;
using System.Threading.Tasks;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceContract.Command.UserTokenCommands;
using UserManagement.Domains;

namespace Service.Contract.UserTokenCommandHandler
{
    public class InvokePersistGrantsCommandHandler:ITransactionalCommandHandlerMediatR<InvokePersistGrantsCommandRequest,InvokePersistGrantsCommandResponse>
    {
        private readonly UserManager<User> _userManager;

        public InvokePersistGrantsCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<InvokePersistGrantsCommandResponse> Handle(InvokePersistGrantsCommandRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<InvokePersistGrantsCommandResponse> next)
        {
            var user = await _userManager.Users.Include(a => a.PersistGrants)
                .FirstOrDefaultAsync(a => a.SubjectId.ToString() == request.UserId, cancellationToken: cancellationToken);
            user.UpdatePersistGrants();
            return new InvokePersistGrantsCommandResponse(true, ResultCode.Success);
        }
    }
}
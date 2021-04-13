using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using TicketManagement.Commands;
using TicketManagement.Domain;

namespace TicketManagement.CommandHandler
{
    public class UpdateTicketStateCommandHandler:ITransactionalCommandHandlerMediatR<UpdateTicketStateCommandRequest,UpdateTicketStateCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;

        public UpdateTicketStateCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<UpdateTicketStateCommandResponse> Handle(UpdateTicketStateCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<UpdateTicketStateCommandResponse> next)
        {
            var ticket = await _ticketRepository.GetById(request.TicketId);
            if (ticket is null)
            {
                throw new AppException(ResultCode.BadRequest, "ticket is null");
            }
            ticket.UpdateState((TicketStatus)request.TicketState);
            _ticketRepository.Update(ticket);
            return new UpdateTicketStateCommandResponse(true, ResultCode.Success);
        }
    }
}
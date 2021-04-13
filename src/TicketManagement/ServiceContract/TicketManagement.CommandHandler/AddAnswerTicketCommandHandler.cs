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
    public class AddAnswerTicketCommandHandler:ITransactionalCommandHandlerMediatR<AddAnswerTicketCommandRequest,AddAnswerTicketCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;

        public AddAnswerTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<AddAnswerTicketCommandResponse> Handle(AddAnswerTicketCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AddAnswerTicketCommandResponse> next)
        {
            var ticket =await _ticketRepository.GetById(request.TicketId);
            if (ticket is null)
            {
                throw new AppException(ResultCode.BadRequest, "can not find Ticket");
            }
            ticket.AddAnswer(request.Answer);
            _ticketRepository.Update(ticket);
            return new AddAnswerTicketCommandResponse(true, ResultCode.Success);
        }
    }
}
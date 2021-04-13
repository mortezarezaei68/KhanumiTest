using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using TicketManagement.Commands;
using TicketManagement.Domain;

namespace TicketManagement.CommandHandler
{
    public class AddTicketCommandHandler:ITransactionalCommandHandlerMediatR<AddTicketCommandRequest,AddTicketCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private static Random random = new Random();
        public AddTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public  Task<AddTicketCommandResponse> Handle(AddTicketCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AddTicketCommandResponse> next)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var refreshToken= new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            var ticket = new Ticket(request.Description, request.Title,refreshToken);
            _ticketRepository.Add(ticket);
            var ticketResponse = new TicketResponse
            {
                TrackingCode = ticket.TrackingCode
            };
            return Task.FromResult( new AddTicketCommandResponse(true, ResultCode.Success,ticketResponse));
        }
    }
}
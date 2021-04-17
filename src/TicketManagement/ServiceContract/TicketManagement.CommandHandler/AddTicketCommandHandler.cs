using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Framework.Commands.CommandHandlers;
using Framework.Common;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using TicketManagement.Commands;
using TicketManagement.Domain;

namespace TicketManagement.CommandHandler
{
    public class AddTicketCommandHandler:ITransactionalCommandHandlerMediatR<AddTicketCommandRequest,AddTicketCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        
        public AddTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public  Task<AddTicketCommandResponse> Handle(AddTicketCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AddTicketCommandResponse> next)
        {
            var ticket = new Ticket(request.Description, request.Title,CodeGenerator.CreateCode());
            _ticketRepository.Add(ticket);
            var ticketResponse = new TicketResponse
            {
                TrackingCode = ticket.TrackingCode
            };
            return Task.FromResult( new AddTicketCommandResponse(true, ResultCode.Success,ticketResponse));
        }
    }
    public class AddTicketCommandRequestValidator : AbstractValidator<AddTicketCommandRequest>
    {
        public AddTicketCommandRequestValidator()
        {
            RuleFor(p => p.Description).NotEmpty().NotNull();
            RuleFor(p => p.Title).NotEmpty().NotNull();
        }
    }
}
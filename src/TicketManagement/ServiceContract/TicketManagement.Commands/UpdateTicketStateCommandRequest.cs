using MediatR;

namespace TicketManagement.Commands
{
    public class UpdateTicketStateCommandRequest:IRequest<UpdateTicketStateCommandResponse>
    {
        public int TicketId { get; set; }
        public int TicketState { get; set; }
    }
}
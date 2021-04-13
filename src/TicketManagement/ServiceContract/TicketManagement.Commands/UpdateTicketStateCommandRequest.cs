using MediatR;

namespace TicketManagement.Commands
{
    public class UpdateTicketStateCommandRequest:IRequest<UpdateTicketStateCommandResponse>
    {
        public int Id { get; set; }
        public int TicketState { get; set; }
    }
}
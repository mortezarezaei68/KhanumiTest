using MediatR;

namespace TicketManagement.Commands
{
    public class AddAnswerTicketCommandRequest:IRequest<AddAnswerTicketCommandResponse>
    {
        public int TicketId { get; set; }
        public string Answer { get; set; }
    }
}
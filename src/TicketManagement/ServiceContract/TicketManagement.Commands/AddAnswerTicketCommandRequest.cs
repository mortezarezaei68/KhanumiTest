using MediatR;

namespace TicketManagement.Commands
{
    public class AddAnswerTicketCommandRequest:IRequest<AddAnswerTicketCommandResponse>
    {
        public string Answer { get; set; }
    }
}
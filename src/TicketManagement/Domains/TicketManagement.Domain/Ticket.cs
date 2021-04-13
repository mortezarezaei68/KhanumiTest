using System.Collections.Generic;
using Framework.Domain.Core;

namespace TicketManagement.Domain
{
    public class Ticket:AggregateRoot<int>
    {
        private readonly List<AnswerTicket> _answerTickets = new();
        public IReadOnlyCollection<AnswerTicket> AnswerTickets => _answerTickets ;
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TicketStatus TicketStatus { get; private set; }

        public void Add(string description, string title)
        {
            TicketStatus = TicketStatus.WaitingForReply;
            Description = description;
            Title = title;
        }

        public void Update(string description, string title)
        {
            Description = description;
            Title = title;
        }

        public void UpdateState(TicketStatus ticketStatus)
        {
            TicketStatus = ticketStatus;
        }

        public void AddAnswer(string answer)
        {
            _answerTickets.Add(new AnswerTicket(answer));
        }
    }
}
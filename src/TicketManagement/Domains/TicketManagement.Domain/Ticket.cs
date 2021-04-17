using System.Collections.Generic;
using System.Linq;
using Common.Exceptions;
using Framework.Domain.Core;
using Framework.Exception.Exceptions.Enum;

namespace TicketManagement.Domain
{
    public class Ticket:AggregateRoot<int>
    {
        public Ticket()
        {
            
        }
        public Ticket(string description, string title, string trackingCode)
        {
            
            TicketStatus = TicketStatus.WaitingForReply;
            Description = description;
            Title = title;
            TrackingCode = trackingCode;
        }
        private readonly List<AnswerTicket> _answerTickets = new();
        public IReadOnlyCollection<AnswerTicket> AnswerTickets => _answerTickets ;
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TicketStatus TicketStatus { get; private set; }
        public string TrackingCode { get; private set; }
        

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
        public void UpdateAnswer(string answer,int id)
        {
            var existAnswer=_answerTickets.FirstOrDefault(a => a.Id == id);
            if (existAnswer is null)
                throw new AppException(ResultCode.BadRequest, "can not find answer");
            
            existAnswer.Update(answer);

        }
    }
}
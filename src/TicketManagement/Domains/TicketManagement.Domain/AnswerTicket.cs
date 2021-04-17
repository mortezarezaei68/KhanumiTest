using Framework.Domain.Core;

namespace TicketManagement.Domain
{
    public class AnswerTicket:Entity<int>
    {
        public AnswerTicket(string answer)
        {
            Answer = answer;
        }

        public string Answer { get;private set; }

        public void Update(string answer)
        {
            Answer = answer;
        }
    }
}
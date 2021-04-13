using System.Collections.Generic;

namespace TicketManagement.Query.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CreatorName { get; set; }
        public IEnumerable<AnswerTicketModel> AnswerTicketModels { get; set; }
    }
}
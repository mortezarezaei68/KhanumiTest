using System.Collections.Generic;
using System.Linq;
using Framework.Queries;

namespace TicketManagement.Query.Models.TicketQueryModel
{
    public class GetAllTicketsQueryResponse:ResponseQuery<IEnumerable<TicketModel>>
    {
        public GetAllTicketsQueryResponse(bool isSuccess, IEnumerable<TicketModel> data, int count = 1, string message = null) : base(isSuccess, data, data.Count(), message)
        {
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Framework.Queries;

namespace TicketManagement.Query.Models.TicketQueryModel
{
    public class GetTicketsByUserIdResponse:ResponseQuery<IEnumerable<TicketModel>>
    {
        public GetTicketsByUserIdResponse(bool isSuccess, IEnumerable<TicketModel> data, int count = 1, string message = null) : base(isSuccess, data, data.Count(), message)
        {
        }
    }
}
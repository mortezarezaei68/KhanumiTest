using Framework.Queries;

namespace TicketManagement.Query.Models.TicketQueryModel
{
    public class GetTicketByIdQueryResponse:ResponseQuery<TicketModel>
    {
        public GetTicketByIdQueryResponse(bool isSuccess, TicketModel data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
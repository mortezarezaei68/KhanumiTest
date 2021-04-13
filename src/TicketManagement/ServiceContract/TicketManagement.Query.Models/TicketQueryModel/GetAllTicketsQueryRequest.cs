using MediatR;

namespace TicketManagement.Query.Models.TicketQueryModel
{
    public class GetAllTicketsQueryRequest:IRequest<GetAllTicketsQueryResponse>
    {
    }
}
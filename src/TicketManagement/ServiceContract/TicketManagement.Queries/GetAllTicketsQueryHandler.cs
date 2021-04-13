using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Queries;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Persistance.EF.Context;
using TicketManagement.Query.Models;
using TicketManagement.Query.Models.TicketQueryModel;

namespace TicketManagement.Queries
{
    public class GetAllTicketsQueryHandler:IQueryHandlerMediatR<GetAllTicketsQueryRequest,GetAllTicketsQueryResponse>
    {
        private readonly TicketManagementDbContext _ticketManagementDbContext;

        public GetAllTicketsQueryHandler(TicketManagementDbContext ticketManagementDbContext)
        {
            _ticketManagementDbContext = ticketManagementDbContext;
        }

        public async Task<GetAllTicketsQueryResponse> Handle(GetAllTicketsQueryRequest request, CancellationToken cancellationToken)
        {
            var tickets=await _ticketManagementDbContext.Tickets.Include(a => a.AnswerTickets).ToListAsync(cancellationToken: cancellationToken);
            var ticketViewModel=tickets.Select(a => new TicketModel
            {
                Id = a.Id,
                Title = a.Title,
                AnswerTicketModels = a.AnswerTickets.Select(a => new AnswerTicketModel
                {
                    Answer = a.Answer,
                    Id = a.Id
                })
            });
            return new GetAllTicketsQueryResponse(true, ticketViewModel);
        }
    }
}
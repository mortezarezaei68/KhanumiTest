using System.Threading.Tasks;
using Framework.Domain.UnitOfWork;
using Framework.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Domain;
using TicketManagement.Persistance.EF.Context;

namespace TicketManagement.Persistance.EF.Repository
{
    public class TicketRepository:ITicketRepository
    {
        private readonly UnitOfWork<TicketManagementDbContext> _unitOfWork;
        private readonly TicketManagementDbContext _managementDbContext;

        public TicketRepository(UnitOfWork<TicketManagementDbContext> unitOfWork, TicketManagementDbContext managementDbContext)
        {
            _unitOfWork = unitOfWork;
            _managementDbContext = managementDbContext;
        }

        public void Add(Ticket ticket)
        {
            _managementDbContext.Tickets.Add(ticket);
        }

        public void Update(Ticket ticket)
        {
            _managementDbContext.Tickets.Update(ticket);
        }

        public async Task<Ticket> GetById(int id) => await _managementDbContext.Tickets.Include(a => a.AnswerTickets)
            .FirstOrDefaultAsync(a => a.Id == id);
        

        public IUnitOfWork UnitOfWork => _unitOfWork;
    }
}
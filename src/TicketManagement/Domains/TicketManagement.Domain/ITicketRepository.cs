using System.Threading.Tasks;
using Framework.Domain.UnitOfWork;

namespace TicketManagement.Domain
{
    public interface ITicketRepository:IRepository
    {
        void Add(Ticket ticket);
        void Update(Ticket ticket);
        Task<Ticket> GetById(int id);
    }
}
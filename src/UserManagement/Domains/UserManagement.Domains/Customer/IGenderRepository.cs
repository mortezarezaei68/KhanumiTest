using System.Threading.Tasks;
using Framework.Domain.UnitOfWork;

namespace UserManagement.Domains.Customer
{
    public interface IGenderRepository:IRepository
    {
        void Add(Gender gender);
        Task<Gender> GetByName(string name);
        Task<Gender> GetById(int id);
        void Update(Gender gender);
    }
}
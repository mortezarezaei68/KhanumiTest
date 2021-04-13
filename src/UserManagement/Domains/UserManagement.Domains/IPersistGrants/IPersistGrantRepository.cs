
using System.Threading.Tasks;
using Framework.Domain.UnitOfWork;

namespace UserManagement.Domains.IPersistGrants
{
    public interface IPersistGrantRepository:IRepository
    {
         void Add(PersistGrants persistGrants);
         Task<PersistGrants> GetByUser(string subjectId);
    }
}
using System.Threading.Tasks;
using Framework.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domains;
using UserManagement.Domains.IPersistGrants;
using UserManagement.Persistance.Context;
using UserManagement.Persistance.UnitOfWork;

namespace UserManagement.Persistance.Repository
{
    public class PersistGrantsRepository:IPersistGrantRepository
    {
        private readonly IdentityUnitOfWork<ApplicationDbContext, User, Role> _identityUnitOfWork;
        private readonly ApplicationDbContext _applicationDbContext;

        public PersistGrantsRepository(IdentityUnitOfWork<ApplicationDbContext, User, Role> identityUnitOfWork, ApplicationDbContext applicationDbContext)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _applicationDbContext = applicationDbContext;
        }

        public IUnitOfWork UnitOfWork => _identityUnitOfWork;
        public void Add(PersistGrants persistGrants)
        {
            _applicationDbContext.PersistGrants.Add(persistGrants);
        }

        public async Task<PersistGrants> GetByUser(string subjectId)
        {
            var persistGrants=await _applicationDbContext.PersistGrants.FirstOrDefaultAsync(a => a.SubjectId == subjectId);
            return persistGrants;
        }
    }
}
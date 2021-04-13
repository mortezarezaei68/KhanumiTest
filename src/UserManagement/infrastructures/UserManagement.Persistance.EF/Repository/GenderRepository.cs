using System.Threading.Tasks;
using Framework.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domains;
using UserManagement.Domains.Customer;
using UserManagement.Persistance.Context;
using UserManagement.Persistance.UnitOfWork;

namespace UserManagement.Persistance.Repository
{
    public class GenderRepository:IGenderRepository
    {
        private readonly IdentityUnitOfWork<ApplicationDbContext, User, Role> _identityUnitOfWork;
        private readonly ApplicationDbContext _applicationDbContext;

        public GenderRepository(IdentityUnitOfWork<ApplicationDbContext, User, Role> identityUnitOfWork, ApplicationDbContext applicationDbContext)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _applicationDbContext = applicationDbContext;
        }

        public IUnitOfWork UnitOfWork => _identityUnitOfWork;
        public void Add(Gender gender)
        {
            _applicationDbContext.Genders.Add(gender);
        }

        public async Task<Gender> GetByName(string name)
        {
            return await _applicationDbContext.Genders.FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task<Gender> GetById(int id)
        {
            return await _applicationDbContext.Genders.FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Update(Gender gender)
        {
            _applicationDbContext.Genders.Update(gender);
        }
    }
}
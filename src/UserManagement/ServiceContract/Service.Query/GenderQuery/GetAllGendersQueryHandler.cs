using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Queries;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.GenderQuery;
using UserManagement.Persistance.Context;

namespace Service.Query.GenderQuery
{
    public class GetAllGendersQueryHandler:IQueryHandlerMediatR<GetAllGendersQueryRequest,GetAllGendersQueryResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GetAllGendersQueryHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<GetAllGendersQueryResponse> Handle(GetAllGendersQueryRequest request, CancellationToken cancellationToken)
        {
            var data= await _applicationDbContext.Genders.Select(a => new GenderModel
            {
                Id = a.Id,
                Name = a.Name
            }).ToListAsync(cancellationToken: cancellationToken);
            return new GetAllGendersQueryResponse(true, data, data.Count);
        }
    }
}
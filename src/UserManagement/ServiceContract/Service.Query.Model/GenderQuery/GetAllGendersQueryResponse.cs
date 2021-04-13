using System.Collections.Generic;
using Framework.Queries;

namespace Service.Query.Model.GenderQuery
{
    public class GetAllGendersQueryResponse:ResponseQuery<IEnumerable<GenderModel>>
    {
        public GetAllGendersQueryResponse(bool isSuccess, IEnumerable<GenderModel> data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
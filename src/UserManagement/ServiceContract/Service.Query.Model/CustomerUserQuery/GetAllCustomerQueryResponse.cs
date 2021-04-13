using System.Collections.Generic;
using Framework.Queries;

namespace Service.Query.Model.CustomerUserQuery
{
    public class GetAllCustomerQueryResponse:ResponseQuery<IEnumerable<CustomerUserModel>>
    {
        public GetAllCustomerQueryResponse(bool isSuccess, IEnumerable<CustomerUserModel> data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
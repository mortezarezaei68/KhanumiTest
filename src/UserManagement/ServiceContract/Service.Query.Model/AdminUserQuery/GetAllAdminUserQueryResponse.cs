using System.Collections.Generic;
using Framework.Queries;

namespace Service.Query.Model.AdminUserQuery
{
    public class GetAllAdminUserQueryResponse:ResponseQuery<IEnumerable<AdminUserModel>>
    {
        public GetAllAdminUserQueryResponse(bool isSuccess, IEnumerable<AdminUserModel> data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
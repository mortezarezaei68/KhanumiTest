using System.Collections.Generic;
using Framework.Queries;

namespace Service.Query.Model.AdminRoleQuery
{
    public class GetAllAdminRolesQueryResponse:ResponseQuery<IEnumerable<RoleModel>>
    {
        public GetAllAdminRolesQueryResponse(bool isSuccess, IEnumerable<RoleModel> data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
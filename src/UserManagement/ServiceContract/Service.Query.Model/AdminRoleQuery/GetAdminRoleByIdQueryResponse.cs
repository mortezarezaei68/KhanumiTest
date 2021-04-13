using Framework.Queries;

namespace Service.Query.Model.AdminRoleQuery
{
    public class GetAdminRoleByIdQueryResponse:ResponseQuery<RoleModel>
    {
        public GetAdminRoleByIdQueryResponse(bool isSuccess, RoleModel data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
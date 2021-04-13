using Framework.Queries;

namespace Service.Query.Model.AdminUserQuery
{
    public class GetAdminUserQueryResponse:ResponseQuery<AdminUserModel>
    {
        public GetAdminUserQueryResponse(bool isSuccess, AdminUserModel data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
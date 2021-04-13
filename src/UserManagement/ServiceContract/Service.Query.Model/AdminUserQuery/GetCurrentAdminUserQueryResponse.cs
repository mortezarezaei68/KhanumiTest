using Framework.Queries;

namespace Service.Query.Model.AdminUserQuery
{
    public class GetCurrentAdminUserQueryResponse:ResponseQuery<AdminCurrentUserModel>
    {
        public GetCurrentAdminUserQueryResponse(bool isSuccess, AdminCurrentUserModel data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
using MediatR;

namespace Service.Query.Model.AdminUserQuery
{
    public class GetAdminUserQueryRequest:IRequest<GetAdminUserQueryResponse>
    {
        public int UserId { get; set; }
    }
}
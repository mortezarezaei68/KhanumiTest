using MediatR;

namespace Service.Query.Model.AdminRoleQuery
{
    public class GetAdminRoleByIdQueryRequest:IRequest<GetAdminRoleByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
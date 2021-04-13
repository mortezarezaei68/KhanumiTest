using Framework.Queries;

namespace Service.Query.Model.CustomerUserQuery
{
    public class CustomerByTokenIdQueryResponse:ResponseQuery<CustomerUserModel>
    {
        public CustomerByTokenIdQueryResponse(bool isSuccess, CustomerUserModel data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace TicketManagement.Commands
{
    public class AddTicketCommandResponse:ResponseCommand<TicketResponse>
    {
        public AddTicketCommandResponse(bool isSuccess, ResultCode resultCode, TicketResponse data, string message = null) : base(isSuccess, resultCode, data, message)
        {
        }
    }

    public class TicketResponse
    {
        public string TrackingCode { get; set; }
    }
}
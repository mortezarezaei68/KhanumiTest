using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace TicketManagement.Commands
{
    public class AddTicketCommandResponse:ResponseCommand
    {
        public AddTicketCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
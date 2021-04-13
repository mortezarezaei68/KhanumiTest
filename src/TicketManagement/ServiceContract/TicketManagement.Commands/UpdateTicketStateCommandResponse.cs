using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace TicketManagement.Commands
{
    public class UpdateTicketStateCommandResponse:ResponseCommand
    {
        public UpdateTicketStateCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
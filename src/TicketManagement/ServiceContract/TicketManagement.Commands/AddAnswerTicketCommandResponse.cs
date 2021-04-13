using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace TicketManagement.Commands
{
    public class AddAnswerTicketCommandResponse:ResponseCommand
    {
        public AddAnswerTicketCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
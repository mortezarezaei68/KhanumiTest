using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ServiceContract.Command.GenderCommands
{
    public class AddGenderCommandResponse:ResponseCommand
    {
        public AddGenderCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ServiceContract.Command.GenderCommands
{
    public class EditGenderCommandResponse:ResponseCommand
    {
        public EditGenderCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}
using MediatR;

namespace ServiceContract.Command.GenderCommands
{
    public class AddGenderCommandRequest:IRequest<AddGenderCommandResponse>
    {
        public string Name { get; set; }
    }
}
using MediatR;

namespace ServiceContract.Command.GenderCommands
{
    public class EditGenderCommandRequest:IRequest<EditGenderCommandResponse>
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using ServiceContract.Command.GenderCommands;
using UserManagement.Domains.Customer;

namespace Service.Contract.GenderCommandHandlers
{
    public class AddGenderCommandHandler:ITransactionalCommandHandlerMediatR<AddGenderCommandRequest,AddGenderCommandResponse>
    {
        private readonly IGenderRepository _genderRepository;

        public AddGenderCommandHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<AddGenderCommandResponse> Handle(AddGenderCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AddGenderCommandResponse> next)
        {
            if (request.Name is null)
                throw new AppException(ResultCode.BadRequest, "name is not exist");
            var existGender=await _genderRepository.GetByName(request.Name);
            if (existGender is not null)
                throw new AppException(ResultCode.BadRequest, "gender is exist");
            
            _genderRepository.Add(new Gender(request.Name));
            return new AddGenderCommandResponse(true,ResultCode.Success);
        }
    }
}
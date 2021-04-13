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
    public class EditGenderCommandHandler:ITransactionalCommandHandlerMediatR<EditGenderCommandRequest,EditGenderCommandResponse>
    {
        private readonly IGenderRepository _genderRepository;

        public EditGenderCommandHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<EditGenderCommandResponse> Handle(EditGenderCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<EditGenderCommandResponse> next)
        {
            var genderExist = await _genderRepository.GetByName(request.Name);
            if (genderExist is not null)
                throw new AppException(ResultCode.BadRequest, "gender is duplicate");

            var gender = await _genderRepository.GetById(request.Id);
            if (gender is null)
                throw new AppException(ResultCode.BadRequest, "gender is not exist");
                
            gender.Update(request.Name);
            _genderRepository.Update(gender);
            return new EditGenderCommandResponse(true, ResultCode.Success);

        }
    }
}
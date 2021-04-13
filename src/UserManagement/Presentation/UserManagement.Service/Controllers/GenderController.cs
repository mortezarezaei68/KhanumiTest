using System.Threading.Tasks;
using Framework.Buses;
using Framework.Controller.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Query.Model.GenderQuery;
using ServiceContract.Command.GenderCommands;

namespace UserManagement.Service.Controllers
{
    [Route("api/v1/[controller]")]
    public class GenderController:BaseController
    {
        private readonly IEventBus _eventBus;

        public GenderController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> AddGender(AddGenderCommandRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }
        
        
        [HttpPut]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> EditGender(EditGenderCommandRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllGender()
        {
            var genders = await _eventBus.IssueQuery(new GetAllGendersQueryRequest());
            return Ok(genders);
        }
    }
}